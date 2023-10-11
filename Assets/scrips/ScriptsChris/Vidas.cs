using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidas : MonoBehaviour
{
    public GameObject[] vidas = new GameObject[3];  //Crea un array con 3 espacios para las vidas
    int contarVidas = 3;

    //Da una vida hasta tener 3
    public void DarVida(){
        if(contarVidas < 3){
            vidas[contarVidas].SetActive(true);
            contarVidas++;
        }
    }

    //Quita la vida o te manda al Menú principal
    public void QuitarVida(){
        if(contarVidas > 1){
            contarVidas--;
            vidas[contarVidas].SetActive(false);
        }
        else{
            SceneManager.LoadScene("Inicio");
        }
    }

}
