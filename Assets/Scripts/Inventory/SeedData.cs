using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Seed")]
public class SeedData : ItemData
{
    //Times watering it takes before the seed matures into a crop
    public int WateringTimesToGrow;

    //The crop the seed will yield
    public ItemData cropToYield; 
}