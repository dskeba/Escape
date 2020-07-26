using UnityEngine;
using System.Collections;

public abstract class AmmoStackBase : MonoBehaviour, IAmmoStack
{
    public AmmoStackBase() { }

    public string Name { get; set; }
    public int Quantity { get; set; }
    public IAmmoType AmmoType { get; set; }

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
        OnBaseUpdate();
    }

    private void FixedUpdate()
    {
        OnBaseFixedUpdate();
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
        Collider collider = GetComponent<Collider>();
        if (collider)
        {
            collider.enabled = false;
        }
        Destroy(gameObject.GetComponent<Rigidbody>());
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/backpack_item", 0.5f);
    }
}
