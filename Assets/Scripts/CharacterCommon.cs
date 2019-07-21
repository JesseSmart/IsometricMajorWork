using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommon : MonoBehaviour
{
    public CharacterStats characterStats;
    private CharacterClass myClass = new CharacterClass();

    // Start is called before the first frame update
    void Start()
    {
        myClass.myHealth = characterStats.health;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (myClass.myHealth >= 0)
        {
            myClass.myHealth -= damage;
        }
        else if (myClass.myHealth < 0)
        {
            int rndChance = Random.Range(0, 100);

            if (rndChance <= Mathf.Abs(myClass.myHealth))
            {
                //alive
                print("Alive");
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
        Destroy(gameObject);

    }

}
