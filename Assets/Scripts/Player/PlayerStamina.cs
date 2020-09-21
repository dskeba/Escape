using UnityEngine;

public class PlayerStamina : StaminaBase
{
    private GameObject staminaObject;

    public PlayerStamina()
    {
        base.MaxStamina = 15.0f;
        base.CurrentStamina = base.MaxStamina;
    }

    private void Start()
    {
    }

    protected override void OnBaseRemoveStamina(float amount, Vector3 position)
    {
    }

    protected override void OnBaseAddStamina(float amount, Vector3 position)
    {

    }
}

