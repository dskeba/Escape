
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static Color EQUIPPED_COLOR = new Color(1, 1, 1, 0.5f);
    private static Color UNEQUIPPED_COLOR = new Color(1, 1, 1, 0.25f);

    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private Transform _inventoryPanel;
    [SerializeField]
    private GameObject _messagePanel;
    [SerializeField]
    private GameObject _ammoPanel;
    private Text _textComponent;

    private void Start()
    {
        _inventory.ItemAdded += Inventory_ItemAdded;
        _inventory.ItemDropped += Inventory_ItemDropped;
        _inventory.ItemEquipped += Inventory_ItemEquipped;
        _inventory.ItemUnequipped += Inventory_ItemUnequipped;

        _textComponent = _messagePanel.transform.Find("Text").GetComponent<Text>();
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

        ShowAmmoPanel();
    }

    private void Inventory_ItemUnequipped(object sender, InventoryEvent inventoryEvent)
    {
        Transform slot = _inventoryPanel.GetChild(inventoryEvent.Item.Index);
        Image image = slot.GetChild(0).GetComponent<Image>();
        image.color = UNEQUIPPED_COLOR;

        HideAmmoPanel();
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

    public void ShowAmmoPanel()
    {
        _ammoPanel.SetActive(true);
    }

    public void HideAmmoPanel()
    {
        _ammoPanel.SetActive(false);
    }
}
