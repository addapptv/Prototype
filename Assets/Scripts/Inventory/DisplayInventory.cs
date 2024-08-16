using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


//TO DO
// - Fix crashing when dragging empty slots
// - Fix crash when dragging objects onto player collider


public class DisplayInventory : MonoBehaviour
{
    private MouseItem _mouseItem = new MouseItem();
    private GameObject _droppedItemPrefab;
    private Transform _droppedItemParent;
    [HideInInspector]
    public bool actionsComplete = true;

    public GameObject slotPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots();
    }

    void Update()
    {
        UpdateSlots();
    }
    public void CreateSlots()
    {

        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }                                                                           
    }
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].itemIcon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.item.Name;
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry
        {
            eventID = type
        };
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        _mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
            _mouseItem.hoverItem = itemsDisplayed[obj];
    }
    public void OnExit(GameObject obj)
    {
        _mouseItem.hoverObj = null;
        _mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].itemIcon;
            img.raycastTarget = false;
        }
        _mouseItem.obj = mouseObject;
        _mouseItem.item = itemsDisplayed[obj];
        actionsComplete = false;
    }
    public void OnDragEnd(GameObject obj)
    {
        var prefab = inventory.database.GetItem[itemsDisplayed[obj].ID].itemPrefab;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (_mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[_mouseItem.hoverObj]);
            Destroy(_mouseItem.obj);
            _mouseItem.item = null;
            actionsComplete = true;
        }
        else if (Physics.Raycast(ray, out hit, 10))
        {
            _droppedItemParent = FindObjectOfType<LocationManager>().GetComponent<LocationManager>().droppedItemParent;
            _droppedItemPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            _droppedItemPrefab.transform.SetParent(_droppedItemParent, false);
            _droppedItemPrefab.transform.position = hit.point;

            inventory.RemoveItem(itemsDisplayed[obj].item);
            Destroy(_mouseItem.obj);
            _mouseItem.item = null;
            actionsComplete = true;
        }
        else
        {
            Destroy(_mouseItem.obj);
            _mouseItem.item = null;
            actionsComplete = true;
        }
    }

    public void OnDrag(GameObject obj)
    {
        if (_mouseItem.obj != null)
            _mouseItem.obj.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}