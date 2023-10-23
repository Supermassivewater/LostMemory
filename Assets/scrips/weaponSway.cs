using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Agrega un efecto de balanceo a las armas del jugador para dar una sensación de realismo. 
El arma se inclina ligeramente en respuesta a los movimientos del mouse, lo que simula el movimiento natural del arma al apuntar. Este script se adjunta al arma del jugador.*/
public class weaponSway : MonoBehaviour
{
    private Quaternion startRotation;
    public float SwayAmount = 8;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        sway();
    }

    private void sway()
    {
        float masueX = Input.GetAxis("Mouse X");
        float masuey = Input.GetAxis("Mouse Y");

        Quaternion xAngle = Quaternion.AngleAxis(masueX * -1.25f, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(masuey * 1.25f, Vector3.left);

        Quaternion targetRotation = startRotation * xAngle * yAngle;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * SwayAmount);
    }
}
