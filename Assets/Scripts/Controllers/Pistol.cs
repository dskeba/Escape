
using UnityEngine;

public class Pistol : Gun
{
    private Sprite _image;

    public override string Name
    {
        get
        {
            return "Pistol";
        }
    }

    public override Sprite Image
    {
        get
        {
            return _image;
        }
    }

    protected override void OnAwake()
    {
        base.fireRate = 0.2f;
        base.autoFire = false;
        _image = Resources.Load<Sprite>("Sprites/Pistol");
    }

    protected override void OnStart() { }

    protected override void OnFixedUpdate() { }

    protected override void OnUpdate() { }

    protected override void OnFireGun()
    {
        SoundManager.Instance.Play(MixerGroup.Sound, "Sounds/pistol_shot", 0.5f);
    }
}
