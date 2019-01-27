using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = GameObject.Find("ItemParent");
        if (parent != null)
        {
            ItemParent itemParent = parent.GetComponent<ItemParent>();
            if (itemParent != null)
            {
                Item[] items = GetComponentsInChildren<Item>();
                foreach (Item i in items)
                {
                    itemParent.AddItem(i);
                }
            }
        }
    }
}
