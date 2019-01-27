using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour
{

    public List<Item> itemList = new List<Item>();

    public void AddItem(Item item)
    {
        item.gameObject.transform.SetParent(transform);
        itemList.Add(item);
    }
    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item)) {
            itemList.Remove(item);
        }
        Destroy(item.gameObject);
    }



}
