using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Quaternion rotation;
    //Private variable to store the offset distance between the player and camera
    private Vector3 offset;         
    public Transform target;
    public float Speed;
    public int maxZoomOut = -10;
    public int maxZoomIn = 10;
    int curZoom = 0;

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
        
        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //Debug.Log("Zoom out " + curZoom);
            if (curZoom >= maxZoomOut)
            {                
                curZoom--;
                if (Camera.main.fieldOfView <= 125)
                    Camera.main.fieldOfView += 2;
                if (Camera.main.orthographicSize <= 20)
                    Camera.main.orthographicSize += 0.5f;
            }            

        }
        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //Debug.Log("Zoom In " + curZoom);
            if (curZoom <= maxZoomIn) {                
                curZoom++;
                if (Camera.main.fieldOfView > 2)
                    Camera.main.fieldOfView -= 2;
                if (Camera.main.orthographicSize >= 1)
                    Camera.main.orthographicSize -= 0.5f;
            }            
        }
    }

}
