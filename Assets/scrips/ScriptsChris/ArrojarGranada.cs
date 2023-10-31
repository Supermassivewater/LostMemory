using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrojarGranada : MonoBehaviour
{
    public GameObject grenadePrefab; // Prefab de la granada que se arrojará
    public Transform throwPoint; // Punto de origen desde donde se arrojará la granada
    public float throwForce = 9f; // Fuerza con la que se arrojará la granada
    public float verticalForce = 6f; // Fuerza vertical para lanzar la granada hacia arriba
    public float cooldownTime = 20f; // Tiempo de retraso entre lanzamientos

    private float lastThrowTime = -50f; // Guarda el tiempo del último lanzamiento

    public TextMeshProUGUI cooldownText; // Asigna el elemento de texto en el Inspector

    void Start()
    {
        cooldownText.text = "Granada Disponible"; // Mensaje inicial
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && Time.time - lastThrowTime >= cooldownTime)
        {
            Throw(); // Llama a la función Throw si ha pasado suficiente tiempo desde el último lanzamiento
            lastThrowTime = Time.time; // Actualiza el tiempo del último lanzamiento
        }

        // Actualiza el texto del tiempo de retraso
        if (Time.time - lastThrowTime < cooldownTime)
        {
            float remainingTime = cooldownTime - (Time.time - lastThrowTime);
            cooldownText.text = "Granada disponible en: " + remainingTime.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = "Granada Disponible";
        }
    }

    void Throw()
    {
        // Crea una instancia de la granada desde el prefab en el punto de origen
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

        // Obtén el Rigidbody de la granada
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Calcula la fuerza para lanzar la granada hacia adelante
        Vector3 forwardForce = throwPoint.forward * throwForce;

        // Calcula la fuerza vertical para lanzar la granada hacia arriba
        Vector3 upForce = throwPoint.up * verticalForce;

        // Aplica ambas fuerzas al Rigidbody para el lanzamiento curvado
        rb.AddForce(forwardForce + upForce, ForceMode.VelocityChange);
    }
}
