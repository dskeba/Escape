using UnityEngine;
using System.Collections;

public abstract class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name { get; }
    public virtual Sprite Image { get; }
    public bool IsEquipped { get; set; }
    public InventoryItemType ItemType { get; set; }

    protected abstract void OnBaseAwake();
    protected abstract void OnBaseStart();
    protected abstract void OnBaseFixedUpdate();
    protected abstract void OnBaseUpdate();

    private void Awake()
    {
        OnBaseAwake();
    }

    private void Start()
    {
        OnBaseStart();
    }

    private void Update()
    {
        if (!IsEquipped) { return; }
        OnBaseUpdate();
    }

    private void FixedUpdate()
    {
        if (!IsEquipped) { return; }
        OnBaseFixedUpdate();
    }

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
