using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using TMPro;

public class ObjectManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    [Header("Objetos a activar (primera presión)")]
    public GameObject[] objetosAActivar;
    public TMP_Text buttonText; // Texto del botón para mostrar el estado
    public Canvas myCanvas; // Referencia al Canvas del botón
    
    [Header("Gestión de Pantallas")]
    public GameObject[] pantallas; // Arrastra aquí Screen1, Screen2, etc.
    private int pantallaActual = -1;

    public void CambiarBoton(int indice)
    {
        StartCoroutine(MoverBotonConRetraso(indice));
    }

    private IEnumerator MoverBotonConRetraso(int indice)
    {
        // Esperamos un tiempo breve para que la animación de "encogerse" 
        // del script UIAnimate haya avanzado lo suficiente o terminado.
        yield return new WaitForSeconds(0.5f);  

        if (indice == 0 && buttonText != null)
        {
            buttonText.text = "Siguiente";
        }

        // Ahora que el botón es invisible o casi invisible, lo movemos
        myCanvas.transform.position = new Vector3(0.434f, 0.665f, 0.563f);
        myCanvas.transform.rotation = Quaternion.Euler(12.079f, 27.812f, 0f);
        
        Debug.Log("Botón movido mientras estaba oculto.");
    }

    public void ActivarObjetos()
    {

        foreach (GameObject obj in objetosAActivar)
        {
            if (obj != null) obj.SetActive(true);
            Debug.Log("Activando: " + obj.name);
        }
    }

    public IEnumerator CambiarEscenaLobby()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos antes de cambiar la escena
        Debug.Log("Cambiando a la escena Assemble.");
        SceneManager.LoadScene("Assemble");
    }

    public void AvanzarPantalla()
    {
        // 1. Apagamos la pantalla actual si existe
        if (pantallaActual >= 0 && pantallaActual < pantallas.Length)
        {
            // Animamos la pantalla vieja para que desaparezca
            UIAnimate anim = pantallas[pantallaActual].GetComponent<UIAnimate>();
            if (anim != null) 
                StartCoroutine(anim.AnimarDesaparecer());
            else
                pantallas[pantallaActual].SetActive(false);
        }

        // 2. Avanzamos el índice y encendemos la que sigue
        pantallaActual++;
        if (pantallaActual < pantallas.Length && pantallas[pantallaActual] != null)
            pantallas[pantallaActual].SetActive(true);
    }

    public void CambiarEscenaHeart()
    {
        Debug.Log("Cambiando a la escena Heart.");
        SceneManager.LoadScene("HeartActivation");
    }

    public void CambiarEscenaResultados()
    {
        Debug.Log("Cambiando a la escena Heart.");
        SceneManager.LoadScene("AppResults");
    }
}