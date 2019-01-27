using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterText : MonoBehaviour
{

    public Text dialogueName;
    public Text dialogueText;
    public Text dialogueSkip;

    private List<string> dialogueLines = new List<string>();
    private int currentDialogueIndex = 0;
    private string currentText = "";
    private bool isTextTyping = false;

    public bool isTextShowing = false;

    void Start()
    {
        dialogueName.text = "";
        dialogueText.text = "";
    }

    void Update()
    {
            if((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            SkipToNextText();
        }
    }

    public void SetDialogueLines(string itemName, List<string> dialogue)
    {
        dialogueName.text = itemName;
        dialogueLines = dialogue;
        currentDialogueIndex = 0;
        isTextShowing = true;
        StartCoroutine(AnimateText());
    }

    private void SkipToNextText()
    {
        StopAllCoroutines();
        if (isTextTyping)
        {
            dialogueText.text = currentText;
            dialogueSkip.gameObject.SetActive(true);
            isTextTyping = false;
        }
        else
        {
            if (++currentDialogueIndex < dialogueLines.Count)
            {
                StartCoroutine(AnimateText());
            }
            else
            {
                dialogueName.text = "";
                dialogueText.text = "";
                dialogueSkip.gameObject.SetActive(false);
                isTextShowing = false;
            }
        }
    }

    IEnumerator AnimateText()
    {   
        currentText = dialogueLines[currentDialogueIndex];

        dialogueSkip.gameObject.SetActive(false);
        isTextTyping = true;
        for (int i = 0; i < (currentText.Length + 1); i++)
        {
            dialogueText.text = currentText.Substring(0, i);
            yield return new WaitForSeconds(0.03f);
        }
        isTextTyping = false;
        if(currentDialogueIndex < dialogueLines.Count-1){ 
            dialogueSkip.gameObject.SetActive(true);
        }
    }
}

