using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string siguienteEscena;

    public void CambiandoEscena()
    {
        SceneManager.LoadScene(siguienteEscena); //Cambia a la escena colocada en la interfaz de Unity juejuejuejue
    }
}