using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Locuciones por estado del botón")]
    public AudioClip[] locucionesBoton; 
    [Header("Configuración inicial")]
    public AudioSource audioSource;
    public GameObject boton; // Botón que se activará después de la primera locución

    void Start()
    {        // Verificar que el AudioSource esté asignado
        if (audioSource == null)        {
            Debug.LogError("AudioSource no asignado en AudioManager");
        }
    }

    IEnumerator ActivarBotonCuandoTermine(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        boton.SetActive(true);
    }
    public void ReproducirLocucion(int indice)
    {
        if (locucionesBoton == null || indice >= locucionesBoton.Length)
        {
            Debug.LogWarning($"No hay locución configurada para el índice {indice}");
            return;
        }

        if (locucionesBoton[indice] != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(locucionesBoton[indice]);
            if (indice == 0)
                StartCoroutine(ActivarBotonCuandoTermine(locucionesBoton[indice]));
                Debug.Log("Esperando para activar el botón...");
            Debug.Log($"Reproduciendo locución {indice}: {locucionesBoton[indice].name}");
        }
    }
}