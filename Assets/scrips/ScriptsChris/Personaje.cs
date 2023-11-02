using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour     //Esto script se piensa que se coloca en el jugador
{
    //Barra de vida
    int saludActual;
    public RawImage barraDeVida;
    public RawImage barraDisminuye;
    float anchoBarra;
    float velocidadDisminucion = 0.6f;
    bool disminuyendo = false;

    //Muerte por caída y suma y resta de vidas
    Vector3 posicionInicial;

    public Vidas vidas;

    //Dinero
    int dinero = 0;
    public TextMeshProUGUI textoDinero;

    //Temporizador
    public TextMeshProUGUI textoTemporizador;
    float tiempo = 90f;
    bool desactivarTiempo = true;

    //Sonidos
    public AudioClip SmasVida;
    public AudioClip SmenosVida;
    public AudioClip Sdinero;
    public AudioClip ScaerMorir;
    AudioSource reproducir;

    //Ganar
    public GameObject textoGanar;
    public Efectos efectos;

    void Start()
    {
        saludActual = 100;
        anchoBarra = barraDeVida.rectTransform.sizeDelta.x; //Toma el ancho de la barra de vida (100%)
        posicionInicial = transform.position;   //Guarda la posición donde aparece el personaje en una variable de Vector3
        textoDinero.text = "Dinero: 0";     //Estable el dinero en 0
        reproducir = GetComponent<AudioSource>();   //Toma el AudioSource del jugador
        textoGanar.SetActive(false);    //Desactiva el texto de ganar la partida
    }

    void Update()
    {
        if (barraDisminuye.rectTransform.sizeDelta.x > barraDeVida.rectTransform.sizeDelta.x && disminuyendo)
        {
            /* Mathf.Lerp es una función en Unity que realiza una interpolación lineal entre dos valores. Interpolar significa 
            calcular un valor intermedio basado en dos valores de inicio y final. Mathf.Lerp se usa comúnmente para realizar 
            transiciones suaves o animaciones entre dos valores a lo largo del tiempo.

            La función Mathf.Lerp toma tres argumentos:

            a: El valor inicial.
            b: El valor final al que deseas llegar.
            t: Un valor entre 0 y 1 que representa la cantidad de interpolación. Cuando t es 0, el resultado será igual a a, 
            y cuando t es 1, el resultado será igual a b. Valores intermedios de t producirán un valor intermedio entre a y b.
            */
            float nuevoAncho = Mathf.Lerp(barraDisminuye.rectTransform.sizeDelta.x, barraDeVida.rectTransform.sizeDelta.x, velocidadDisminucion * Time.deltaTime);
            barraDisminuye.rectTransform.sizeDelta = new Vector2(nuevoAncho, barraDisminuye.rectTransform.sizeDelta.y); //Actualiza el nuevo ancho de la barra que disminuye
        }

        if(desactivarTiempo){
            tiempo -= Time.deltaTime;       //Pal temporizador
            ActualizandoTemporizador();
            if(tiempo <= 0){
                tiempo = 0;
                ActualizandoTemporizador();
                desactivarTiempo = false;   // para que no se meta al if
                Debug.Log("Se terminó el tiempo");
                SceneManager.LoadScene("Inicio");
            }
        }
    }

    private void ModificarSalud(){
        barraDeVida.rectTransform.sizeDelta = new Vector2((anchoBarra / 100) * saludActual, barraDeVida.rectTransform.sizeDelta.y);
        if(barraDisminuye.rectTransform.sizeDelta.x < barraDeVida.rectTransform.sizeDelta.x && disminuyendo){
            barraDisminuye.rectTransform.sizeDelta = new Vector2((anchoBarra / 100) * saludActual, barraDisminuye.rectTransform.sizeDelta.y);
        }
    }

    private void ActualizandoTemporizador()
    {
        /*Math.FloorToInt es una función que redondea un número decimal hacia abajo al número entero más cercano
        y luego lo convierte en un tipo de dato entero (int). En otras palabras, elimina la parte decimal del número,
        dejando solo la parte entera.*/

        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        int milisegundos = Mathf.FloorToInt((tiempo % 1) * 100);

        textoTemporizador.text = "Tiempo: " + string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("DaVida")){
            reproducir.clip = SmasVida;
            reproducir.Play();
            for (int i = 0; i < 25; i++) saludActual += 1;
            if(saludActual > 100){
                saludActual = 100;
                vidas.DarVida();
            }  

            ModificarSalud();
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag("QuitaVida")){
            reproducir.clip = SmenosVida;
            reproducir.Play();
            for (int i = 0; i < 40; i++) saludActual -= 1;
            if(saludActual <= 0){
                reproducir.clip = ScaerMorir;
                reproducir.Play();
                saludActual = 100;
                vidas.QuitarVida();
            } 

            ModificarSalud();
            other.gameObject.SetActive(false);
            disminuyendo = true;
        }

        if(other.CompareTag("Morir")){
            reproducir.clip = ScaerMorir;
            reproducir.Play();
            saludActual = 100;
            ModificarSalud();
            vidas.QuitarVida();
        }

        if(other.CompareTag("Dinero")){
            reproducir.clip = Sdinero;
            reproducir.Play();
            dinero++;
            textoDinero.text = "Pesos: " + dinero.ToString();
            other.gameObject.SetActive(false);

            if(dinero == 15){
                StartCoroutine(Ganar());
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Morir")){
            transform.position = posicionInicial;
        }
    }

    IEnumerator Ganar(){
        textoGanar.SetActive(true);
        while(efectos.opacidad.a <= 1){
            efectos.opacidad.a += 0.005f;
            efectos.opacidadInicio.color = efectos.opacidad;
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("Inicio");
    }
}