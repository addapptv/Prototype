using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tent Object", menuName = "Inventory System/Items/Tent")]

public class TentObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Tent;
    }
}
