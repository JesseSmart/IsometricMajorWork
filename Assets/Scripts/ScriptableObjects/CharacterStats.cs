using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "CharacterStatistics")]
public class CharacterStats : ScriptableObject
{

    public string myName;


    public string basicAbilityDescription;
    public string movementAbilityDescription;
    public string ultimateAbilityDescription;
    public string passiveAbilityDescription;

    public Sprite displayImage;

    public float health;
    public float basicAbilityCooldown;
    public float movementAbilityCooldown;
    public float ultimateAbilityCooldown;
    public float dodgeCooldown;


}
