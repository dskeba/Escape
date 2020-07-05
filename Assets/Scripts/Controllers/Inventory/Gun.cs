using UnityEngine;
using System.Collections;

public abstract class Gun : Usable
{
    public Transform firePoint;

    protected abstract void OnGunAwake();
    protected abstract void OnGunStart();
    protected abstract void OnGunFixedUpdate();
    protected abstract void OnGunUpdate();
    protected abstract void OnFireGun();

    private GameObject _tracerObject;
    private LineRenderer _tracerLineRenderer;
    private GameObject _muzzleFlashObject;
    private Light _muzzleFlashLight;
    private float _muzzleFlashSeconds = 0.015f;
    private float _tracerSeconds = 0.015f;
    private Vector3 _tracerStartPoint;
    private Vector3 _tracerEndPoint;
    private float _tracerMaxDistance = 10000f;

    public Gun() { }

    protected override void OnUsableAwake()
    {
        OnGunAwake();
    }

    protected override void OnUsableStart()
    {
        _tracerObject = GameObject.FindGameObjectWithTag("Tracer");
        _tracerLineRenderer = _tracerObject.GetComponent<LineRenderer>();
        _muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        _muzzleFlashLight = _muzzleFlashObject.GetComponent<Light>();

        OnGunStart();
    }

    protected override void OnUsableUpdate()
    {
        OnGunUpdate();
    }

    protected override void OnUsableFixedUpdate()
    {
        OnGunFixedUpdate();
    }

    protected override void OnUse()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        _tracerStartPoint = firePoint.position;
        _tracerEndPoint = ray.direction * _tracerMaxDistance;
        RaycastHit hit;
        int layerMask = 1 << 10;

        if (Physics.Raycast(ray, out hit, _tracerMaxDistance, layerMask))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                var parentTransform = hit.collider.transform.root;
                var colliderHealth = parentTransform.GetComponent<ZombieHealth>();
                if (colliderHealth != null)
                {
                    colliderHealth.TakeDamage(25, hit.point);
                }
            }
            _tracerEndPoint = hit.point;
        }

        StartCoroutine(DrawTracerForSeconds(_tracerSeconds));
        StartCoroutine(DrawMuzzleFlashForSeconds(_muzzleFlashSeconds));

        OnFireGun();
    }

    private IEnumerator DrawMuzzleFlashForSeconds(float seconds)
    {
        _muzzleFlashLight.enabled = true;

        yield return new WaitForSeconds(seconds);

        _muzzleFlashLight.enabled = false;
    }

    private IEnumerator DrawTracerForSeconds(float seconds)
    {
        _tracerLineRenderer.SetPosition(0, _tracerStartPoint);
        _tracerLineRenderer.SetPosition(1, _tracerEndPoint);
        _tracerLineRenderer.enabled = true;

        yield return new WaitForSeconds(seconds);

        _tracerLineRenderer.enabled = false;
    }
}
