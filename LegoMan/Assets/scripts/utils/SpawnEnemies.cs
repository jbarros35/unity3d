using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    // List of AI Points for pathing
    public Transform[] randomDestinies;
    // prefab reference to enemy
    public GameObject enemyType;
    // reference to player
    public Transform Player;
    public int maxEnemies;
    public float timeSpawn;
    private float time;
    public int countEnemies;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (countEnemies < maxEnemies && time > timeSpawn)
        {
            GameObject enemy = Instantiate(enemyType, transform.position, transform.rotation);
            NavMeshControl meshControl = enemy.GetComponent<NavMeshControl>();
            meshControl.Player = Player;
            meshControl.randomDestinies = randomDestinies;

            countEnemies++;
            time = 0;
        }
    }
}
