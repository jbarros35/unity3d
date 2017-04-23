using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public Animator animation;
    public float speedFwd = 8;
    public float speedRot;
    public GameObject weaponPosition;
    public GameObject baseballBat;

    public float speed;
	// Use this for initialization
	void Start () {
        animation = this.gameObject.GetComponent<Animator>();
        //weaponPosition = (GameObject)Instantiate(weaponPosition);
        //baseballBat = (GameObject)Instantiate(baseballBat);
        baseballBat = Instantiate(baseballBat, weaponPosition.transform.localPosition, weaponPosition.transform.localRotation);
        baseballBat.transform.parent = weaponPosition.transform;
        baseballBat.transform.localPosition = weaponPosition.transform.localPosition;
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

        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(0, speedRot, 0);
            } else
            {
                transform.Rotate(0, -speedRot, 0);
            }
        }

        if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.Translate(0, 0, speedWalk);
            } else
            {
                transform.Translate(0, 0, -speedWalk);
            }
            animation.SetInteger("CurrentAction", 1);
        }
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            animation.SetInteger("CurrentAction", 0);
        }
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(0, 0, 0.01f);
            animation.SetTrigger("attacking");
        }
                              
    }

   
}
