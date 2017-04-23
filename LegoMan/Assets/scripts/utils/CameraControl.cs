using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Quaternion rotation;
    //Private variable to store the offset distance between the player and camera
    private Vector3 offset;         
    public Transform target;
    public float Speed;

    private void Start()
    {
        offset = transform.position - target.transform.position;
        rotation = transform.rotation;

    }
     

    void LateUpdate()
    {
        //transform.position = target.transform.position + offset;
        transform.LookAt(target);

        transform.position += transform.right * Speed * Time.deltaTime;

    }

}
