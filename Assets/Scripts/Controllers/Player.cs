﻿
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public bool Equiped;

    private GameObject palmObject;
    private Animator animator;

    private void Start()
    {
        /*        var pistolPrefab = Resources.Load<GameObject>("Models/GunPack/FBX/AssaultRifle2_1");
                var pistolPosition = new Vector3(0.00045f, 0.00315f, -0.0007f);
                var pistolRotation = new Vector3(-6.352f, 82.234f, -88.08f);
                var pistolScale = new Vector3(0.4f, 0.4f, 0.4f);
                var palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
                pistolPrefab.transform.localPosition = pistolPosition;
                pistolPrefab.transform.localEulerAngles = pistolRotation;
                pistolPrefab.transform.localScale = pistolScale;
                Instantiate(pistolPrefab, palmObject.transform);*/
        animator = GetComponent<Animator>();
        palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        inventory.ItemAdded += Inventory_ItemAdded;
        inventory.ItemEquipped += Inventory_ItemEquipped;
        inventory.ItemUnequipped += Inventory_ItemUnequipped;
    }

    private void Inventory_ItemAdded(object sender, InventoryEventArgs args)
    {
        Debug.Log("Inventory_ItemAdded");
        /*        IInventoryItem item = args.Item;
                GameObject goItem = (item as MonoBehaviour).gameObject;
                goItem.SetActive(true);
                goItem.transform.parent = palmObject.transform;
                var position = new Vector3(0.00045f, 0.00315f, -0.0007f);
                var rotation = new Vector3(-6.352f, 82.234f, -88.08f);
                goItem.transform.localPosition = position;
                goItem.transform.localEulerAngles = rotation;*/

    }

    private void Inventory_ItemEquipped(object sender, InventoryEventArgs args)
    {
        Debug.Log("Inventory_ItemEquiped");
        IInventoryItem item = args.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = palmObject.transform;
        var position = new Vector3(0.00045f, 0.00315f, -0.0007f);
        var rotation = new Vector3(-6.352f, 82.234f, -88.08f);
        goItem.transform.localPosition = position;
        goItem.transform.localEulerAngles = rotation;
        animator.SetBool("ItemEquipped", true);
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEventArgs args)
    {
        IInventoryItem item = args.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(false);
        animator.SetBool("ItemEquipped", false);
    }

        private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInventoryItem item = collision.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }
}
