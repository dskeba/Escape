
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    private GameObject palmObject;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        palmObject = GameObject.FindGameObjectWithTag("PlayerPalm");
        inventory.ItemEquipped += Inventory_ItemEquipped;
        inventory.ItemUnequipped += Inventory_ItemUnequipped;
        inventory.ItemDropped += Inventory_ItemDropped;

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
        goItem.transform.position = transform.position + new Vector3(2f, 0f, 2f);
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
