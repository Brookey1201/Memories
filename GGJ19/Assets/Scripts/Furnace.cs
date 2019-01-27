using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    public GameManager game;

    public Slider meter;

    public float Warmth;//{set;get;}
    public float light;
    
    public SpriteRenderer roomLight;
    public SpriteRenderer roomDarkness;
    
    public ItemParent itemParent;
    List<Item> burningItems = new List<Item>();


    void Start(){
        Warmth = 50.0f;
        light=Warmth;
    }

    void Update()
    {
        if(game.playing){
            meter.value = Warmth;
            light = Mathf.Clamp((light+Random.RandomRange(-0.5f,0.5f)),Warmth-10,Warmth+10);
            roomLight.color = new Color(1,1,1,light/100.0f);
            roomDarkness.color = new Color(0,0,0,(100-light)/100.0f);
            Warmth-=Time.deltaTime;
            Warmth = Mathf.Clamp(Warmth,0,100);

            for(int i = burningItems.Count-1; i>=0;i--){
                if(burningItems[i].burnLeft<=0){
                    Item toBeRemoved = burningItems[i];
                    burningItems.RemoveAt(i);
                    itemParent.RemoveItem(toBeRemoved);
                }
            }

        }
    }

    public void Burn(Item i){

        foreach(FamilyMember f in i.familyMembers){
            f.memoryValue-=i.memoryValue;
        }
        Warmth +=i.flamability;

        if(burningItems.Count>2){
            Item old = burningItems[0];
            burningItems.RemoveAt(0);
            itemParent.RemoveItem(old);
        }
        burningItems.Add(i);
        i.gameObject.transform.SetParent(transform);
        i.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        i.gameObject.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        i.gameObject.transform.localPosition = Vector3.zero;
        i.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        i.gameObject.GetComponent<PolygonCollider2D>().enabled=false;
        i.burning = true;
        i.gameObject.GetComponent<DraggableItem>().enabled=false;

        

    }

}
