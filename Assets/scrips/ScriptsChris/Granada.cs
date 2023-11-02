using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public float explosionRadius = 8f;      // Radio de explosión de la granada.
    public float explosionForce = 1000f;   // Fuerza de la explosión.
    public float detonationTime = 2f;      // Tiempo antes de que la granada explote automáticamente.
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

                if (hitCollider.CompareTag("destroy")) // Verifica si la colisión es con un objeto etiquetado como "destroy".
                {
                    Destroy(hitCollider.gameObject); // Destruye el cubo.
                }

                // Aplica una fuerza de empuje a los objetos dentro del radio de explosión.
                if (hitCollider.attachedRigidbody != null)
                {
                    // Calcula la dirección desde la granada al objeto.
                    Vector3 direction = hitCollider.transform.position - transform.position;

                    // Normaliza la dirección para asegurarse de que tenga una longitud de 1.
                    direction.Normalize();

                    // Aplica la fuerza de empuje desde el centro de la explosión.
                    hitCollider.attachedRigidbody.AddForce(direction * explosionForce);
                }
            }

            // Destruye la granada después de la explosión.
            Destroy(gameObject);
        }
    }
}