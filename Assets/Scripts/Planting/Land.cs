using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }
    
    [Header("Crops")]
    public CropTransmition crop;
    private bool planted = false;
    private bool grown = false;

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    //The selection gameobject to enable when the player is selecting the land
    public GameObject select;
    public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        //Get the renderer component
        renderer = GetComponent<Renderer>();

        //Set the land to soil by default
        SwitchLandStatus(LandStatus.Soil);

        //Deselect the land by default
        Select(false);

    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        //Set land status accordingly
        landStatus = statusToSwitch;

        Material materialToSwitch = soilMat; 

        //Decide what material to switch to
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                //Switch to the soil material
                materialToSwitch = soilMat;
                break;
            case LandStatus.Farmland:
                //Switch to farmland material 
                materialToSwitch = farmlandMat;
                break;

            case LandStatus.Watered:
                //Switch to watered material
                materialToSwitch = wateredMat;
                break; 

        }

        //Get the renderer to apply the changes
        renderer.material = materialToSwitch; 
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    //When the player presses the interact button while selecting this land


    // I need to transform all the logic to a diffrent function where i can wait and make the interact
    // function only for switching the case ::::::::::::::::::::::;

    private IEnumerator Hoe()
    {
        ThirdPersonMovment.Instance.FarmingAnimation();
        yield return new WaitForSeconds(0.9f);
        SwitchLandStatus(LandStatus.Farmland);
    }

    private IEnumerator Gathering()
    {
        ThirdPersonMovment.Instance.GatheringAnimation();
        yield return new WaitForSeconds(1f);
        planted = false;
        grown = false;
        
        SwitchLandStatus(LandStatus.Farmland);       
        crop.Delete();
        InventoryManager.Instance.AddItemToInventory(crop.plantTransform);
    }

    public void Interact()
    {

        //Check the player's tool slot
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        //Try casting the itemdata in the toolslot as EquipmentData
        EquipmentData equipmentTool = toolSlot as EquipmentData; 

        //Check if it is of type EquipmentData 
        if(equipmentTool != null)
        {
            //Get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    if(landStatus == LandStatus.Soil)
                    {                      
                        StartCoroutine(Hoe());                        
                    }

                    if(landStatus == LandStatus.Watered && planted && grown)
                    {
                        StartCoroutine(Gathering());
                    
                    }

                    break;
                case EquipmentData.ToolType.WateringCan:

                    if(landStatus == LandStatus.Farmland)
                    {
                        SwitchLandStatus(LandStatus.Watered);
                    }
                    break;
                case EquipmentData.ToolType.Timer65D:
                    if(landStatus == LandStatus.Watered && planted && !grown && crop.VegieName() == "Tomato")
                    {
                        crop.Grow();
                        grown = true;
                    }
                    break;
                case EquipmentData.ToolType.Timer120D:
                    if(landStatus == LandStatus.Watered && planted && !grown && crop.VegieName() == "Cabbage")
                    {
                        crop.Grow();
                        grown = true;
                    }
                    break;
            }

            return;
        }

        SeedData seedTool = toolSlot as SeedData;
        if(seedTool != null && landStatus != LandStatus.Soil && !planted)
        {
            crop.Plant(seedTool);
            planted = true;
        }
    }
}