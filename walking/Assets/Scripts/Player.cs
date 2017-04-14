using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float lifePoints;
    public Slider healthBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        healthBar.value = lifePoints / 1;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collided" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            lifePoints -= 0.1f;
        }
        
    }
}
