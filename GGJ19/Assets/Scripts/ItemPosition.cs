using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPosition : MonoBehaviour
{
    private DraggableItem DI;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DI = GetComponent<DraggableItem>();
    }

    // Update is called once per frame


    public bool InSuitcase()
    {
       if (GetComponent<PolygonCollider2D>().IsTouching(GameObject.Find("suitcase/Inside").GetComponent<PolygonCollider2D>()) && 
            !(GetComponent<PolygonCollider2D>()
                      .IsTouching(GameObject.Find("suitcase/Outside").GetComponent<PolygonCollider2D>())))
        {
            return true;
        }
        else
        {
            return false;
        }



    }


}
