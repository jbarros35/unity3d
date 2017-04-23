using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    
    public bool debugActive;
    public string monsterName;
    public float HP = 1.0f;
    public float maxHP = 1.0f;
    public float DanoDoInimigo = 0.1f;

    void Start()  {
      
    }


    void Update()  {
   
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        debug("Collision enter:"+collision.gameObject.name);
        // force is how forcefully we will push the player away from the enemy.
        float force = 3;

        // If the object we hit is the enemy
        if (collision.gameObject.tag == "Player")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = collision.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);
        }

        if (collision.gameObject.tag == "Weapon")
        {
            debug("Have been hit");
            if (HP>0)
            {
                HP -= 0.1f;
            } else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        debug("Collision exit:"+ collision.gameObject.name);
    }

    private void debug(string msg)
    {
        if (debugActive)
        {
            Debug.Log(msg);
        }        
    }
}