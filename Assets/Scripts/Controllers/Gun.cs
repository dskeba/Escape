using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate = 0.2f;
    private float fireRateTimer;
    public Transform firePoint;
    private GameObject tracerObject;
    private LineRenderer tracerLineRenderer;
    private GameObject muzzleFlashObject;
    private Light muzzleFlashLight;
    private float muzzleFlashSeconds = 0.015f;
    private float tracerSeconds = 0.015f;
    private Vector3 tracerStartPoint;
    private Vector3 tracerEndPoint;
    private float tracerMaxDistance = 10000f;

    private void Start()
    {
        tracerObject = GameObject.FindGameObjectWithTag("Tracer");
        tracerLineRenderer = tracerObject.GetComponent<LineRenderer>();
        muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        muzzleFlashLight = muzzleFlashObject.GetComponent<Light>();
        
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                fireRateTimer = 0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        tracerStartPoint = firePoint.position;
        tracerEndPoint = ray.direction * tracerMaxDistance;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, tracerMaxDistance))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                hit.collider.transform.localScale = Vector3.zero;
                var parentTransform = hit.collider.transform.root;
                var colliderHealth = parentTransform.GetComponent<ZombieHealth>();
                colliderHealth.TakeDamage(50, hit.point);
            }
            tracerEndPoint = hit.point;
        }
        StartCoroutine(DrawTracerForSeconds(tracerSeconds));
        StartCoroutine(DrawMuzzleFlashForSeconds(muzzleFlashSeconds));
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