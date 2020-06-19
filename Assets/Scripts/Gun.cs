using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Gun : MonoBehaviour
{
    private float fireRate = 0.2f;

    private int damage = 1;

    private float timer;

    public Transform firePoint;

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
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);

        RaycastHit hitInfo;
        GameObject lineObject = GameObject.FindGameObjectWithTag("BulletTracer");
        lineObject.GetComponent<Renderer>().enabled = true;
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, firePoint.position);
        lineObject.SetActive(true);

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider.gameObject.layer != 8)
            {
                hitInfo.collider.transform.localScale = Vector3.zero;
                //Destroy(hitInfo.collider.gameObject);
            }
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        lineRenderer.SetPosition(1, ray.direction * 1000);
        StartCoroutine(HideRendererAfterSeconds(lineObject, 0.004f));
        StartCoroutine(muzzleFlashForSeconds(0.009f));
    }

    private IEnumerator muzzleFlashForSeconds(float secondsToFlash)
    {
        GameObject muzzleFlashObject = GameObject.FindGameObjectWithTag("MuzzleFlash");
        Light light = muzzleFlashObject.GetComponent<Light>();
        Debug.Log(light);
        light.enabled = true;
        yield return new WaitForSeconds(secondsToFlash);
        light.enabled = false;
    }

    private IEnumerator HideRendererAfterSeconds(GameObject gameObject, float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
