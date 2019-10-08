using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCommon : MonoBehaviour
{
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    public Slider sldHealth;
    public Slider sldBrinkHealth;
    public Slider sldDeathChance;

    private bool canUpdateHealth = true;

	public TextMeshProUGUI pNumIndic;
    // Start is called before the first frame update
    void Start()
    {
        myClass.myHealth = characterStats.health;
		pNumIndic.text = "P" +  (gameObject.GetComponent<IsometricPlayerMovementController>().playerNumber + 1);
    }

    // Update is called once per frame
    void Update()
    {
        UISetter();
    }

    void UISetter()
    {
        if (myClass.myHealth > 0)
        {
            sldHealth.value = myClass.myHealth / characterStats.health;

            if (!sldHealth.gameObject.active || sldBrinkHealth.gameObject.active)
            {
                sldHealth.gameObject.SetActive(true);
                sldBrinkHealth.gameObject.SetActive(false);
            }
        }
        else
        {
            if (canUpdateHealth)
            {
                sldBrinkHealth.value = Mathf.Abs(myClass.myHealth / characterStats.health);

                if (sldHealth.gameObject.active || !sldBrinkHealth.gameObject.active)
                {
                    sldHealth.gameObject.SetActive(false);
                    sldBrinkHealth.gameObject.SetActive(true);
                }

            }
        }
    }

    public void TakeDamage(float minDamage, float maxDamage, GameObject dealerOwner)
    {
		float damage = Random.Range(minDamage, maxDamage);

        if (myClass.myHealth >= 0)
        {
            myClass.myHealth -= damage;


        }
        else if (myClass.myHealth < 0)
        {


            int rndChance = Random.Range(0, 100);

            //sldDeathChance.gameObject.SetActive(true);
            sldDeathChance.value = rndChance / 100f;            
			//FindObjectOfType<CameraController>().BrinkZoom(transform);
            StartCoroutine(disbableUIDelay(sldDeathChance.gameObject));
			if (rndChance >= Mathf.Abs(myClass.myHealth))
            {
                //alive
                print("Alive");
                myClass.myHealth -= damage;

            }
            else
            {
                //dead
                Death(dealerOwner);
            }
        }



    }

    void Death(GameObject killer)
    {
		if (killer.GetComponent<GunFreakController>())
		{
			killer.GetComponent<GunFreakController>().PassiveAbility();
		}

		Destroy(gameObject, 1f);
		FindObjectOfType<CameraController>().PlayerDeath(transform);
    }

    private IEnumerator disbableUIDelay(GameObject obj)
    {
        obj.SetActive(true);
        canUpdateHealth = false;
        yield return new WaitForSecondsRealtime(1f);
        canUpdateHealth = true;
        obj.SetActive(false);
        
    }

}
