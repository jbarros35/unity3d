using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

    public GameObject player;
    private NavMeshAgent navMesh;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        navMesh.destination = player.transform.position;
	}
}
