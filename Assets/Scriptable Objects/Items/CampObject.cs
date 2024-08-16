using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Camp Object", menuName = "Inventory System/Items/Camp")]

public class CampObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Camp;
    }
}
