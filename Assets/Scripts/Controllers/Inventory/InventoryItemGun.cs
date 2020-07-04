using UnityEngine;
using System.Collections;

public abstract class InventoryItemGun : InventoryItemUsable
{
    GameObject tracerObject;
    LineRenderer tracerLineRenderer;
    GameObject muzzleFlashObject;
    Light muzzleFlashLight;
    float muzzleFlashSeconds = 0.015f;
    float tracerSeconds = 0.015f;
    Vector3 tracerStartPoint;
    Vector3 tracerEndPoint;
    float tracerMaxDistance = 10000f;

    protected abstract void OnGunAwake();
    protected abstract void OnGunStart();
    protected abstract void OnGunFixedUpdate();
    protected abstract void OnGunUpdate();
    protected abstract void OnFireGun();

    public Transform firePoint;

    protected override void OnUsableAwake()
    {
        base.ItemType = InventoryItemType.Gun;
        OnGunAwake();
    }

    protected override void OnUsableStart()
    {
        tracerObject = GameObject.FindGameObjectWithTag("Tracer");
        tracerLineRenderer = tracerObject.GetComponent<LineRenderer>();
        muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        muzzleFlashLight = muzzleFlashObject.GetComponent<Light>();
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
        tracerStartPoint = firePoint.position;
        tracerEndPoint = ray.direction * tracerMaxDistance;
        RaycastHit hit;
        int layerMask = 1 << 10;
        if (Physics.Raycast(ray, out hit, tracerMaxDistance, layerMask))
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
            tracerEndPoint = hit.point;
        }
        StartCoroutine(DrawTracerForSeconds(tracerSeconds));
        StartCoroutine(DrawMuzzleFlashForSeconds(muzzleFlashSeconds));

        OnFireGun();
    }

    private IEnumerator DrawMuzzleFlashForSeconds(float seconds)
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(seconds);
        muzzleFlashLight.enabled = false;
    }

    private IEnumerator DrawTracerForSeconds(float seconds)
    {
        tracerLineRenderer.SetPosition(0, tracerStartPoint);
        tracerLineRenderer.SetPosition(1, tracerEndPoint);
        tracerLineRenderer.enabled = true;
        yield return new WaitForSeconds(seconds);
        tracerLineRenderer.enabled = false;
    }
}
