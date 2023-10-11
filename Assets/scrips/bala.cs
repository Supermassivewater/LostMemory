using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("destroy")) // Verifica si la colisión es con un objeto etiquetado como "Cubo".
        {

            Destroy(col.gameObject); // Destruye el cubo.
            Destroy(gameObject); // Destruye la bala.
        }
        else if (col.gameObject.CompareTag("enemy")) // Verifica si la colisión es con un objeto etiquetado como "destroy".
        {
            col.gameObject.GetComponent<AI>().TakeDamage(1); // Llama a la función TakeDamage del enemigo
            Destroy(gameObject); // Destruye la bala.
        }
    }

}
