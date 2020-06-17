using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float fireRate = 1f;

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
                //FireGun();
            }
        }
    }

    private void FireGun()
    {
        Debug.Log("FIRE");
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Destroy(hitInfo.collider.gameObject);
        }
        //Debug.DrawRay()
    }
}
