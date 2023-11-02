using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Efectos : MonoBehaviour
{
    //Esto hace que al cargar la pantalla pase de opacidad 100% a 0%
    public Image opacidadInicio;
    [HideInInspector]
    public Color opacidad;

    void Start()
    {
        opacidad = opacidadInicio.color;    //la variable opacidad toma el color de opacidadInicio que es una imagen arrastrada desde el inspector
        StartCoroutine(PantallaNegra());    //Ejecuta una Corutina o función con tiempo
    }

    IEnumerator PantallaNegra(){
        while(opacidad.a > 0){     //Mientras la opacidad sea mayor a 0
            opacidad.a -= 0.01f;    //Le quita un poquito de opacidad
            opacidadInicio.color = opacidad;    //El valor de opacidad se pasa a la imagen con opacidad
            yield return new WaitForSeconds(0.01f);     //Espera 0.01s antes de repetir el while
        }
    }
}
