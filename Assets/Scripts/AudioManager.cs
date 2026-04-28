using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Locuciones por estado del botón")]
    public AudioClip[] locuciones;
    public AudioSource audioSource;
    
    [Header("Configuración inicial")]
    public float delayInicial = 8f;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Reproducir locución inicial con delay
        if (locuciones != null && locuciones.Length > 0)
            StartCoroutine(ReproducirConDelay(0, delayInicial));
    }

    IEnumerator ReproducirConDelay(int indice, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReproducirLocucion(indice);
    }

    public void ReproducirLocucion(int indice)
    {
        if (locuciones == null || indice >= locuciones.Length)
        {
            Debug.LogWarning($"No hay locución configurada para el índice {indice}");
            return;
        }

        if (locuciones[indice] != null && audioSource != null)
        {
            audioSource.PlayOneShot(locuciones[indice]);
            Debug.Log($"Reproduciendo locución {indice}: {locuciones[indice].name}");
        }
    }
}