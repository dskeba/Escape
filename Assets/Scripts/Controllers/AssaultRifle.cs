
using UnityEngine;

public class AssaultRifle : Gun
{
    private Sprite _image;

    public override string Name {
        get
        {
            return "Assault Rifle";
        }
    }

    public override Sprite Image
    {
        get
        {
            return _image;
        }
    }

    protected override void OnAwake() {
        base.fireRate = 0.2f;
        _image = Resources.Load<Sprite>("Sprites/Assault_Rifle");
    }

    protected override void OnStart() { }

    protected override void OnFixedUpdate() { }

    protected override void OnUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/assault_rifle_shot", 0.5f);
    }
}