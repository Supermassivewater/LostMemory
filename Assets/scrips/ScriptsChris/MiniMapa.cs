using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    [SerializeField]
    private Transform target;   //Es la posición del jugador

    void Update()
    {
        gameObject.transform.position = new Vector3(target.position.x, target.position.y + 6, target.position.z);   //Sigue al jugador desde arriba
    }
}
