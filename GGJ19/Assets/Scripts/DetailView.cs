using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailView : MonoBehaviour
{
    public GameObject display;
    public Image mainImage;
    public Image overlay;
    public TypeWriterText text;
    

    public void OpenDetail(Sprite m, List<string> dialogue)
    {
        OpenDetail(m, null, 0.0f, dialogue);
    }
    public void OpenDetail(Sprite m, Sprite o, float opacity, List<string> dialogue)
    {
        display.SetActive(true);
        mainImage.sprite = m;
        overlay.sprite = o;
        overlay.color = new Color(0, 0, 0, opacity);
        
        text.SetDialogueLines("", dialogue);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(display.active && !text.isTextShowing){
            display.SetActive(false);
        }
    }
}
