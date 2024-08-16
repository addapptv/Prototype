using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Food,
    Collectible,
    Tent,
    Default
}

public enum ItemAttribs
{
    Warmth,
    Speed,
    RestoreEnergy,
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public string itemName;
    public GameObject itemPrefab;
    public InventoryObject targetInventory;
    public Sprite itemIcon;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public float size;
    public float value;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}


[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].value)
            {
                attribute = item.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public ItemAttribs attribute;
    public int value;

    public ItemBuff(int _value)
    {
        _value = value;
    }

}