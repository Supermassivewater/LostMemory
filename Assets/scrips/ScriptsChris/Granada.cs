using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public float explosionRadius = 5f;      // Radio de explosión de la granada.
    public float explosionForce = 1000f;   // Fuerza de la explosión.
    public float detonationTime = 2.5f;      // Tiempo antes de que la granada explote automáticamente.
    public GameObject explosionEffect;     // Prefab para el efecto de explosión.

    private bool hasExploded = false;

    void Start()
    {
        // Programa una llamada al método "Detonate" después del tiempo de detonación especificado.
        Invoke("Detonate", detonationTime);
    }

    void Detonate()
    {
        if (!hasExploded)
        {
            hasExploded = true;

            // Crea un efecto de explosión si se ha asignado un prefab.
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Detecta objetos dentro del radio de explosión.
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider hitCollider in colliders)
            {
                if (hitCollider.CompareTag("enemy")) // Comprueba si el objeto tiene el tag "enemy" (debes asignar este tag a tus enemigos).
                {
                    // Obtiene el componente AI del enemigo.
                    AI enemyScript = hitCollider.GetComponent<AI>();
                    if (enemyScript != null)
                    {
                        // Llama al método "TakeDamage" del enemigo para sumar 3 a su variable "hitCount".
                        enemyScript.TakeDamage(3);
                    }
                }
            }

            // Destruye la granada después de la explosión.
            Destroy(gameObject);
        }
    }
}