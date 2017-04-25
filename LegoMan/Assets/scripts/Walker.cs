using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    private Animator animation;
    public float speedFwd = 8;
    public float speedRot;
    public GameObject weaponPosition;
    public GameObject meleeWeapon;
    bool ActivateTimerToReset = false;

    public float currentComboTimer = 0.5f;
    public int currentComboState = 0;
    float origTimer;

    public float speed;
	// Use this for initialization
	void Start () {
        animation = this.gameObject.GetComponent<Animator>();
        if (meleeWeapon)
        {
          //  weaponPosition = GameObject.Find("weaponPosition");
            Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts) if (t.gameObject.name == "weaponPosition") weaponPosition = t.gameObject;
            
            meleeWeapon = Instantiate(meleeWeapon, weaponPosition.transform.localPosition, weaponPosition.transform.localRotation);
            meleeWeapon.transform.parent = weaponPosition.transform;
            meleeWeapon.transform.localPosition = weaponPosition.transform.localPosition;
        }
        origTimer = currentComboTimer;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        float speedWalk = speedFwd * Time.deltaTime;
        float speedBack = speedWalk / 2;
        speedRot = 45 * Time.deltaTime;
        //healthTxt.text = 'HP:' + health;
        //scoreTxt.text = 'Score:' + score;
        NewComboSystem();
        //Initially set to false, so the method won't start
        ResetComboState(ActivateTimerToReset);
        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(0, speedRot, 0);
            } else
            {
                transform.Rotate(0, -speedRot, 0);
            }
            animation.SetInteger("attacking", -1);
            animation.SetInteger("CurrentAction", 1);
        }

        if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.Translate(0, 0, speedWalk);
            } else
            {
                transform.Translate(0, 0, -speedBack);
            }
            animation.SetInteger("attacking", -1);
            animation.SetInteger("CurrentAction", 1);
        }
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            animation.SetInteger("CurrentAction", 0);
            animation.SetInteger("attacking", -1);
        }
        /*
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(0, 0, 0.01f);
            animation.SetInteger("attacking", 1);
        }*/
                              
    }

    void ResetComboState(bool resetTimer)
    {
        if (resetTimer)
        //if the bool that you pass to the method is true
        // (aka if ActivateTimerToReset is true, then the timer start
        {
            currentComboTimer -= Time.deltaTime;
            //If the parameter bool is set to true, a timer start, when the timer
            //runs out (because you don't press fast enought Z the second time)
            //currentComboState is set again to zero, and you need to press it twice again
            if (currentComboTimer <= 0)
            {
                currentComboState = 0;
                ActivateTimerToReset = false;
                currentComboTimer = origTimer;
            }
        }
    }

    void NewComboSystem()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            //No need to create a comboStateUpdate()
            //function while you can directly
            //increment a variable using ++ operator
            currentComboState++;

            //Okay, you pressed Z once, so now the resetcombostate Function is
            //set to true, and the timer starts to reset the currcombostate
            ActivateTimerToReset = true;

            //Note that I'm to lazy to setup a switch statement
            //that would be WAY more readable than 3 if's in a row
            if (currentComboState == 1)  {

                Debug.Log("1 hit");
                animation.SetInteger("attacking", 0);
            }
            if (currentComboState == 2)  {
                Debug.Log("2 hit, The combo Should Start");
                animation.SetInteger("attacking", 1);
                //Do your awesome stuff there and combokill the bitches
            }
          
        }
    }

}
