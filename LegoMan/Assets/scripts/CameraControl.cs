using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public float speed = 2.0f;
    private float zoomSpeed = 2.0f;
    public int i = 1;
    public float smooth = 2.0F;
    public float tiltAngle = 30.0F;

    private float smoothSpeed = 0.125f;


    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
	}

    void LateUpdate()
    {
       transform.position = player.transform.position + offset;
       transform.LookAt(player.transform);
        // camera rotation code
        //transform.position = Vector3.Lerp(transform.position, offset, smoothSpeed);

        //CameraVectors();

    }

    public void FixedUpdate()
    {

        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            //  transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
        }
        */
    }
    

    void CameraVectors()
    {
        switch (i)
        {
            case 1:
                offset = new Vector3(0, 5, -5);
                break;
            case 2:
                offset = new Vector3(5, 5, 0);
                break;
            case 3:
                offset = new Vector3(0, 5, 5);
                break;
            case 4:
                offset = new Vector3(-5, 5, 0);
                break;
            default:
                offset = new Vector3(0, 5, -5);
                break;
        }
    }
}
