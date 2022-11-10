using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTransmition : MonoBehaviour
{

    private SeedData seedData;
    public GameObject seed;
    private GameObject grownPlant;
    string vegieName;
    
    private enum CropState
    {
        Seed, GrownPlant
    }


    public void Plant(SeedData seed)
    {
        seedData = seed;
        ItemData plantTransform = seedData.cropToYield;
        grownPlant = Instantiate(plantTransform.gameModel, transform);
        vegieName = plantTransform.name;
        CropStateSwitch(CropState.Seed);

    }

    public void Grow()
    {
        CropStateSwitch(CropState.GrownPlant);
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
        }
    }

    public string VegieName()
    {
        Debug.Log(vegieName);
        return vegieName;
    }
}
