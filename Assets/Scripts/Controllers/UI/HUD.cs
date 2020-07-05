
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private GameObject _messagePanel;
    private Text textComponent;

    private void Start()
    {
        _inventory.ItemAdded += Inventory_ItemAdded;
        _inventory.ItemDropped += Inventory_ItemDropped;

        textComponent = _messagePanel.transform.Find("Text").GetComponent<Text>();
    }

    private void Inventory_ItemAdded(object sender, InventoryEventArgs args)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = args.Item.Image;
                return;
            }
        }
    }

    private void Inventory_ItemDropped(object sender, InventoryEventArgs args)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (image.enabled && image.sprite == args.Item.Image)
            {
                image.enabled = false;
            }
        }
    }

    public void ShowMessagePanel(string text)
    {
        textComponent.text = text;
        _messagePanel.SetActive(true);
    }

    public void HideMessagePanel()
    {
        _messagePanel.SetActive(false);
    }
}
