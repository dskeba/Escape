using UnityEngine;
using System.Collections;
using System;

public abstract class Gun : Usable
{
    public event EventHandler<GunEvent> Reload;
    public event EventHandler<GunEvent> Fire;

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
    private float _reloadTimer = 0f;
    private bool _isReloading = false;

    public Gun() { }

    public AmmoType AmmoType { get; set; }
    public int MagSize { get; set; }
    public int AmmoLoaded { get; set; }
    public float ReloadTime { get; set; }

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
        if (Input.GetKeyDown("r") && !_isReloading)
        {
            _isReloading = true;
        }

        if (_isReloading)
        {
            ReloadTimer();
        }

        OnGunUpdate();
    }

    protected override void OnUsableFixedUpdate()
    {
        OnGunFixedUpdate();
    }

    protected override void OnUse()
    {
        if (_isReloading)
        {
            return;
        }

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

        if (Fire != null)
        {
            Fire(this, new GunEvent(this));
        }

        OnFireGun();
    }

    private void ReloadTimer()
    {
        _reloadTimer += Time.deltaTime;
        if (_reloadTimer < ReloadTime)
        {
            return;
        }
        ReloadAmmo();
        _isReloading = false;
        _reloadTimer = 0;
    }

    private bool ReloadAmmo()
    {
        int ammoAvailable = AmmoSupply.Instance.GetQuantity(AmmoType);
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
        AmmoSupply.Instance.RemoveAmmo(AmmoType, ammoReloadAmount);
        AmmoLoaded += ammoReloadAmount;
        if (Reload != null)
        {
            Reload(this, new GunEvent(this));
        }
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
