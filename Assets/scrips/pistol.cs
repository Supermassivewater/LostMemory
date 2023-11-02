using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Controla la animación y disparo de una pistola. Permite al jugador apuntar y disparar su arma. Al presionar el botón derecho del ratón,
 activa la animación de apuntar, y al soltarlo, activa la animación inversa. Este script se adjunta al arma del jugador.*/
public class pistol : MonoBehaviour
{

    // private Vector3 positionOriginal;
    //public Transform transform;
    public Animator animator; // Cambia el tipo de variable a Animator.
    public float velocidadAnimacion =  1.5f;
    // Start is called before the first frame update
    void Start()
    {
        // positionOriginal = transform.localPosition;
       animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.speed = velocidadAnimacion;
            animator.SetTrigger("Point");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            
            animator.SetTrigger("PointerReverse");
        }
    }
}
