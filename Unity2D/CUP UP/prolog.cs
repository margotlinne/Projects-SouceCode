using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
    
}

public class prolog : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text txtDialogue;


    public GameManager gameManager;
    public GameObject skipText;
    public static bool isSkipped = false;
    private bool isDialogueStarted = false;
    public static bool isDialogueDone = false;
    private bool isDialogue = false;
    private Animator anim;

    private int count;

    [SerializeField] private Dialogue[] dialogue;

    private bool isDone = false;

    public void textBoxAppeared()
    {
        isDone = true;
    }

    public void ShowDialogue()
    {
        //(q1) answer ********************
        if(isSkipped == false)
        //(q1) answer ********************
        {
            // when it starts show dialogue box and text
            OnOff(true);
            count = 0;
            NextDialogue();
        }
        
    }

    private void OnOff(bool _flag)
    {
        dialogueBox.SetActive(_flag);
        // when dialogue box appears then show text
        if(_flag == true)
        {
            if(isDone == true)
            {
                txtDialogue.gameObject.SetActive(_flag);
            }
        }
        // when dialogue disappear text diappear together
        else
        {
            txtDialogue.gameObject.SetActive(_flag);
        }

        isDialogue = _flag;
    }


  

    private void NextDialogue()
    {
        txtDialogue.text = dialogue[count].dialogue;
        count++;
    }

    private void DialogueDone()
    {
        skipText.SetActive(false);
        isSkipped = false;
        anim.SetTrigger("dialogue_done");
        anim.SetBool("dialogue_start", false);
        isDone = false;
        OnOff(false);
    }

    
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        // add if press 'enter' skip the prolog and go to game scene right away
        if (isDialogueDone == false && MainCamera.isCameraMoved == true)
        {
            // error when textbox shows up and not text yet + press enter, text is not disappearing. (q1)
            // fixed by adding "if(isSkipped == false)" to Showdialogue
            if(Input.GetKeyDown(KeyCode.Return))
            {
                DialogueDone();
                isSkipped = true;
                // transition.cs -> startGame()
            }

            // only at the begining show first dialogue
            // when enter is pressed at the very beginning it has issue that dialogue text is not disappeared, so added "isSkiped == false" and made transition from cup_idle to departing
            // *when enter is pressed before textbox shows up**
            if (isSkipped == false && count == 0)
            {
                skipText.SetActive(true);
                anim.SetBool("dialogue_start", true);
                isDialogueStarted = true;
            }
        }


        if (isDialogueStarted == true)
        {
            ShowDialogue();
        }

        // add "isDone(textbox appeared)" so there's no error when pressing "X" before text box shows up(like text is invisible)
        if (isDialogue && isDone)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Audio.UISoundPlay();
                // because I didn't code this, above this "if(isDialogueStarted == true){ ShowDialogue()}" was looping. little mistake 
                isDialogueStarted = false;
                if (count < dialogue.Length)
                {
                    NextDialogue();

                }
                else
                {
                    isDialogueDone = true;
                }
            }
        }



        if (isDialogueDone == true)
        {
            DialogueDone();
        }
    }





}
