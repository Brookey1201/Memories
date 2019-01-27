using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gameoverText;
    public GameObject gameOverScreen;

    public float happiness{set;get;}
    public Slider happinessSlider;

    private Crate currentCrate;
    public float timeBetweenLevels;
    public Image betweenLevels;
    private float currentTimeBetweeenLevels;

    public Furnace furnace;

    public List<Crate> crates;
    
    public bool playing = true;
    public bool midLevel = true;

    public ItemParent itemParent;
    
    public List<FamilyMember> familyMembers;

    int day = -1;

    public TypeWriterText text { set; get; }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.active = false;
        text = GetComponent<TypeWriterText>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!gameOverScreen.active){
            float total = 0;
            float denominator = 0.01f;
            foreach(FamilyMember f in familyMembers){
                denominator += f.influence;
                total+=f.influence*(f.memoryValue/100.0f);
            }

            happiness = Mathf.Clamp(total/denominator, 0, 100);
            happinessSlider.value = happiness*100.0f;
        
        
            if(!midLevel){//inbetween levels
                if(playing){
                    bool stillPlaying = false;
                    foreach(Item i in itemParent.itemList){
                        if(!i.InSuitcase()){
                            stillPlaying = true;
                        }
                    }
                    if(happiness<=0.01f || furnace.Warmth<=1){
                        stillPlaying = false;
                        day = 1000;
                        if(happiness<=0.01f){
                            gameoverText.text = "Too few memories led you to insanity...";
                        }else{
                            gameoverText.text = "You didn't survive the cold...";
                        }
                    }
                    if(!stillPlaying){
                        playing = false;
                    }
                }else{
                    StartNewDay();
                }
            }else{

                if(currentTimeBetweeenLevels>0){
                    currentTimeBetweeenLevels-=Time.deltaTime;
                }
                else{
                    playing = true;
                    midLevel = false;
                    betweenLevels.color = Color.clear;
                }
            }
        }
    }

    void StartNewDay(){
        ClearItems();
        day++;
        if(currentCrate!=null){
            Destroy(currentCrate.gameObject);
        }
        if(day<crates.Count){
            currentCrate = Instantiate<Crate>(crates[day]);
            midLevel = true;
            currentTimeBetweeenLevels = timeBetweenLevels;
            betweenLevels.color = Color.black;
            furnace.Warmth=80.0f;
        }else{
            gameOverScreen.SetActive(true);
        }
        
    }

    void ClearItems(){
        for(int i = itemParent.itemList.Count-1; i>=0; i--){
            if(!itemParent.itemList[i].InSuitcase()){
                itemParent.RemoveItem(itemParent.itemList[i]);
            }
        }
    }

    public List<FamilyMember> GetFamilyMembers(List<int> idList){
        List<FamilyMember> newList = new List<FamilyMember>();
        while(idList.Count>0){
            int id = idList[0];
            idList.RemoveAt(0);

            if(id>=0 && id<familyMembers.Count){
                newList.Add(familyMembers[id]);
            }
        }

        return newList;
    }
}
