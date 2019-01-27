using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    
    public string itemName;
    public float memoryValue;
    public float flamability;
    public List<int> familyID;
    public List<FamilyMember> familyMembers;
    public Sprite mainImage;
    public List<string> dialogue;

    private ItemPosition itemPosition;

    float clickSpeed = 0.3f;
    float mouseDownTime;

    bool inSuitcaseLastFrame = false;

    public bool burning = false;
    public float burnLeft = 10.0f;

    public static bool canClick() {
		PointerEventData pos = new PointerEventData(EventSystem.current);
		pos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pos, results);
		return !(results.Count > 0);
	}

    private void OnMouseDown(){
        if(canClick()&&!burning){
            mouseDownTime = Time.time;
        }
    }

    private void OnMouseUp(){
        if(canClick()&&!burning){
            float currentTime = Time.time;

            if((currentTime-mouseDownTime)<clickSpeed){
                DetailView detailView = GameObject.Find("DetailView").GetComponent<DetailView>();
                if(detailView!=null){
                    detailView.OpenDetail(mainImage,dialogue);
                }
                foreach(FamilyMember f in familyMembers){
                    if(f.discovered==false){
                        f.Discover();
                    }
                }
            }else{
                Furnace furnace = GameObject.Find("Furnace").GetComponent<Furnace>();
                if(furnace!=null){
                    if(Vector2.Distance(transform.localPosition,furnace.transform.localPosition)<1.5f){
                        furnace.Burn(this);
                    }
                }
            }

            

        }
    }
    
    void Start(){
        itemPosition = GetComponent<ItemPosition>();
        GameManager game = GameObject.Find("Main").GetComponent<GameManager>();
        familyMembers = game.GetFamilyMembers(familyID);
    }

    void Update(){
        if(burning){
        burnLeft-=Time.deltaTime;
            float colour = (burnLeft/10.0f)*(burnLeft/10.0f);
        GetComponent<SpriteRenderer>().color = new Color(colour,colour,colour,(burnLeft/10.0f));
        }

        bool inSuitcase = itemPosition.InSuitcase();
        if(inSuitcase && !inSuitcaseLastFrame){
            //add goodness
            foreach(FamilyMember f in familyMembers){
                f.memoryValue+=memoryValue;
            }
        }else if (!inSuitcase && inSuitcaseLastFrame){
            //remove goodness
            foreach(FamilyMember f in familyMembers){
                f.memoryValue-=memoryValue;
            }
        }
        inSuitcaseLastFrame = inSuitcase;
    }

    public bool InSuitcase(){
        return itemPosition.InSuitcase();
    }

}
