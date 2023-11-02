using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform[] destinations;
    public float distanceToFollowPath = 2f;
    private int i = 0;
    private GameObject player;

    private int hitCount = 0;
    private float distanceToPlayer;
    public float distanceToFollowPlayer = 10;
    private bool congelado = false; // Nuevo: indica si el enemigo está congelado.

    void Start()
    {
        destinations = GameObject.FindGameObjectsWithTag("DestinationTag").Select(go => go.transform).ToArray();
        agent.destination = destinations[0].transform.position;
        player = FindObjectOfType<movment>().gameObject;
    }

    void Update()
    {
        if (congelado) // Nuevo: Si el enemigo está congelado, no realices acciones.
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= distanceToFollowPlayer)
        {
            FollowPlayer();
        }
        else
        {
            EnemyPath();
        }
    }

    public void EnemyPath()
    {
        agent.destination = destinations[i].position;
        if (Vector3.Distance(transform.position, destinations[i].position) < distanceToFollowPath)
        {
            if (destinations[i] != destinations[destinations.Length - 1])
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hitCount += damage;
        if (hitCount >= 5)
        {
            Destroy(gameObject);
        }
    }

    public void FollowPlayer()
    {
        agent.destination = player.transform.position;
    }

    public void Congelar()
    {
        congelado = true;
        agent.isStopped = true;
        Invoke("Descongelar", 5f);
    }

    private void Descongelar()
    {
        congelado = false;
        agent.isStopped = false;
    }
}