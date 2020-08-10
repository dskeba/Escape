
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

    private Text _messagePanelText;
    private Text _pistolAmmoText;
    private Text _shotgunAmmoText;
    private Text _assaultRifleAmmoText;
    private Text _currentAmmoText;
    private Text _totalAmmoText;
    private Image _ammoImage; 

    private void Start()
    {
        Inventory.Instance.ItemAdded += Inventory_ItemAdded;
        Inventory.Instance.ItemDropped += Inventory_ItemDropped;
        Inventory.Instance.ItemEquipped += Inventory_ItemEquipped;
        Inventory.Instance.ItemUnequipped += Inventory_ItemUnequipped;

        AmmoSupply.Instance.AmmoAdded += AmmoSupply_AmmoAdded;
        AmmoSupply.Instance.AmmoRemoved += AmmoSupply_AmmoRemoved;

        _messagePanelText = _messagePanel.transform.Find("Text").GetComponent<Text>();
        _pistolAmmoText = _ammoPanel.transform.Find("PistolText").GetComponent<Text>();
        _shotgunAmmoText = _ammoPanel.transform.Find("ShotgunText").GetComponent<Text>();
        _assaultRifleAmmoText = _ammoPanel.transform.Find("AssaultRifleText").GetComponent<Text>();
        _currentAmmoText = _gunPanel.transform.Find("CurrentAmmo").GetComponent<Text>();
        _totalAmmoText = _gunPanel.transform.Find("TotalAmmo").GetComponent<Text>();
        _ammoImage = _gunPanel.transform.Find("AmmoImage").GetComponent<Image>();
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
        if (inventoryEvent.Item is Gun)
        {
            ShowGunPanel();
            (inventoryEvent.Item as Gun).Reload += Gun_Reload;
            (inventoryEvent.Item as Gun).Shoot += Gun_Shoot;
            UpdateGunText((inventoryEvent.Item as Gun));
        }
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetComponent<Image>();
        image.color = UNEQUIPPED_COLOR;
        if (inventoryEvent.Item is Gun)
        {
            HideGunPanel();
            (inventoryEvent.Item as Gun).Reload -= Gun_Reload;
            (inventoryEvent.Item as Gun).Shoot -= Gun_Shoot;
            UpdateGunText((inventoryEvent.Item as Gun));
        }
    }

    private void Gun_Reload(object sender, GunEvent gunEvent)
    {
        UpdateGunText(gunEvent.Gun);
    }

    private void Gun_Shoot(object sender, GunEvent gunEvent)
    {
        UpdateGunText(gunEvent.Gun);
    }

    private void AmmoSupply_AmmoAdded(object sender, AmmoSupplyEvent ammoSupplyEvent)
    {
        UpdateAmmoText(ammoSupplyEvent.Name);
    }

    private void AmmoSupply_AmmoRemoved(object sender, AmmoSupplyEvent ammoSupplyEvent)
    {
        UpdateAmmoText(ammoSupplyEvent.Name);
    }

    private void UpdateGunText(Gun gun)
    {
        _currentAmmoText.text = gun.AmmoLoaded.ToString();
        _totalAmmoText.text = AmmoSupply.Instance.GetQuantity(gun.AmmoType.Name).ToString();
        _ammoImage.sprite = gun.AmmoType.Image;
    }

    private void UpdateAmmoText(AmmoName name)
    {
        if (name == AmmoName.Pistol)
        {
            _pistolAmmoText.text = AmmoSupply.Instance.GetQuantity(AmmoName.Pistol).ToString();
        }
        else if (name == AmmoName.Shotgun)
        {
            _shotgunAmmoText.text = AmmoSupply.Instance.GetQuantity(AmmoName.Shotgun).ToString();
        }
        else if (name == AmmoName.AssaultRifle)
        {
            _assaultRifleAmmoText.text = AmmoSupply.Instance.GetQuantity(AmmoName.AssaultRifle).ToString();
        }
    }

    public void ShowMessagePanel(string text)
    {
        _messagePanelText.text = text;
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
