using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Locuciones por estado del botón")]
    public AudioClip[] locucionesBoton; // 0: inicial, 1: primera acción, 2: segunda acción, 3: tercera acción
    [Header("Locuciones con tiempo específico")]
    public AudioClip loc_exito;           // Audio de "perfecto"
    public AudioClip lobby2;        // Segundo audio 

    
    [Header("Configuración inicial")]
    public GameObject button; // Referencia al botón para detectar su estado   
    public AudioSource audioSource;
    public float delayInicial = 5f;
    public float tiempo_loc_exito = 41f;
    public float tiempo_lobby2 = 45f;
    public CollectableCounter collectableCounter;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Reproducir locución inicial con delay
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
            button.SetActive(true); // Activar el botón después de reproducir lobby2
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
            // ✅ Verificar que el AudioSource del CollectableCounter exista antes de usarlo
            if (collectableCounter != null && collectableCounter.audioCompletado != null)
            {
                collectableCounter.audioCompletado.Stop();
            }
            audioSource.Stop();
            audioSource.PlayOneShot(locucionesBoton[indice]);
            Debug.Log($"Reproduciendo locución {indice}: {locucionesBoton[indice].name}");
        }
    }
}