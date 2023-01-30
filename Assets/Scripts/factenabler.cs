using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factenabler : MonoBehaviour
{

    public static factenabler Instance { get; private set; }

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

    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject note4;

    public void EnableNote(int numberOfNote)
    {
        if(numberOfNote == 1)
        {
            if(!note1.activeSelf)
            {
                note1.SetActive(!note1.activeSelf);
            }
        }
        else if(numberOfNote == 2)
        {
            if(!note2.activeSelf)
            {
                note2.SetActive(!note2.activeSelf);
            }
        }
        else if(numberOfNote == 3)
        {
            if(!note3.activeSelf)
            {
                note3.SetActive(!note3.activeSelf);
            }
        }
        else if(numberOfNote == 4)
        {
            if(!note4.activeSelf)
            {
                note4.SetActive(!note4.activeSelf);
            }
        }
    }
}
