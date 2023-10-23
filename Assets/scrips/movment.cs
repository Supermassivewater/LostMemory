using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/* Maneja el movimiento del jugador. Permite al jugador moverse hacia adelante, atrás, izquierda y derecha, saltar y ajustar la velocidad de movimiento al presionar la tecla Shift. 
También maneja la gravedad y la detección de colisiones con el suelo. Este script se adjunta al objeto del jugador.*/
public class movment : MonoBehaviour
{
    public CharacterController characterController;

    public float VelocidadMove = 5f;
    private float gravity = -9.81f;
    Vector3 velocity;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    public float jumpHeight= 3;

    public bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position,sphereRadius,groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Obtener la entrada del teclado.
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        //calcular el valorsillo
        Vector3 Moving = transform.right * MoveHorizontal + transform.forward * MoveVertical;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            VelocidadMove = VelocidadMove * 2;
        }else if(Input.GetKeyUp(KeyCode.LeftShift)){
            VelocidadMove = VelocidadMove / 2;
        }

        characterController.Move(Moving * VelocidadMove * Time.deltaTime);  
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
