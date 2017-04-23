using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthBar : MonoBehaviour {
    public Transform target;

    private void FixedUpdate()
    {
        
        transform.position = Camera.main.WorldToScreenPoint(target.position);
    }

}
