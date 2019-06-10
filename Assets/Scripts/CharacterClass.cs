using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass
{
    public float myHealth;
    public float basicACooldown;
    public float moveACooldown;
    public float ultACooldown;
    //public float dodgeCooldown;


    public float basicATimer;
    public float moveATimer;
    public float ultATimer;
    //public float dodgeTimer;

    // Inputs
    public int basicButton = 0;
    public int moveButton = 2;
    public int ultButton = 3;

}
