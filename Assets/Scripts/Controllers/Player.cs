
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private void Start()
    {
        var pistolPrefab = Resources.Load<GameObject>("Models/GunPack/FBX/AssaultRifle2_1");
        var pistolPosition = new Vector3(0.00045f, 0.00315f, -0.0007f);
        var pistolRotation = new Vector3(-6.352f, 82.234f, -88.08f);
        var pistolScale = new Vector3(0.4f, 0.4f, 0.4f);
        var palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        pistolPrefab.transform.localPosition = pistolPosition;
        pistolPrefab.transform.localEulerAngles = pistolRotation;
        pistolPrefab.transform.localScale = pistolScale;
        Instantiate(pistolPrefab, palmObject.transform);
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
