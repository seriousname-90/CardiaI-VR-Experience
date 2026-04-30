using UnityEngine;
using System.Collections.Generic;

public class CollectableCounter : MonoBehaviour
{
    [Header("Objetos Selecionables")]
    public GameObject objetosSeleccionable; // Los 3 objetos a agarrar

    [Header("Materiales para los indicadores")]
    public Material materialActivado;       // Material cuando se agarra el objeto
    public Material materialCompletado;       // Material cuando se agarra el objeto
    public GameObject indicador;              // Los 3 palos (mismo orden que objetos)

    [Header("Activación al completar")]
    public GameObject botonAActivar;        // Botón que se activa
    public ObjectManager objectManager;
    public AudioManager audioManager;
    public AudioClip sonidoIndicador;        // Audio al activar indicador
    public AudioClip exito;        // Audio al completar
    public AudioSource audiosource; // AudioSource para reproducir los clips
    private bool grabbed;

    // Llama a este método desde el evento OnSelectEntered de cada objeto
    public void RegistrarAgarre(GameObject objetoAgarrado)
    {
        Renderer renderer = indicador.GetComponent<Renderer>();
        if (renderer != null && grabbed == false)
            renderer.material = materialActivado;
            audiosource.PlayOneShot(sonidoIndicador);
            grabbed = true;
        
        Completado();
    }

    void Completado()
    {
        Debug.Log("¡Completado! Activando botón y reproduciendo audio");
        
        Renderer renderer = indicador.GetComponent<Renderer>();
        if (renderer != null)
            renderer.material = materialCompletado;
   
        audiosource.PlayOneShot(exito);
        if (botonAActivar != null)
            botonAActivar.SetActive(true);
        
        if (audiosource != null && grabbed)
            audioManager.audioSource.Stop();
            audioManager.ReproducirLocucion(2); // Locución para completar la acción
            grabbed = false; // Reiniciar para evitar múltiples activaciones
    }
}