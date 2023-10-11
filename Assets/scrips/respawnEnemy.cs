using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que quieres generar
    public Transform[] spawnPoints; // Puntos donde se generarán los enemigos
    public float spawnRate = 5f; // Tiempo en segundos entre spawns

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time; // Inicializa el tiempo del próximo spawn
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; // Establece el próximo tiempo de spawn
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length); // Elige un punto de spawn aleatorio
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation); // Genera el enemigo en el punto de spawn elegido
    }
}
