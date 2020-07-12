
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private HUD _hud;
    private GameObject _palmObject;
    private Animator _animator;
    private List<IInventoryItem> _pickupItems;

    private void Start()
    {
        _pickupItems = new List<IInventoryItem>();
        _animator = GetComponent<Animator>();
        _palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        _inventory.ItemEquipped += Inventory_ItemEquipped;
        _inventory.ItemUnequipped += Inventory_ItemUnequipped;
        _inventory.ItemDropped += Inventory_ItemDropped;
        SoundManager.Instance.Play(MixerGroup.Music, "Sounds/creeprs", 0.5f);
    }

    private void Inventory_ItemEquipped(object sender, InventoryEvent inventoryEvent)
    {
        IInventoryItem item = inventoryEvent.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = _palmObject.transform;
        var position = new Vector3(0.00045f, 0.00315f, -0.0007f);
        var rotation = new Vector3(-6.352f, 82.234f, -88.08f);
        goItem.transform.localPosition = position;
        goItem.transform.localEulerAngles = rotation;
        SetItemEquipped(item, true);
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEvent inventoryEvent)
    {
        IInventoryItem item = inventoryEvent.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(false);
        SetItemEquipped(item, false);
    }

    private void Inventory_ItemDropped(object sender, InventoryEvent inventoryEvent)
    {
        IInventoryItem item = inventoryEvent.Item;
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.transform.parent = transform.parent;
        SetItemEquipped(item, false);
    }

    private void SetItemEquipped(IInventoryItem item, bool equipped)
    {
        if (item.GetType().IsSubclassOf(typeof(Gun)))
        {
            _animator.SetBool("GunEquipped", equipped);
        }
        else if (item.GetType().IsSubclassOf(typeof(Consumable)))
        {
            _animator.SetBool("ConsumableEquipped", equipped);
        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (_pickupItems.Count > 0 && Input.GetKeyDown("f"))
        {
            _inventory.AddItem(_pickupItems[0]);
            _pickupItems.RemoveAt(0);
            if (_pickupItems.Count == 0)
            {
                _hud.HideMessagePanel();
            } 
            else
            {
                _hud.ShowMessagePanel("Press F To Pickup " + _pickupItems[0].Name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if (_pickupItems.Count == 0)
            {
                _hud.ShowMessagePanel("Press F To Pickup " + item.Name);
            }
            _pickupItems.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Debug.Log("exit " + item.Name);
            _pickupItems.Remove(item);
            if (_pickupItems.Count == 0)
            {
                _hud.HideMessagePanel();
            }
            else
            {
                _hud.ShowMessagePanel("Press F To Pickup " + _pickupItems[0].Name);
            }
        }
    }
}
