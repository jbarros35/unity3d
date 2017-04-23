using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScreenSpaceUIScript : MonoBehaviour {


    private BasicEnemy enemyScript;
    public Canvas canvas;
    public GameObject healthPrefab;

    public float healthPanelOffset = 0.35f;
    public GameObject healthPanel;
    public Text enemyName;
    public Slider healthSlider;
    private Renderer selfRenderer;
    public GameObject target;


    // Use this for initialization
    void Awake () {        
        GameObject enemyObj = GameObject.Find("enemyObj");
        enemyScript = enemyObj.GetComponent<BasicEnemy>();         
        healthPanel = Instantiate(healthPrefab) as GameObject;
        GameObject CanvasObj = GameObject.Find("EnemyCanvas");
        canvas = CanvasObj.GetComponent<Canvas>();
        healthPanel.transform.SetParent(canvas.transform, false);        
        enemyName = healthPanel.GetComponentInChildren<Text>();
        enemyName.text = enemyScript.monsterName;
        healthSlider = healthPanel.GetComponentInChildren<Slider>();        
        selfRenderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate () {
        healthSlider.value = enemyScript.HP / (float)enemyScript.maxHP;
       // Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
       Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
       // healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

        /*if (selfRenderer.isVisible)
        {
            healthPanel.SetActive(true);
        }
        else
        {
            healthPanel.SetActive(false);
        }*/
    }
}
