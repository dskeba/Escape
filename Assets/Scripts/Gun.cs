using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate = 0.2f;
    private float timer;
    public Transform firePoint;
    private GameObject tracerObject;
    private LineRenderer tracerLineRenderer;
    private GameObject muzzleFlashObject;
    private Light muzzleFlashLight;
    private Vector3 tracerStartPoint;
    private Vector3 tracerEndPoint;

    private void Start()
    {
        tracerObject = GameObject.FindGameObjectWithTag("Tracer");
        tracerLineRenderer = tracerObject.GetComponent<LineRenderer>();
        muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        muzzleFlashLight = muzzleFlashObject.GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        tracerStartPoint = firePoint.position;
        tracerEndPoint = ray.direction * 1000;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.layer != 8)
            {
                hit.collider.transform.localScale = Vector3.zero;
            }
            tracerEndPoint = hit.point;
        }
        StartCoroutine(DrawTracerForSeconds(0.015f));
        StartCoroutine(DrawMuzzleFlashForSeconds(0.015f));
    }

    private IEnumerator DrawMuzzleFlashForSeconds(float secondsToFlash)
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(secondsToFlash);
        muzzleFlashLight.enabled = false;
    }

    private IEnumerator DrawTracerForSeconds(float secondsToWait)
    {
        tracerLineRenderer.SetPosition(0, tracerStartPoint);
        tracerLineRenderer.SetPosition(1, tracerEndPoint);
        tracerLineRenderer.enabled = true;
        yield return new WaitForSeconds(secondsToWait);
        tracerLineRenderer.enabled = false;
    }
}
