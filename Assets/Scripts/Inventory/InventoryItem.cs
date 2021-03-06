﻿using UnityEngine;

public interface IInventoryItem : IPickupObject
{
    int Index { get; set; }
    Sprite Image { get; set; }
    bool IsEquipped { get; set; }

    void OnDrop();
}