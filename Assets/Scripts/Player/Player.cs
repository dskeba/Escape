
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private AmmoSystem _ammoSystem;
    [SerializeField]
    private HUD _hud;
    private GameObject _palmObject;
    private Animator _animator;
    private List<IPickupObject> _pickupObjects;

    private void Start()
    {
        _pickupObjects = new List<IPickupObject>();
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
        if (item is Gun)
        {
            _animator.SetBool("GunEquipped", equipped);
        }
        else if (item is Consumable)
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

        if (_pickupObjects.Count > 0 && Input.GetKeyDown("f"))
        {
            if (_pickupObjects[0] is IInventoryItem)
            {
                Debug.Log("inventory item");
                _inventory.AddItem((IInventoryItem)_pickupObjects[0]);
            }
            else if (_pickupObjects[0] is IAmmo)
            {
                _ammoSystem.AddAmmo((IAmmo)_pickupObjects[0]);
            }
            _pickupObjects.RemoveAt(0);
            if (_pickupObjects.Count == 0)
            {
                _hud.HideMessagePanel();
            } 
            else
            {
                _hud.ShowMessagePanel("Press F To Pickup " + _pickupObjects[0].Name);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickupObject pickupObject = other.GetComponent<IPickupObject>();
        if (pickupObject != null)
        {
            if (_pickupObjects.Count == 0)
            {
                _hud.ShowMessagePanel("Press F To Pickup " + pickupObject.Name);
            }
            _pickupObjects.Add(pickupObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPickupObject pickupObject = other.GetComponent<IPickupObject>();
        if (pickupObject != null)
        {
            Debug.Log("exit " + pickupObject.Name);
            _pickupObjects.Remove(pickupObject);
            if (_pickupObjects.Count == 0)
            {
                _hud.HideMessagePanel();
            }
            else
            {
                _hud.ShowMessagePanel("Press F To Pickup " + _pickupObjects[0].Name);
            }
        }
    }
}
