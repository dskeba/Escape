
using UnityEngine;

public class AssaultRifle : Gun, IInventoryItem
{

    public string Name { get; set; }
    public Sprite Image { get; set; }
    public override bool Equiped { get; set; }

    protected override void OnAwake() {
        base.fireRate = 0.2f;
        Equiped = false;
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
        Equiped = true;
        gameObject.SetActive(false);
    }
}