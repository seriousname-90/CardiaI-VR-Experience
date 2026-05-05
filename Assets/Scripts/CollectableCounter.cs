using UnityEngine;
using System.Collections.Generic;

public class CollectableCounter : MonoBehaviour
{
    public GameObject objetosSeleccionable; // El objeto a agarrar

    [Header("Materiales para los indicadores")]
    public Material materialActivado;       
    public Material materialCompletado;     
    public GameObject indicador;              

    [Header("Activación al completar")]
    public GameObject botonAActivar;        
    public AudioManager audioManager;
    public AudioClip sonidoIndicador;        
    public AudioClip exito;                
    public AudioSource audiosource;
    
    private bool yaCompletado = false; // Controla si ya se completó todo
    private bool audioIndicadorReproducido = false; // Controla el audio del indicador

    // Llama a este método desde el evento OnSelectEntered del objeto
    public void RegistrarAgarre(GameObject objetoAgarrado)
    {
        // Si ya se completó, no hacer nada
        if (yaCompletado) return;
        
        // Si aún no se ha reproducido el audio del indicador
        if (!audioIndicadorReproducido)
        {
            // Cambiar material del indicador
            Renderer renderer = indicador.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = materialActivado;

            // Reproducir audio del indicador UNA SOLA VEZ
            audiosource.PlayOneShot(sonidoIndicador);
            audioIndicadorReproducido = true;
            
            // Llamar a completado
            Completado();
        }
    }

    void Completado()
    {
        if (yaCompletado) return;

        Debug.Log("¡Completado! Activando botón y reproduciendo audio");
        
        // Cambiar material a completado
        Renderer renderer = indicador.GetComponent<Renderer>();
        if (renderer != null)
            renderer.material = materialCompletado;
   
        // Reproducir audio de éxito
        audiosource.PlayOneShot(exito);
        
        // Activar el botón
        if (botonAActivar != null)
            botonAActivar.SetActive(true);
        
        // Reproducir locución (con null check mejorado)
        if (audioManager != null && audioManager.audioSource != null) 
        {
            audioManager.audioSource.Stop();
            audioManager.ReproducirLocucion(2);
        }

        yaCompletado = true;
    }
}