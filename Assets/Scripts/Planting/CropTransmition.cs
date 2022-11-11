using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTransmition : MonoBehaviour
{

    private SeedData seedData;
    public GameObject seed;
    private GameObject grownPlant;
    string vegieName;
    public ItemData plantTransform;
    
    private enum CropState
    {
        Seed, GrownPlant, Nothing
    }


    public void Plant(SeedData seed)
    {
        seedData = seed;
        plantTransform = seedData.cropToYield;
        grownPlant = Instantiate(plantTransform.gameModel, transform);
        vegieName = plantTransform.name;
        CropStateSwitch(CropState.Seed);

    }

    public void Grow()
    {
        CropStateSwitch(CropState.GrownPlant);
    }

    public void Delete()
    {
        CropStateSwitch(CropState.Nothing);
    }

    private void CropStateSwitch(CropState state)
    {
        seed.SetActive(false);
        grownPlant.SetActive(false);

        switch (state)
        {
            case CropState.Seed:
            seed.SetActive(true);
            break;

            case CropState.GrownPlant:
            grownPlant.SetActive(true);
            break;

            case CropState.Nothing:
            break;
        }
    }

    public string VegieName()
    {
        Debug.Log(vegieName);
        return vegieName;
    }
}
