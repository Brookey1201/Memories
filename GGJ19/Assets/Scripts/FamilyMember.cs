using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FamilyMember : MonoBehaviour
{

    public bool preDiscovered = false;

    public float influence;


    public float memoryValue{set;get;}
    private float tempMemoryValue = 0;

    public Sprite mainImage;
    public SpriteRenderer overlay;
    public List<string> dialogue;
    
    public bool discovered {set;get;}




    float clickSpeed = 0.3f;
    float mouseDownTime;

    void Start(){
        
        discovered=false;
        memoryValue = 0.0f;
        tempMemoryValue = 0.0f;
        if(preDiscovered){
            Discover();
        }
    }

    public void Discover(){
        discovered = true;
        memoryValue += 50.0f;
    }

    void Update(){
        memoryValue = Mathf.Clamp(memoryValue,0,100);
        tempMemoryValue -=(tempMemoryValue-memoryValue)/100.0f;
        overlay.color = new Color(0,0,0,(100.0f-tempMemoryValue)/100.0f);
    }


    public static bool canClick() {
		PointerEventData pos = new PointerEventData(EventSystem.current);
		pos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pos, results);
		return !(results.Count > 0);
	}

    private void OnMouseDown(){
        if(canClick()){
            mouseDownTime = Time.time;
        }
    }

    private void OnMouseUp(){
        if(canClick()){
            float currentTime = Time.time;

            if((currentTime-mouseDownTime)<clickSpeed){
                DetailView detailView = GameObject.Find("DetailView").GetComponent<DetailView>();
                List<string> correctedDialogue = new List<string>();
                if(memoryValue>60){
                    correctedDialogue = dialogue;
                }else{
                    if(memoryValue<=10){
                        correctedDialogue.Add("I'm not sure who this is anymore");
                        
                    }else{
                        correctedDialogue.Add(dialogue[0]);
                        if(memoryValue<=30){
                            correctedDialogue.Add("I dont remember much");
                        }else{
                            correctedDialogue.Add(dialogue[1]);
                            if(memoryValue<=60){
                                correctedDialogue.Add("I wish I remembered more");
                            }
                        }
                    }
                    
                }
                if(detailView!=null){
                    detailView.OpenDetail(mainImage,overlay.sprite,(100.0f-memoryValue)/100.0f,correctedDialogue);
                }
            }
        }
    }
}
