using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Locuciones por estado del botón")]
    public AudioClip[] locucionesBoton;

    [Header("Referencias de Interfaz (NUEVO)")]
    public GameObject panelBienvenida;
    public GameObject panelInstrucciones;

    [Header("Locuciones con tiempo específico")]
    public AudioClip loc_exito;
    public AudioClip lobby2;

    [Header("Configuración inicial")]
    public GameObject button;
    public AudioSource audioSource;
    public float delayInicial = 5f;
    public float tiempo_loc_exito = 41f;
    public float tiempo_lobby2 = 45f;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Estado inicial de la pantalla
        if (panelBienvenida != null) panelBienvenida.SetActive(true);
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);

        if (locucionesBoton != null && locucionesBoton.Length > 0)
            StartCoroutine(ReproducirConDelay(0, delayInicial));
    }

    IEnumerator ReproducirConDelay(int indice, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReproducirLocucion(indice);

        yield return new WaitForSeconds(tiempo_loc_exito);
        if (loc_exito != null)
        {
            audioSource.PlayOneShot(loc_exito);
            Debug.Log("Reproduciendo locución de éxito (perfecto)");
        }

        yield return new WaitForSeconds(tiempo_lobby2 - tiempo_loc_exito);

        if (lobby2 != null)
        {
            audioSource.PlayOneShot(lobby2);
            Debug.Log("Reproduciendo locución lobby2 (40 seg)");

            button.SetActive(true);

            // Cambio de paneles al aparecer el botón
            if (panelBienvenida != null) panelBienvenida.SetActive(false);
            if (panelInstrucciones != null) panelInstrucciones.SetActive(true);
        }
    }

    public void ReproducirLocucion(int indice)
    {
        if (locucionesBoton == null || indice >= locucionesBoton.Length)
        {
            Debug.LogWarning($"No hay locución configurada para el índice {indice}");
            return;
        }

        if (locucionesBoton[indice] != null && this.audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(locucionesBoton[indice]);
            Debug.Log($"Reproduciendo locución {indice}: {locucionesBoton[indice].name}");
        }
    }
}