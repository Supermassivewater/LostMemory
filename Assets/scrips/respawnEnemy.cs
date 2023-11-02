using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Genera enemigos de manera periódica en diferentes puntos de spawn. Utiliza un prefab de enemigo y una matriz de puntos de spawn para generarlos. 
El script determina cuándo se generan nuevos enemigos en función del tiempo. Se utiliza para mantener un flujo constante de enemigos en el juego.*/
public class respawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que quieres generar
    public Transform[] spawnPoints; // Puntos donde se generar�n los enemigos
    public float spawnRate = 5f; // Tiempo en segundos entre spawns

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time; // Inicializa el tiempo del pr�ximo spawn
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; // Establece el pr�ximo tiempo de spawn
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length); // Elige un punto de spawn aleatorio
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation); // Genera el enemigo en el punto de spawn elegido
    }
}
