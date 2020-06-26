
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultRifle : Gun, IInventoryItem
{

    public string Name { get; set; }
    public Sprite Image
    {
        get
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Assault_Rifle");
            return sprite;
        }
    }
    public override bool Equipped { get; set; }

    protected override void OnAwake() {
        base.fireRate = 0.2f;
        Equipped = false;
    }

    protected override void OnStart() { }

    protected override void OnFixedUpdate() { }

    protected override void OnUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
    }

    void IInventoryItem.OnPickup()
    {
        Equipped = true;
        gameObject.SetActive(false);
    }

    void IInventoryItem.OnDrop()
    {
        Equipped = false;
        gameObject.SetActive(true);
    }
}