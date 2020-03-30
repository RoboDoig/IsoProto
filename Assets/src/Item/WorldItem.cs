using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WorldItem
{
    public ItemType itemType;
    public int amount;

    public static List<string> itemTypes = new List<string>();

    public WorldItem(ItemType _itemType, int _amount)
    {
        itemType = _itemType;
        amount = _amount;

        if (!itemTypes.Contains(itemType.type.ToLower()))
        {
            itemTypes.Add(itemType.type);
        }
    }
}
