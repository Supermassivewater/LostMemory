using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
