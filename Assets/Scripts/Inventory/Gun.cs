using UnityEngine;
using System.Collections;
using System;

public abstract class Gun : Usable
{
    public Transform firePoint;

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

    public AmmoType AmmoType { get; set; }
    public int MagSize { get; set; }
    public int AmmoLoaded { get; set; }

    protected abstract void OnGunAwake();
    protected abstract void OnGunStart();
    protected abstract void OnGunFixedUpdate();
    protected abstract void OnGunUpdate();
    protected abstract void OnFireGun();

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
        if (Input.GetKeyDown("r"))
        {
            ReloadAmmo();
        }

        OnGunUpdate();
    }

    protected override void OnUsableFixedUpdate()
    {
        OnGunFixedUpdate();
    }

    protected override void OnUse()
    {
        if (!UseAmmo())
        {
            SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/empty_fire", 0.25f);
            return;
        }

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

    private bool ReloadAmmo()
    {
        int ammoAvailable = AmmoSystem.Instance.GetQuantity(AmmoType);
        if (ammoAvailable <= 0)
        {
            return false;
        }
        int ammoNeeded = MagSize - AmmoLoaded;
        int ammoReloadAmount;
        if (ammoAvailable < ammoNeeded)
        {
            ammoReloadAmount = ammoAvailable;
        } 
        else
        {
            ammoReloadAmount = ammoNeeded;
        }
        AmmoSystem.Instance.RemoveAmmo(AmmoType, ammoReloadAmount);
        AmmoLoaded += ammoReloadAmount;
        return true;
    }

    private Boolean UseAmmo()
    {
        if (AmmoLoaded <= 0)
        {
            return false;
        }
        AmmoLoaded -= 1;
        return true;
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
