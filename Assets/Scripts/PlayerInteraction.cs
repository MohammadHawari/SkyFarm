using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        if(Physics.Raycast(transform.position, Vector3.down,out hit,  1))
        {
            OnInteractableHit(hit);
        }
    }

    //Handles what happens when the interaction raycast hits something interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        
        //Check if the player is going to interact with land
        if(other.tag == "Land")
        {
            //Get the land component
            Land land = other.GetComponent<Land>();
            land.Select(true);
        }

    }


}