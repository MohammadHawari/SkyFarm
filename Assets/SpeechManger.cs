using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechManger : MonoBehaviour
{
    public static SpeechManger Instance { get; private set; }

    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this; 
        }
    }

    public TMP_Text dialog;

    int tilltedLandNum = 0;
    int seedLandNum = 0;
    int wateredLandNum = 0;
    int eventflow = 0;

    public Land[] farm = new Land[9];


    public enum ConversationNum
    {
        one, two, three, four, five
    }

    public ConversationNum conversationNum;


    public void LandCounter(string landName)
    {
        if (landName == "tillted")
        {
            tilltedLandNum++;
        }
        else if(landName == "seed")
        {
            seedLandNum++;
        }
        else if(landName == "watered")
        {
            wateredLandNum++;
        }
    }

    public void SwitchDialog(ConversationNum statusToSwitch)
    {
        //Set dialog status accordingly
        conversationNum = statusToSwitch;
 
        //Decide what dialog to switch to
        switch (statusToSwitch)
        {
            case ConversationNum.one:
                
                dialog.text = "first of all you need to plow the soil to turn and break it up, to bury crop residues, and to help control weeds. Open up your inventory and equip the hoe to do that";
                // hide the info panel
                break;
            case ConversationNum.two:
                
                dialog.text = "Good job, now you need to plant the seeds you have and watering them. Most vegetables need to be watered every day early in the morning before the sun gets too hot";
                break;

            case ConversationNum.three:
                
                dialog.text = "Now we wait, tomato needs about 65 days to fully grow and cabbages needs about 120 days";
                break;

            case ConversationNum.four:
                
                dialog.text = "We are not finshed yet go ahead and harvest the vegetables, you wil need them to feed the hidden rabbits in exchange for facts. Good luck with searching ;) ";
                break;  
        }
    }

    public void SkipTime()
    {
        foreach (Land land in farm)
        {
            land.Growing();
        }
    }

    void Start()
    {
        dialog.text = "Welcome to Sky Farm, lets plant some vegetables and learn about farming";
        UIManager.Instance.ToggleInfoPanel();
    }

    // Update is called once per frame
    void Update()
    {

        if(eventflow == 0)
        {

            if(Input.GetKeyDown("f"))
            {
                SwitchDialog(ConversationNum.one);
                eventflow++;
            }
            
        }
        else if(eventflow == 1)
        {
            if(Input.GetKeyDown("f"))
            {
                UIManager.Instance.ToggleOffInfoPanel();
                eventflow++;
            }
            
        }
        else if(eventflow == 2)
        {
            
            if(tilltedLandNum >= 3)
            {
                UIManager.Instance.ToggleInfoPanel();
                SwitchDialog(ConversationNum.two);
                eventflow++;
            }
        }
        else if(eventflow == 3)
        {
            if(Input.GetKeyDown("f"))
            {
                UIManager.Instance.ToggleOffInfoPanel();
                eventflow++;
            }
        }
        else if(eventflow == 4)
        {
            
            if(wateredLandNum >= 3 && seedLandNum >=3)
            {
                UIManager.Instance.ToggleInfoPanel();
                SwitchDialog(ConversationNum.three);
                eventflow++;
            }
        }
        else if(eventflow == 5)
        {
            if(Input.GetKeyDown("f"))
            {
                SkipTime();
                SwitchDialog(ConversationNum.four);
                eventflow++;
            }
        }
        else if(eventflow == 6)
        {
            if(Input.GetKeyDown("f"))
            {
                UIManager.Instance.ToggleOffInfoPanel();
                eventflow++;
            }
        }        
    }


}
