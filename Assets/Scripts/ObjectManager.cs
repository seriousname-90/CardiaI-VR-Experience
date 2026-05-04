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
    public float delay = 3f;
    public GameObject button; // Referencia al botón para cambiar su texto
    public TMP_Text buttonText; // Texto del botón para mostrar el estado
    public Canvas myCanvas;

    public void CambiarBoton(int indice)
    {
        
        if (indice == 0 && buttonText != null)
        {
            buttonText.text = "Siguiente";
        }
        myCanvas.transform.position = new Vector3(0.434f, 0.665f, 0.563f);
        myCanvas.transform.rotation = Quaternion.Euler(12.079f, 27.812f, 0f);
        // Aquí puedes cambiar el texto del botón o su apariencia
        Debug.Log("CambiarBoton llamado. Aquí puedes actualizar el estado del botón.");
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
        yield return new WaitForSeconds(delay);
        Debug.Log("Cambiando a la escena Assemble.");
        SceneManager.LoadScene("Assemble");
    }
}