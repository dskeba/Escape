using UnityEngine;
using System.Collections;

public abstract class Gun : InventoryItemBase
{
    float fireRateTimer = 0f;
    GameObject tracerObject;
    LineRenderer tracerLineRenderer;
    GameObject muzzleFlashObject;
    Light muzzleFlashLight;
    float muzzleFlashSeconds = 0.015f;
    float tracerSeconds = 0.015f;
    Vector3 tracerStartPoint;
    Vector3 tracerEndPoint;
    float tracerMaxDistance = 10000f;

    protected float fireRate = 1f;
    protected bool autoFire = false;
    protected abstract void OnAwake();
    protected abstract void OnStart();
    protected abstract void OnFixedUpdate();
    protected abstract void OnUpdate();
    protected abstract void OnFireGun();

    public Transform firePoint;

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        tracerObject = GameObject.FindGameObjectWithTag("Tracer");
        tracerLineRenderer = tracerObject.GetComponent<LineRenderer>();
        muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        muzzleFlashLight = muzzleFlashObject.GetComponent<Light>();

        OnStart();
    }

    void Update()
    {
        if (!base.IsEquipped) { return; }

        fireRateTimer += Time.deltaTime;
        if (fireRateTimer >= fireRate)
        {
            if (Input.GetButton("Fire1") && autoFire ||
                Input.GetButtonDown("Fire1") && !autoFire)
            {
                fireRateTimer = 0f;
                FireGun();
            }
        }

        OnUpdate();
    }

    void FixedUpdate()
    {
        if (!base.IsEquipped) { return; }

        OnFixedUpdate();
    }

    public void FireGun()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        tracerStartPoint = firePoint.position;
        tracerEndPoint = ray.direction * tracerMaxDistance;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, tracerMaxDistance))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                //hit.collider.transform.localScale = Vector3.zero;
                var parentTransform = hit.collider.transform.root;
                var colliderHealth = parentTransform.GetComponent<ZombieHealth>();
                colliderHealth.TakeDamage(25, hit.point);
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
