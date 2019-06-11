using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{

    public Rigidbody2D rbody;
    public int initialForce;

    public GameObject myOwner;

    public float maxFlightDuration;
    private float flightTimer;

    public float pullSpeed;
    private Vector2 direction;

    private bool stuckToTarget;
    private bool stuckToTerrain;
    private GameObject myTarget;

    private float pullFloat;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        flightTimer = maxFlightDuration;
    }

    // Update is called once per frame
    void Update()
    {
        flightTimer -= Time.deltaTime;
        if (flightTimer <= 0)
        {
            Destroy(gameObject);
        }

        if (stuckToTarget)
        {
            transform.position = myTarget.transform.position;
            TargetToViking(gameObject);
        }
        else if (stuckToTerrain)
        {
            VikingToTerrain(myTarget);
        }
    }

    void FixedUpdate()
    {

    }


    public void Throw(Vector2 dir)
    {
        direction = dir;
        rbody.AddForce(dir.normalized * initialForce);
        //look into force modes like acceleration
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!stuckToTarget && !stuckToTerrain)
        {

            if (other.gameObject.CompareTag("PlayerCharacter") && !myOwner) 
            {
                //deal damage here
                print("hit " + other);
                myTarget = other.gameObject;
                stuckToTarget = true;
            }
            else if (!myOwner)
            {
                //terrain
                //myTarget = other.gameObject;
                //rbody.constraints = RigidbodyConstraints2D.FreezeAll; 
                //stuckToTerrain = true;
            }


        }
        print("coll");
    }


    public void TargetToViking(GameObject target)
    {
        print("Player to " + target);
        pullFloat += Time.deltaTime;
        target.transform.position = Vector2.Lerp(target.transform.position, myOwner.transform.position, pullFloat * pullSpeed);

    }

    public void VikingToTerrain(GameObject target)
    {
        pullFloat += Time.deltaTime * 0.1f;

        myOwner.transform.position = Vector2.Lerp(myOwner.transform.position, transform.position, pullFloat * pullSpeed);

    }

}
