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
    [Header("---------------follow Player?-------------------")]
    public bool followPlayer;
    //contador de veces que a golpiado al enemigo
    private int hitCount = 0;

    private float distanceToPlayer;
    public float distanceToFollowPlayer = 10;
    // Start is called before the first frame update
    void Start()
    {
        destinations = GameObject.FindGameObjectsWithTag("DestinationTag").Select(go => go.transform).ToArray();
        agent.destination = destinations[0].transform.position;
        player = FindObjectOfType<movment>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {


        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= distanceToFollowPlayer && followPlayer)
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

    // que desaparesca el enemigo
    public void TakeDamage(int damage)
    {
        hitCount += damage;

        if (hitCount >= 5)
        {
            Destroy(gameObject); // Destruye el enemigo después de recibir 5 disparos
        }
    }

    public void FollowPlayer()
    {
        agent.destination = player.transform.position;
    }
}
