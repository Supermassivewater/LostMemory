using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrojarGranada : MonoBehaviour
{
    public GameObject standardGrenadePrefab;
    public GameObject freezingGrenadePrefab;
    public Transform throwPoint;
    public float standardGrenadeCooldown = 20f;
    public float freezingGrenadeCooldown = 10f;
    public Camera playerCamera; // Variable pública para la cámara del jugador.

    private float standardGrenadeLastThrowTime = -50f;
    private float freezingGrenadeLastThrowTime = -50f;

    private GameObject currentGrenade;
    public TextMeshProUGUI cooldownText;

    public string standardGrenadeName = "Granada";
    public string freezingGrenadeName = "Granada Congeladora";

    void Start()
    {
        currentGrenade = standardGrenadePrefab;
        UpdateCooldownText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && Time.time - GetLastThrowTime() >= GetCurrentGrenadeCooldown())
        {
            Throw();
            SetLastThrowTime(Time.time);
            UpdateCooldownText();
        }

        if (Time.time - GetLastThrowTime() < GetCurrentGrenadeCooldown())
        {
            float remainingTime = GetCurrentGrenadeCooldown() - (Time.time - GetLastThrowTime());
            cooldownText.text = GetCurrentGrenadeName() + " disponible en: " + remainingTime.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = GetCurrentGrenadeName() + " Disponible";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentGrenade != standardGrenadePrefab)
        {
            currentGrenade = standardGrenadePrefab;
            UpdateCooldownText();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentGrenade != freezingGrenadePrefab)
        {
            currentGrenade = freezingGrenadePrefab;
            UpdateCooldownText();
        }
    }

    float GetCurrentGrenadeCooldown()
    {
        if (currentGrenade == standardGrenadePrefab)
        {
            return standardGrenadeCooldown;
        }
        else if (currentGrenade == freezingGrenadePrefab)
        {
            return freezingGrenadeCooldown;
        }

        return 0f; // Valor predeterminado
    }

    void UpdateCooldownText()
    {
        if (Time.time - GetLastThrowTime() < GetCurrentGrenadeCooldown())
        {
            float remainingTime = GetCurrentGrenadeCooldown() - (Time.time - GetLastThrowTime());
            cooldownText.text = GetCurrentGrenadeName() + " disponible en: " + remainingTime.ToString("F1") + "s";
        }
        else
        {
            cooldownText.text = GetCurrentGrenadeName() + " Disponible";
        }
    }

    string GetCurrentGrenadeName()
    {
        if (currentGrenade == standardGrenadePrefab)
        {
            return standardGrenadeName;
        }
        else if (currentGrenade == freezingGrenadePrefab)
        {
            return freezingGrenadeName;
        }

        return "Granada Desconocida"; // O un valor predeterminado si no se encuentra ninguna granada
    }

    void Throw()
    {
        // Crea una instancia de la granada desde el prefab en el punto de origen
        GameObject grenade = Instantiate(currentGrenade, throwPoint.position, throwPoint.rotation);

        // Obtén el Rigidbody de la granada
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Obtiene la dirección de la cámara del jugador, que siempre apunta en la dirección del eje z.
        Vector3 cameraForward = playerCamera.transform.forward;

        // Aplica la fuerza de lanzamiento en la dirección de la cámara.
        Vector3 throwForce = cameraForward * 10f; // Fuerza de lanzamiento común.

        if (currentGrenade == freezingGrenadePrefab)
        {
            throwForce = cameraForward * 14f; // Cambia la fuerza de lanzamiento para la granada congeladora.
        }

        rb.AddForce(throwForce, ForceMode.VelocityChange);
    }

    float GetLastThrowTime()
    {
        if (currentGrenade == standardGrenadePrefab)
        {
            return standardGrenadeLastThrowTime;
        }
        else if (currentGrenade == freezingGrenadePrefab)
        {
            return freezingGrenadeLastThrowTime;
        }

        return 0f;
    }

    void SetLastThrowTime(float time)
    {
        if (currentGrenade == standardGrenadePrefab)
        {
            standardGrenadeLastThrowTime = time;
        }
        else if (currentGrenade == freezingGrenadePrefab)
        {
            freezingGrenadeLastThrowTime = time;
        }
    }
}
