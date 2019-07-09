using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingController : MonoBehaviour
{
    public int pNum;
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    public GameObject spinMoveObj;
    public GameObject throwAxeObject;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldowns();
        Inputer();

    }

    private void SetStats()
    {
        myClass.myHealth = characterStats.health;
        myClass.basicACooldown = characterStats.basicAbilityCooldown;
        myClass.moveACooldown = characterStats.movementAbilityCooldown;
        myClass.ultACooldown = characterStats.ultimateAbilityCooldown;
        //myClass.dodgeCooldown = characterStats.dodgeCooldown;


        //myClass.basicATimer = characterStats.basicAbilityCooldown;
        //myClass.moveATimer = characterStats.movementAbilityCooldown;
        myClass.ultATimer = characterStats.ultimateAbilityCooldown;
        //myClass.dodgeTimer = characterStats.dodgeCooldown;
    }

    private void BasicAbility()
    {
        if (myClass.basicATimer <= 0)
        {
            print(pNum + " Player: DO BASIC");

            GameObject spinObj = Instantiate(spinMoveObj, transform.position, transform.rotation);
            spinObj.GetComponent<VikingSpinMove>().myOwner = gameObject;

            myClass.basicATimer = myClass.basicACooldown;
        }
        else
        {
            print("Basic recharging");
        }
    }

    private void MovementAbility()
    {
        if (myClass.moveATimer <= 0)
        {
            GameObject thrownAxe = Instantiate(throwAxeObject, transform.position, transform.rotation);
            thrownAxe.GetComponent<ThrowAxe>().myOwner = gameObject;
            
            thrownAxe.GetComponent<ThrowAxe>().Throw(gameObject.GetComponent<IsometricPlayerMovementController>().lastDir);
            print(gameObject.GetComponent<IsometricPlayerMovementController>().currentDir);
            myClass.moveATimer = myClass.moveACooldown;
        }
        else
        {
            //Recharging
            print("Movement recharging");
        }
    }

    private void UltimateAbility()
    {
        if (myClass.ultATimer <= 0)
        {
            print(pNum + " Player: DO ULT");
            myClass.ultATimer = myClass.ultACooldown;
        }
        else
        {
            //Recharging
        }
    }

    private void PassiveAbility()
    {

    }

    private void Inputer()
    {
        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 0))
        {
            BasicAbility();
        }

        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2))
        {
            MovementAbility();
        }

        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3))
        {
            UltimateAbility();
        }

    }

    void Cooldowns()
    {
        myClass.basicATimer -= Time.deltaTime;
        myClass.moveATimer -= Time.deltaTime;
        myClass.ultATimer -= Time.deltaTime;
    }

    


}
