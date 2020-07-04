
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory;
    public HUD HUD;

    private GameObject palmObject;
    private Animator animator;
    private List<IInventoryItem> pickupItems;

    private void Start()
    {
        pickupItems = new List<IInventoryItem>();

        animator = GetComponent<Animator>();
        palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        Inventory.ItemEquipped += Inventory_ItemEquipped;
        Inventory.ItemUnequipped += Inventory_ItemUnequipped;
        Inventory.ItemDropped += Inventory_ItemDropped;

        SoundManager.Instance.Play(MixerGroup.Music, "Sounds/creeprs", 0.5f);
    }

    private void Inventory_ItemEquipped(object sender, InventoryEventArgs args)
    {
        IInventoryItem item = args.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = palmObject.transform;
        var position = new Vector3(0.00045f, 0.00315f, -0.0007f);
        var rotation = new Vector3(-6.352f, 82.234f, -88.08f);
        goItem.transform.localPosition = position;
        goItem.transform.localEulerAngles = rotation;
        SetItemEquipped(item, true);
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEventArgs args)
    {
        IInventoryItem item = args.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(false);
        SetItemEquipped(item, false);
    }

    private void Inventory_ItemDropped(object sender, InventoryEventArgs args)
    {
        IInventoryItem item = args.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.transform.parent = transform.parent;
        SetItemEquipped(item, false);
    }

    private void SetItemEquipped(IInventoryItem item, bool equipped)
    {
        if (item.ItemType == InventoryItemType.Gun)
        {
            animator.SetBool("GunEquipped", equipped);
        }
        else if (item.ItemType == InventoryItemType.Consumable)
        {
            animator.SetBool("ConsumableEquipped", equipped);
        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (pickupItems.Count > 0 && Input.GetKeyDown("f"))
        {
            Inventory.AddItem(pickupItems[0]);
            pickupItems.RemoveAt(0);
            if (pickupItems.Count == 0)
            {
                HUD.HideMessagePanel();
            } 
            else
            {
                HUD.ShowMessagePanel("Press F To Pickup " + pickupItems[0].Name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (pickupItems.Count == 0)
            {
                HUD.ShowMessagePanel("Press F To Pickup " + item.Name);
            }
            pickupItems.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Debug.Log("exit " + item.Name);
            pickupItems.Remove(item);
            if (pickupItems.Count == 0)
            {
                HUD.HideMessagePanel();
            }
            else
            {
                HUD.ShowMessagePanel("Press F To Pickup " + pickupItems[0].Name);
            }
        }
    }
}
