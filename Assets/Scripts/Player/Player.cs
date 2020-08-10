
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private HUD _hud;
    private GameObject _palmObject;
    private Animator _animator;
    private List<IPickupObject> _pickupObjects;

    private void Start()
    {
        _pickupObjects = new List<IPickupObject>();
        _animator = GetComponent<Animator>();
        _palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        Inventory.Instance.ItemEquipped += Inventory_ItemEquipped;
        Inventory.Instance.ItemUnequipped += Inventory_ItemUnequipped;
        Inventory.Instance.ItemDropped += Inventory_ItemDropped;

        var go = GameObject.FindGameObjectWithTag("HUD");
        _hud = go.GetComponent<HUD>();
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
        Debug.Log(goItem.transform.parent);
        SetItemEquipped(item, false);
    }

    private void SetItemEquipped(IInventoryItem item, bool equipped)
    {
        if (item is Gun)
        {
            _animator.SetBool("GunEquipped", equipped);
            SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pickup", 0.25f);
        }
        else if (item is Consumable)
        {
            _animator.SetBool("ConsumableEquipped", equipped);
            SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pickup", 0.25f);
        }
    }

    private void Update()
    {
        CheckQuit();
        CheckPickupObject();
    }

    private void CheckQuit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void CheckPickupObject()
    {
        if (_pickupObjects.Count > 0 && Input.GetKeyDown("f"))
        {
            IPickupObject pickupObject = _pickupObjects[0];
            _pickupObjects.RemoveAt(0);
            if (pickupObject is IInventoryItem)
            {
                Inventory.Instance.AddItem((IInventoryItem)pickupObject);
            }
            else if (pickupObject is IAmmoStack)
            {
                AmmoSupply.Instance.AddAmmo((IAmmoStack)pickupObject);
            }
            if (_pickupObjects.Count == 0)
            {
                _hud.HideMessagePanel();
            }
            else
            {
                ShowPickupMessage(pickupObject);
            }
        }
    }

    private void ShowPickupMessage(IPickupObject pickupObject)
    {
        _hud.ShowMessagePanel("Press F To Pickup " + pickupObject.Name);
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickupObject pickupObject = other.GetComponent<IPickupObject>();
        if (pickupObject != null)
        {
            if (_pickupObjects.Count == 0)
            {
                ShowPickupMessage(pickupObject);
            }
            _pickupObjects.Add(pickupObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPickupObject pickupObject = other.GetComponent<IPickupObject>();
        if (pickupObject != null)
        {
            _pickupObjects.Remove(pickupObject);
            if (_pickupObjects.Count == 0)
            {
                _hud.HideMessagePanel();
            }
            else
            {
                ShowPickupMessage(pickupObject);
            }
        }
    }
}
