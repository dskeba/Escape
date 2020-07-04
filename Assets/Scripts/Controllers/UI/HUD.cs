
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject MessagePanel;

    private Text textComponent;

    void Start()
    {
        Inventory.ItemAdded += Inventory_ItemAdded;
        Inventory.ItemDropped += Inventory_ItemDropped;

        textComponent = MessagePanel.transform.Find("Text").GetComponent<Text>();
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
        MessagePanel.SetActive(true);
    }

    public void HideMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
