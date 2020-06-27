using UnityEngine;
using System.Collections;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name { get; }
    public virtual Sprite Image { get; }
    public bool IsEquipped { get; set; }

    public void OnDrop()
    {
        gameObject.SetActive(true);
        Collider collider = GetComponent<Collider>();
        if (collider)
        {
            collider.enabled = true;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
        Collider collider = GetComponent<Collider>();
        if (collider) {
            collider.enabled = false;
        }
    }
}
