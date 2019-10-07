using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingController : MonoBehaviour
{
    public int pNum;
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    public GameObject spinMoveObj;
    public GameObject throwAxeObject;
	public GameObject ultimateAxeObj;


    //UI
    public Slider sldBasicA;
    public Slider sldMovementA;
    public Slider sldUltA;

	Animator anim;


	// Start is called before the first frame update
	void Start()
    {
		pNum = gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber;
		anim = GetComponentInChildren<Animator>();
		

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
        //myClass.myHealth = characterStats.health;
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
			PlayClip("Basic Ab");

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
			PlayClip("Move Ab");
			

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
			PlayClip("Ult Ab");


			GameObject ultAxe = Instantiate(ultimateAxeObj, transform.position, transform.rotation);
			ultAxe.GetComponent<VikingUltimateSpin>().myOwner = gameObject;

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
        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 0) || Input.GetKeyDown(KeyCode.E))
        {
            BasicAbility();
        }

        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 2) || Input.GetKeyDown(KeyCode.F))
        {
            MovementAbility();
        }

        if (Input.GetKeyDown("joystick " + (pNum + 1) + " button " + 3) || Input.GetKeyDown(KeyCode.R))
        {
            UltimateAbility();
        }

    }

    void Cooldowns()
    {
        myClass.basicATimer -= Time.deltaTime;
        myClass.moveATimer -= Time.deltaTime;
        myClass.ultATimer -= Time.deltaTime;

        sldBasicA.value = 1 - (myClass.basicATimer / myClass.basicACooldown);
        sldMovementA.value = 1 -(myClass.moveATimer / myClass.moveACooldown);
        sldUltA.value = 1 - (myClass.ultATimer / myClass.ultACooldown);
    }


	void PlayClip(string clipName)
	{
		anim.Play(clipName);
		GetComponent<IsometricPlayerMovementController>().DisableAnims(anim.GetCurrentAnimatorClipInfo(0).Length);
	}



}
