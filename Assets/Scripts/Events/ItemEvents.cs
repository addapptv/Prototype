using System;

public class ItemEvents
{
    public event Action<string> OnCollectItem;
    public void CollectItem(string itemName)
    {
        if (OnCollectItem != null)
        {
            OnCollectItem(itemName);
        }
    }
}
