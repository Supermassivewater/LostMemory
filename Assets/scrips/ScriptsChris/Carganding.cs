using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Carganding : MonoBehaviour
{
    public string sceneToLoad;      //Es la escena a la que se va a cargar después
    public RawImage barraDeVida;    //Una barra que va a estar CRECIENDO durante la pantalla de carga
    float anchoBarra;               //Toma el valor del ancho de la barrita
    public TextMeshProUGUI textoConsejo;    //Pa que lean mientras carga
    string texto = "Consejo: ";     //El texto donde se va a mostrar TEXTO hecho en el if del start :)

    void Start()
    {
        int textoAleatorio = Random.Range(1, 11);   //Elige un número aleatorio entre 1 y 11 (el 1 si se incluye, el 11 nop)

        if(textoAleatorio == 1) texto += "El amor eterno dura aproximadamente 3 meses.";
        else if(textoAleatorio == 2) texto += "Lo importante no es ganar, sino hacer perder al otro.";
        else if(textoAleatorio == 3) texto += "La pereza es la madre de todos los vicios. Y como madre... hay que respetarla.";
        else if(textoAleatorio == 4) texto += "Hay dos palabras que te abrirán muchas puertas: 'Tire' y 'Empuje'.";
        else if(textoAleatorio == 5) texto += "De cada 10 personas que miran televisión, cinco son la mitad.";
        else if(textoAleatorio == 6) texto += "Ramírez, estoy hablando solo. Estoy haciendo un monólogo cuando esto debería ser un bi-ólogo.";
        else if(textoAleatorio == 7) texto += "No hay que confundir libertad con libertinaje... a mí me gusta más el libertinaje.";
        else if(textoAleatorio == 8) texto += "Me dijeron que me pagarían de acuerdo a mi capacidad... con esa miseria no me alcanza para nada.";
        else if(textoAleatorio == 9) texto += "¡Gunter! ¡¿Te volviste reguetonero?!";
        else if(textoAleatorio == 10) texto += "Gunter, ¿De verdad me amas? (CUEEEK) Es retórico Gunter >:(";

        textoConsejo.text = texto;  //Se coloca el texto en el string texto

        anchoBarra = barraDeVida.rectTransform.sizeDelta.x;   //Toma el valor actual del ancho de la barra (100%)
        barraDeVida.rectTransform.sizeDelta = new Vector2(0, barraDeVida.rectTransform.sizeDelta.y);    //Hace que el ancho de la barra sea de 0%
        StartCoroutine(LoadSceneAsync());   //Ejecuta una Corutina, o función que trabaja con tiempo
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);    //Guarda la acción de cargar la escena deseada en la variable asyncLoad. NO la realiza
        asyncLoad.allowSceneActivation = false; //Esto hace que no se cargue la siguiente escena

        float progreso = 0; //Valor que se va a estar incrementando para el ancho de la barra
        float progresoBarra;

        while(progreso < 1){    //Mientras el progreso de la barra sea menor al 100%
            progreso += Time.deltaTime * 0.3f;  //El valor que va a tomar el ancho de la barra va a crecer de poco en poco

            progresoBarra = Mathf.Clamp01(progreso / 1);    //Hace que el valor de progresoBarra se limite entre 0 y 1
            barraDeVida.rectTransform.sizeDelta = new Vector2(progresoBarra * anchoBarra, barraDeVida.rectTransform.sizeDelta.y); //Actualiza el ancho de la barra con el valor de progresoBarra

            yield return null;  //hace que regrese al while sin esperar tiempo
        }

        asyncLoad.allowSceneActivation = true;  //Cambia a la escena
    }
}