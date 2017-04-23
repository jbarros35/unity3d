using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshControl : MonoBehaviour {


    public Transform Player;
    // reference to navMesh
    private NavMeshAgent navMesh;
    public float playerDistance, aiPointDistance;
    public float perceptionDistance = 30, pursuitDistance = 20, attackDistance = 2, VelocidadeDePasseio = 3, VelocidadeDePerseguicao = 6, TempoPorAtaque = 1.5f, DanoDoInimigo = 0.1f;
    public bool VendoOPlayer;
    // List of AI Points for pathing
    public Transform[] randomDestinies;
    // the first point to MeshAgent
    public int AIPointAtual;
    public bool PerseguindoAlgo, contadorPerseguindoAlgo, atacandoAlgo;
    private float cronometroDaPerseguicao, cronometroAtaque;
    public LayerMask ignoreMask;
    // empty object which raycast is send
    public Transform LineOfSight;
    // animator controller
    private Animator animation;
    // the model itself
    private GameObject animatedObj;
    public bool debugActive;
   

    void Start()
    {
        AIPointAtual = Random.Range(0, randomDestinies.Length);
        navMesh = transform.GetComponent<NavMeshAgent>();
        animatedObj = transform.Find("enemyObj").gameObject;
        animation = animatedObj.GetComponent<Animator>();
    }

    // Fix for navMesh
    void OnAnimatorMove()
    {
        navMesh.velocity = animation.deltaPosition / Time.deltaTime;
    }

    void Update()
    {

        playerDistance = Vector3.Distance(Player.transform.position, transform.position);
        aiPointDistance = Vector3.Distance(randomDestinies[AIPointAtual].transform.position, transform.position);

        //============================== RAYCAST ===================================//
        RaycastHit hit;
        Vector3 deOnde = LineOfSight.transform.position;
        Vector3 paraOnde = Player.transform.position;
        //Vector3 paraOnde = attackPosition;
        Vector3 direction = paraOnde - deOnde;
              
        //if (Physics.Raycast(LineOfSight.transform.position, direction, out hit, 1000, ignoreMask))
        Ray ray = new Ray(LineOfSight.transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            debug("HIT:" + hit.collider.gameObject.name + hit.collider.gameObject.CompareTag("Player"));
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Player = hit.transform;
                VendoOPlayer = true;
            }
            else
            {
                VendoOPlayer = false;
            }
        }
        //================ CHECHAGENS E DECISOES DO INIMIGO ================//
        if (playerDistance > perceptionDistance)
        {
            Walk();
        }
        if (playerDistance <= perceptionDistance && playerDistance > pursuitDistance)
        {
            if (VendoOPlayer == true)
            {
                Look();
            }
            else
            {
                Walk();
            }
        }
        if (playerDistance <= pursuitDistance && playerDistance > attackDistance)
        {
            if (VendoOPlayer == true)
            {
                Pursuit();
                PerseguindoAlgo = true;
            }
            else
            {
                Walk();
            }
        }
        if (playerDistance <= attackDistance)
        {
            Attack();
        }


        //COMANDOS DE PASSEAR
        if (aiPointDistance <= 2)
        {
            AIPointAtual = Random.Range(0, randomDestinies.Length);
            Walk();
        }
        //CONTADORES DE PERSEGUICAO
        if (contadorPerseguindoAlgo == true)
        {
            cronometroDaPerseguicao += Time.deltaTime;
        }
        if (cronometroDaPerseguicao >= 5 && VendoOPlayer == false)
        {
            contadorPerseguindoAlgo = false;
            cronometroDaPerseguicao = 0;
            PerseguindoAlgo = false;
        }
        // CONTADOR DE ATAQUE
        if (atacandoAlgo == true)
        {
            cronometroAtaque += Time.deltaTime;
        }
        if (cronometroAtaque >= TempoPorAtaque && playerDistance <= attackDistance)
        {
            atacandoAlgo = true;
            cronometroAtaque = 0;
            //PLAYER.VIDA = PLAYER.VIDA - DanoDoInimigo;
            //Debug.Log("recebeuAtaque");
        }
        else if (cronometroAtaque >= TempoPorAtaque && playerDistance > attackDistance)
        {
            atacandoAlgo = false;
            cronometroAtaque = 0;
            //Debug.Log("errou");
        }

    }

    void Walk()
    {
        animation.SetInteger("CurrentState", 0);
        debug("Passear()");
        if (PerseguindoAlgo == false)
        {
            navMesh.acceleration = 5;
            navMesh.speed = VelocidadeDePasseio;
            navMesh.destination = randomDestinies[AIPointAtual].position;
        }
        else if (PerseguindoAlgo == true)
        {
            contadorPerseguindoAlgo = true;
        }
    }

    void Look()
    {
        animation.SetInteger("CurrentState", 1);
        debug("Olhar()");
        navMesh.speed = 0;
        transform.LookAt(Player);
    }

    void Pursuit()
    {
        animation.SetInteger("CurrentState", 2);
        debug("Perseguir()");
        navMesh.acceleration = 8;
       
        navMesh.speed = VelocidadeDePerseguicao;
        Vector3 attackPosition = new Vector3(Player.transform.position.x, Player.position.y, Player.position.z);
        navMesh.destination = Player.transform.position;
    }

    void Attack()
    {
        Transform[] transforms = this.GetComponentsInChildren<Transform>();
        Transform enemyObj = null;
        foreach (Transform t in transforms)
        {
            if (t.gameObject.name == "enemyObj")
            {
                //Debug.Log("Found " + t);
                enemyObj = t;
            }
        }
        if (enemyObj != null)
        {
            // fix child position at attack
            Vector3 localPos = enemyObj.localPosition;
            //localPos.z = attackDistance;
            //enemyObj.localPosition = localPos;
            //debug(transform.position + ", obj:" + localPos);
            enemyObj.position = transform.position;
        }      

        animation.SetInteger("CurrentState", 3);
        debug("Atacar()");
        atacandoAlgo = true;
    }

   

    private void debug(string msg)
    {
        if (debugActive)
        {
            Debug.Log(msg);
        }
    }
}
