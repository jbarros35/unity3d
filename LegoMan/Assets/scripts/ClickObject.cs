using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {
    
    public Color newEmissionColor;
    public bool emission;
    // Use this for initialization
    void Start () {
     
    }

    // Update is called once per frame
    void Update()
    {        
        if (emission)
        {
            Glow(newEmissionColor);
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Construction")
                {
                    Debug.Log("It's working!");
                }
                else
                {
                    Debug.Log("nopz");
                }
            }
            else
            {
                //Debug.Log("No hit");
            }
            //Debug.Log("Mouse is down");
        }
    }

    void Glow(Color baseColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission = Mathf.PingPong(Time.time, 1.0f);
        //Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

        //Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        Color finalColor = newEmissionColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
    
    void OnMouseOver()
    {
        emission = true;
    }

    void OnMouseExit()
    {
        emission = false;
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetColor("_EmissionColor", Color.black);
    }
}
