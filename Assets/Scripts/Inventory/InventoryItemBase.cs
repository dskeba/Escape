using UnityEngine;

public abstract class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public InventoryItemBase() { }

    public int Index { get; set; }
    public string Name { get; set; }
    public Sprite Image { get; set; }
    public bool IsEquipped { get; set; }

    protected abstract void OnBaseAwake();
    protected abstract void OnBaseStart();
    protected abstract void OnBaseFixedUpdate();
    protected abstract void OnBaseUpdate();
    protected abstract void OnBaseDrop();

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
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5.0f, ForceMode.Impulse);

        OnBaseDrop();
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
        Collider collider = GetComponent<Collider>();
        if (collider) {
            collider.enabled = false;
        }
        Destroy(gameObject.GetComponent<Rigidbody>());
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/backpack_item", 0.5f);
    }
}
