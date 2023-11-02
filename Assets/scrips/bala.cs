using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Este script maneja el comportamiento de las balas. Cuando una bala colisiona con un objeto etiquetado como "destroy,",
 destruye tanto la bala como el objeto con el que colisionó. Si colisiona con un objeto etiquetado como "enemy," llama a la función "TakeDamage"
  del script AI asociado al enemigo y luego destruye la bala. Este script debe estar adjunto a las balas en el juego. */
public class bala : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("destroy")) // Verifica si la colisi�n es con un objeto etiquetado como "destroy".
        {

            Destroy(col.gameObject); // Destruye el cubo.
            Destroy(gameObject); // Destruye la bala.
        }
        else if (col.gameObject.CompareTag("enemy")) // Verifica si la colisi�n es con un objeto etiquetado como "destroy".
        {
            col.gameObject.GetComponent<AI>().TakeDamage(1); // Llama a la funci�n TakeDamage del enemigo
            Destroy(gameObject); // Destruye la bala.
        }
    }

}
