﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCommon : MonoBehaviour
{
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    public Slider sldHealth;
    public Slider sldBrinkHealth;
    public Slider sldDeathChance;

    private bool canUpdateHealth = true;
    // Start is called before the first frame update
    void Start()
    {
        myClass.myHealth = characterStats.health;
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

    public void TakeDamage(float minDamage, float maxDamage)
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
                Death();
            }
        }
    }

    void Death()
    {
        Destroy(gameObject, 1f);

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
