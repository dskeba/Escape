
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static Color EQUIPPED_COLOR = new Color(1, 1, 1, 0.5f);
    private static Color UNEQUIPPED_COLOR = new Color(1, 1, 1, 0.25f);

    [SerializeField]
    private Transform _inventoryPanel;
    [SerializeField]
    private GameObject _messagePanel;
    [SerializeField]
    private GameObject _gunPanel;
    [SerializeField]
    private GameObject _ammoPanel;

    private Text _textComponent;
    private Text _pistolAmmoText;
    private Text _assaultRifleAmmoText;
    private Text _currentAmmoText;
    private Text _totalAmmoText;

    private void Start()
    {
        Inventory.Instance.ItemAdded += Inventory_ItemAdded;
        Inventory.Instance.ItemDropped += Inventory_ItemDropped;
        Inventory.Instance.ItemEquipped += Inventory_ItemEquipped;
        Inventory.Instance.ItemUnequipped += Inventory_ItemUnequipped;

        AmmoSystem.Instance.AmmoAdded += AmmoSystem_AmmoAdded;
        AmmoSystem.Instance.AmmoUsed += AmmoSystem_AmmoUsed;

        _textComponent = _messagePanel.transform.Find("Text").GetComponent<Text>();
        _pistolAmmoText = _ammoPanel.transform.Find("PistolText").GetComponent<Text>();
        _assaultRifleAmmoText = _ammoPanel.transform.Find("AssaultRifleText").GetComponent<Text>();
        _currentAmmoText = _gunPanel.transform.Find("CurrentAmmo").GetComponent<Text>();
        _totalAmmoText = _gunPanel.transform.Find("TotalAmmo").GetComponent<Text>();
    }

    private void Inventory_ItemAdded(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
        image.enabled = true;
        image.sprite = inventoryEvent.Item.Image;
    }

    private void Inventory_ItemDropped(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
        image.enabled = false;
    }

    private void Inventory_ItemEquipped(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetComponent<Image>();
        image.color = EQUIPPED_COLOR;

        ShowGunPanel();
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetComponent<Image>();
        image.color = UNEQUIPPED_COLOR;

        HideGunPanel();
    }

    private void AmmoSystem_AmmoAdded(object sender, AmmoSystemEvent ammoSystemEvent)
    {
        UpdateAmmoText(ammoSystemEvent.Type);
    }

    private void AmmoSystem_AmmoUsed(object sender, AmmoSystemEvent ammoSystemEvent)
    {
        UpdateAmmoText(ammoSystemEvent.Type);
    }

    private void UpdateAmmoText(AmmoType type)
    {
        if (type == AmmoType.Pistol)
        {
            _pistolAmmoText.text = AmmoSystem.Instance.GetQuantity(AmmoType.Pistol).ToString();
        }
        else if (type == AmmoType.AssaultRifle)
        {
            _assaultRifleAmmoText.text = AmmoSystem.Instance.GetQuantity(AmmoType.AssaultRifle).ToString();
        }
    }

    public void ShowMessagePanel(string text)
    {
        _textComponent.text = text;
        _messagePanel.SetActive(true);
    }

    public void HideMessagePanel()
    {
        _messagePanel.SetActive(false);
    }

    public void ShowGunPanel()
    {
        _gunPanel.SetActive(true);
    }

    public void HideGunPanel()
    {
        _gunPanel.SetActive(false);
    }
}
