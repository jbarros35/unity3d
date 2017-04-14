using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    private float speed = 2.0f;
    private float zoomSpeed = 2.0f;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
	}

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //transform.rotation = player.transform.rotation;
        transform.Rotate(player.transform.rotation.x, 45, player.transform.rotation.z);
        //float scroll = Input.GetAxis("Mouse ScrollWheel");
        //transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
        /*
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        } else
        {
           
        }
        */
    }
}
