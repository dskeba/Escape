﻿using UnityEngine;

public class PlayerHealth : HealthBase
{
    private GameObject bloodObject;

    public PlayerHealth()
    {
        base.MaxHealth = 100;
        base.CurrentHealth = base.MaxHealth;
    }

    private void Start()
    {
        bloodObject = Resources.Load<GameObject>("Prefabs/Blood");
    }

    protected override void OnBaseDamage(int damage, Vector3 position)
    {
        Debug.Log("player has taken " + damage + " damage");
        Instantiate(bloodObject, position, Quaternion.identity);
    }

    protected override void OnBaseHeal(int damage, Vector3 position)
    {
        
    }

    protected override void OnBaseDie()
    {
        
    }

    protected override void OnBaseRevive() { }
}

