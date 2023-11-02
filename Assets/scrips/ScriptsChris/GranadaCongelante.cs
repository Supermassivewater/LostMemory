using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadaCongelante : MonoBehaviour
{
    public float explosionRadius = 10f;      // Radio de explosión de la granada.
    public float detonationTime = 1.5f;     // Tiempo antes de que la granada explote automáticamente.
    public GameObject explosionEffect;      // Prefab para el efecto de explosión.

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
                        // Llama a un nuevo método "Congelar" del enemigo para congelarlo.
                        enemyScript.Congelar();
                        // Llama al método "TakeDamage" del enemigo para sumar 1 a su variable "hitCount".
                        enemyScript.TakeDamage(1);
                    }
                }
            }

            // Destruye la granada después de la explosión.
            Destroy(gameObject);
        }
    }
}
