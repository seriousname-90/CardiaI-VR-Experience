using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Locuciones")]
    public AudioClip[] locuciones; 
    [Header("Configuración inicial")]
    public AudioSource audioSource;
    public GameObject boton; // Arrastra aquí el objeto "Button" de la jerarquía
    public SubtitlePlayer subtitlePlayer;
    public SubtitleSequence[] secuencias; // Arrastra aquí la secuencia de subtítulos para la locución inicial

    private int locucionActual = 0;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado en AudioManager");
        }
    }

    IEnumerator ActivarBotonCuandoTermine(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        if (boton != null)
        {
            boton.SetActive(true);
            Debug.Log("Botón reactivado tras finalizar el audio.");
        }
    }

    public void ReproducirLocucion(int indice)
    {
        if (locuciones == null || indice >= locuciones.Length || indice != locucionActual) return;

        if (locuciones[indice] != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(locuciones[indice]);

            // Reactivamos el botón después de la locución inicial (0) 
            // Y después de la locución de éxito de la actividad (2)
            if (indice == 0) 
            {
                StartCoroutine(ActivarBotonCuandoTermine(locuciones[indice]));
            }
            if (subtitlePlayer != null && secuencias != null && indice < secuencias.Length)
            {
                subtitlePlayer.PlaySequence(secuencias[indice]);
            }

            Debug.Log($"Reproduciendo locución {indice}: {locuciones[indice].name}");
            locucionActual++;
        }
    }

    public void ReproducirLocucionConDelay(int indice, float delay)
    {
        StartCoroutine(ReproducirConDelay(indice, delay));
    }
    
    IEnumerator ReproducirConDelay(int indice, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReproducirLocucion(indice);
    }
}