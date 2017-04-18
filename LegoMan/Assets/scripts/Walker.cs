using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public Animator animation;
    public bool IsWalking;
    public float speedFwd = 8;
    public float speedRot;

    public float speed;
	// Use this for initialization
	void Start () {
        animation = this.gameObject.GetComponent<Animator>();        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float speedWalk = speedFwd * Time.deltaTime;
        speedRot = 45 * Time.deltaTime;
        //healthTxt.text = 'HP:' + health;
        //scoreTxt.text = 'Score:' + score;
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            IsWalking = true;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, speedWalk);
            } else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -speedWalk);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -speedRot, 0);
            } else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, speedRot, 0);
            }
            animation.SetInteger("CurrentAction", 1);            
        } if (Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.S)|| Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            IsWalking = false;
            animation.SetInteger("CurrentAction", 0);
        }


        // punch
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.Translate(0, 0, speedWalk);
            animation.SetInteger("CurrentAction", 2);
        } else if (Input.GetKeyUp(KeyCode.T))
        {
            animation.SetInteger("CurrentAction", 4);
        }
        // kick
        if (Input.GetKeyDown(KeyCode.Y))
        {
            transform.Translate(0, 0, speedWalk);
            animation.SetInteger("CurrentAction", 3);
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            animation.SetInteger("CurrentAction", 4);
        }
                
    }

   
}
