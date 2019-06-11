using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public Rigidbody2D rbody;

    public int initialForce;
    public int retForce;

    public GameObject myOwner;


    public float cantCatchDuration;
    private float cantCatchTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReturnToOwner(myOwner);
    }

    public void ReturnToOwner(GameObject player)
    {
        Vector2 dir = (Vector2)player.transform.position - rbody.position;
        dir.Normalize();

        rbody.AddForce(dir * retForce);

        CheckCatch(player);
    }

    public void Throw(Vector2 dir)
    {
        rbody.AddForce(Vector2.up * initialForce);
        //look into force modes like acceleration
    }

    private void CheckCatch(GameObject player)
    {
        cantCatchTimer -= Time.deltaTime;
        if (cantCatchTimer <= 0)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 0.2)
            {
                Destroy(gameObject, 0.4f);
            }
        }
    }
}
