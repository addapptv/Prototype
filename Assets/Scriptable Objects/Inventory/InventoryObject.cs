using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;


    public void AddItem(Item _item)
    {
        SetEmptySlot(_item);
        return;
    }

    public InventorySlot SetEmptySlot(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= -1) 
            {
                Container.Items[i].UpdateSlot(_item.Id, _item);
                return Container.Items[i];
            }
        }
        //Set up functionality for when inventory full//
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item);
        item2.UpdateSlot(item1.ID, item1.item);
        item1.UpdateSlot(temp.ID, temp.item);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null);
            }
        }
    }


    [ContextMenu("Save")]
    public void SaveInventory()
    {
        //JSON SAVE SYSTEM//

        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();

        //BINARY SAVE SYSTEM//

        /*IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();*/
    }

    [ContextMenu("Load")]
    public void LoadInventory()
    {
        //JSON LOAD SYSTEM//

        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();

        }

        //BINARY LOAD SYSTEM//

        /*IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
        Inventory newContainer = (Inventory)formatter.Deserialize(stream);
        for (int i = 0; i < Container.Items.Length; i++)
        {
            Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
        }
        stream.Close();*/

    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[32];
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;

    public InventorySlot()
    {
        ID = -1;
        item = null;
    }
    public InventorySlot(int _id, Item _item)
    {
        ID = _id;
        item = _item;
    }
    public void UpdateSlot(int _id, Item _item)
    {
        ID = _id;
        item = _item;
    }

}

