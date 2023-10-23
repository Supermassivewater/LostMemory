using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Controla la cámara en primera persona. Permite al jugador mover la vista con el mouse y ajusta la rotación de la cámara en función de los movimientos del jugador.
 Se utiliza para dar al jugador la sensación de control de la vista. Este script se adjunta a la cámara del jugador.*/
public class camara : MonoBehaviour
{
    public float sensitive = 10f;

    public Transform PlayerBody;

    public float xrotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float masueX = Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;
        float masueY = Input.GetAxis("Mouse Y") * sensitive * Time.deltaTime;

        xrotation -= masueY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xrotation, 0, 0);

        PlayerBody.Rotate(Vector3.up * masueX);
    }
}
