using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class shot : MonoBehaviour
{
    // Referencias a otros objetos y componentes
    public Transform spawnPoint; // Punto desde donde se disparar� la bala
    public GameObject bullet; // Prefab de la bala que se disparar�
    public TextMeshProUGUI bulletCounterText; // Referencia al componente TextMeshPro para mostrar el contador de balas

    // Variables relacionadas con el disparo
    public float shotForce = 1000f; // Fuerza con la que se disparar� la bala
    public float coolDown = 0.5f; // Tiempo de espera entre disparos
    private float shotRateTime = 0; // Controla el tiempo entre disparos

    // Variables para el contador de balas
    public int totalBullets = 15; // Total de balas cuando el cargador est� lleno
    public int remainingBullets; // Balas restantes en el cargador

    // Variables para la recarga
    public float reloadTime = 2f; // Tiempo que tarda en recargarse
    private bool isReloading = false; // Indica si la pistola est� recargando

    //barra de municion
    public Image bulletBar;

    void Start()
    {
        // Inicializaci�n
        Debug.Log("Hola");
        remainingBullets = totalBullets; // Inicializa las balas restantes con el total
        UpdateBulletCounter(); // Actualiza el contador al inicio
    }

    void Update()
    {
        // Si la pistola est� recargando, no permitir disparar
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        // Si se presiona el bot�n del rat�n y hay balas restantes
        if (Input.GetMouseButtonDown(0) && remainingBullets > 0)
        {
            // Si ha pasado el tiempo de espera entre disparos
            if (Time.time > shotRateTime)
            {
                Shoot(); // Disparar
            }
        }

        // Si las balas llegan a 0, iniciar la recarga
        if (remainingBullets <=0)
        {
            StartCoroutine(Reload());
        }

    }

    void OnCollisionEnter(Collision col)
    {
        // Se ejecuta cuando el objeto con este script colisiona con otro objeto
        Debug.Log("Chocaste");
    }

    void UpdateBulletCounter()
    {
        // Actualiza el texto del contador de balas
        bulletCounterText.text = $"{remainingBullets}/{totalBullets} balas";

        // Actualiza la barra de porcentaje
        float percentage = (float)remainingBullets / totalBullets;
        bulletBar.rectTransform.sizeDelta = new Vector2(350 * percentage, 20);
    }

    void Shoot()
    {


        // Funci�n para disparar una bala
        remainingBullets--; // Disminuye las balas restantes
        GameObject newBullet;
        newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation); // Crea una nueva bala
        newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce); // Aplica una fuerza para dispararla

        shotRateTime = Time.time + coolDown; // Establece el pr�ximo tiempo en el que se podr� disparar
        Destroy(newBullet, 5); // Destruye la bala despu�s de 5 segundos

        
        UpdateBulletCounter(); // Actualiza el contador
    }

    // Corutina de recarga
    IEnumerator Reload()
    {
        isReloading = true; // Indica que est� recargando
        if (remainingBullets <= 0) remainingBullets = 1;
        float reloadTime2 = reloadTime / remainingBullets;
        // Recarga visualmente, incrementando desde remainingBullets hasta totalBullets
        float reloadSpeed = reloadTime2 / (totalBullets - remainingBullets); // Tiempo que tarda en recargar cada bala
        for (int i = remainingBullets + 1; i <= totalBullets; i++)
        {
            remainingBullets = i; // Incrementa las balas restantes
            UpdateBulletCounter(); // Actualiza el contador y la barra de porcentaje
            yield return new WaitForSeconds(reloadSpeed); // Espera antes de recargar la siguiente bala
        }
        reloadTime2 = reloadTime;
        isReloading = false; // Indica que ha terminado de recargar
    }
}
