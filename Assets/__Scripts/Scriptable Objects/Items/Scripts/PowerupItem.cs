using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Power-up", menuName = "Inventory System/Items/PowerUp")]
public class PowerupItem : Item
{
    [TextArea(10, 20)]
    public string ability;

    public enum PowerUpAbility { SpeedBoost, WildCard, EnergyDrink, None };
    public PowerUpAbility type = PowerUpAbility.None;

    public void Awake()
    {
        itemCategory = ItemCategory.PowerUp;
    }
}
