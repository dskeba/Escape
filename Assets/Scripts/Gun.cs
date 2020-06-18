using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("FIRE");
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            hitInfo.collider.transform.localScale = Vector3.zero;
            //Destroy(hitInfo.collider.gameObject);
        }
    }
}
