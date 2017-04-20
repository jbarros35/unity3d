﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    // slider healthbar reference
    public Slider healthBar;
    public float currentHealth { get; set; }
    public float maxHealth { get; set; }

	// Use this for initialization
	void Start () {
        maxHealth = 1.0f;
        currentHealth = maxHealth;
        healthBar.value = calculateHealth();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        healthBar.value = calculateHealth();
    }

    private float calculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth -= 0.1f;
        }
    }
}