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
    public float force = 1;
    Renderer mainRenderer;
    Material m;
    Color32 c;

    void Start()  {
        debugActive = true;
        mainRenderer = gameObject.GetComponentInChildren<Renderer>();
        m = mainRenderer.material;
        c = mainRenderer.material.color;
    }


    void Update()  {
   
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        //debug("Collision enter:"+collision.gameObject.name);
        // force is how forcefully we will push the player away from the enemy.
        
        // If the object we hit is the enemy
        if (collision.gameObject.tag == "Player")
        {
            //debug("Collision on player");
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
           // debug("Have been hit HP:"+HP);
            StartCoroutine("Glow");
            if (HP>0)
            {
                HP -= 0.01f;
            } else
            {
                // This is model inside a navmesh agent we destroy the parent of object
                Destroy(transform.parent.gameObject);               
            }
        }
    }
    
    private void debug(string msg)
    {
        if (debugActive)
        {
            Debug.Log(msg);
        }        
    }

    IEnumerator Glow()
    {  
        
         mainRenderer.material = null;
         mainRenderer.material.color = Color.red;
         yield return new WaitForSeconds(0.5f);
        Debug.Log("change back color " + c);
         mainRenderer.material = m;
         mainRenderer.material.color = c;            
    }

}