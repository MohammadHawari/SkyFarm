using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentData : ItemData
{
    public enum ToolType
    {
        Hoe, WateringCan, Axe, Pickaxe, Timer65D, Timer120D
    }
    public ToolType toolType;
}
