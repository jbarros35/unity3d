using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent naveMesh;
    public float DistanciaDoPlayer, DistanciaDoAIPoint;
    public float DistanciaDePercepcao = 30, DistanciaDeSeguir = 20, DistanciaDeAtacar = 2, VelocidadeDePasseio = 3, VelocidadeDePerseguicao = 6, TempoPorAtaque = 1.5f, DanoDoInimigo = 40;
    public bool VendoOPlayer;
    public Transform[] DestinosAleatorios;
    public int AIPointAtual;
    public bool PerseguindoAlgo, contadorPerseguindoAlgo, atacandoAlgo;
    private float cronometroDaPerseguicao, cronometroAtaque;
    public LayerMask ignoreMask;
    public Transform LineOfSight;
    private Animator animation;
    private GameObject animatedObj;

    void Start()
    {
        AIPointAtual = Random.Range(0, DestinosAleatorios.Length);
        naveMesh = transform.GetComponent<NavMeshAgent>();
        animatedObj = transform.Find("bandit2").gameObject;
        animation = animatedObj.GetComponent<Animator>();
    }
    void Update()
    {

        DistanciaDoPlayer = Vector3.Distance(Player.transform.position, transform.position);
        DistanciaDoAIPoint = Vector3.Distance(DestinosAleatorios[AIPointAtual].transform.position, transform.position);
                
   //============================== RAYCAST ===================================//
   RaycastHit hit;
   Vector3 deOnde = LineOfSight.transform.position;
   Vector3 paraOnde = Player.transform.position;
   Vector3 direction = paraOnde - deOnde;
        Debug.DrawLine(LineOfSight.transform.position, direction * 1000, Color.red);
        //if (Physics.Raycast(transform.position, direction, out hit, 1000) && DistanciaDoPlayer < DistanciaDePercepcao)
        if (Physics.Raycast(LineOfSight.transform.position, direction, out hit, 1000)) {
            Debug.Log("HIT:"+hit.collider.gameObject.name+ hit.collider.gameObject.CompareTag("Player"));
           if (hit.collider.gameObject.CompareTag("Player")) {
               VendoOPlayer = true;
           } else {
                VendoOPlayer = false;
            }
   }
   //================ CHECHAGENS E DECISOES DO INIMIGO ================//
   if (DistanciaDoPlayer > DistanciaDePercepcao)
   {
       Passear();
   }
   if (DistanciaDoPlayer <= DistanciaDePercepcao && DistanciaDoPlayer > DistanciaDeSeguir)
   {
       if (VendoOPlayer == true)
       {
           Olhar();
       }
       else
       {
           Passear();
       }
   }
   if (DistanciaDoPlayer <= DistanciaDeSeguir && DistanciaDoPlayer > DistanciaDeAtacar)
   {
       if (VendoOPlayer == true)
       {
           Perseguir();
           PerseguindoAlgo = true;
       }
       else
       {
           Passear();
       }
   }
   if (DistanciaDoPlayer <= DistanciaDeAtacar)
   {
       Atacar();
   }


   //COMANDOS DE PASSEAR
   if (DistanciaDoAIPoint <= 2)
   {
       //Debug.Log(DistanciaDoAIPoint + "<=2");
       AIPointAtual = Random.Range(0, DestinosAleatorios.Length);
       Passear();
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
   if (cronometroAtaque >= TempoPorAtaque && DistanciaDoPlayer <= DistanciaDeAtacar)
   {
       atacandoAlgo = true;
       cronometroAtaque = 0;
       //PLAYER.VIDA = PLAYER.VIDA - DanoDoInimigo;
       Debug.Log("recebeuAtaque");
   }
   else if (cronometroAtaque >= TempoPorAtaque && DistanciaDoPlayer > DistanciaDeAtacar)
   {
       atacandoAlgo = false;
       cronometroAtaque = 0;
       Debug.Log("errou");
   }
   
    }

    void Passear()
    {
        animation.SetInteger("CurrentState", 0);
        //Debug.Log("Passear()");
        if (PerseguindoAlgo == false)
        {
            naveMesh.acceleration = 5;
            naveMesh.speed = VelocidadeDePasseio;
            naveMesh.destination = DestinosAleatorios[AIPointAtual].position;
        }
        else if (PerseguindoAlgo == true)
        {
            contadorPerseguindoAlgo = true;
        }
    }

    void Olhar()
    {
        animation.SetInteger("CurrentState", 1);
        Debug.Log("Olhar()");
        naveMesh.speed = 0;
        transform.LookAt(Player);
    }
    void Perseguir()
    {
        animation.SetInteger("CurrentState", 0);
        Debug.Log("Perseguir()");
        naveMesh.acceleration = 8;
        naveMesh.speed = VelocidadeDePerseguicao;
        naveMesh.destination = Player.position;
    }
    void Atacar()
    {
        animation.SetInteger("CurrentState", 2);
        Debug.Log("Atacar()");
        atacandoAlgo = true;
    }
}