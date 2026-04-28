using UnityEngine;
using System.Collections.Generic;

public class CollectableCounter : MonoBehaviour
{
    [Header("Objetos Selecionables")]
    public GameObject[] objetosSeleccionables; // Los 3 objetos a agarrar

    [Header("Materiales para los indicadores")]
    public Material materialActivado;       // Material cuando se agarra el objeto
    public Material materialCompletado;       // Material cuando se agarra el objeto
    public GameObject[] indicadores;              // Los 3 palos (mismo orden que objetos)

    [Header("Activación al completar")]
    public GameObject botonAActivar;        // Botón que se activa
    public AudioSource audioCompletado;     // Audio al completar
    public ObjectManager objectManager;
    public AudioClip indicador;        // Audio al activar indicador
    public AudioClip exito;        // Audio al completar
    private int contador = 0;
    private List<GameObject> objetosAgarrados = new List<GameObject>(); // Lista de control

    void Start()
    {
        // Verificar que los arrays tengan el mismo tamaño
        if (objetosSeleccionables.Length != indicadores.Length)
        {
            Debug.LogError("Los arrays de objetos y palos no tienen el mismo tamaño");
        }
    }

    // Llama a este método desde el evento OnSelectEntered de cada objeto
    public void RegistrarAgarre(GameObject objetoAgarrado)
    {
        // Verificar si ya fue agarrado antes
        if (objetosAgarrados.Contains(objetoAgarrado))
        {
            Debug.Log($"Objeto {objetoAgarrado.name} ya fue agarrado. Ignorando.");
            return;
        }

        // Buscar qué índice tiene este objeto
        for (int i = 0; i < objetosSeleccionables.Length; i++)
        {
            if (objetosSeleccionables[i] == objetoAgarrado)
            {
                objetosAgarrados.Add(objetoAgarrado);
                // Cambiar material del palo correspondiente
                if (indicadores[i] != null && materialActivado != null)
                {
                    Renderer renderer = indicadores[i].GetComponent<Renderer>();
                    if (renderer != null)
                        renderer.material = materialActivado;
                        audioCompletado.PlayOneShot(indicador);
                }
                
                contador++;
                Debug.Log($"Objeto {i} agarrado. Progreso: {contador}/{objetosSeleccionables.Length}");
                
                break;
            }
        }

        // Verificar si completamos los 3
        if (contador >= objetosSeleccionables.Length)
        {
            Completado();
        }
    }

    void Completado()
    {
        Debug.Log("¡Completado! Activando botón y reproduciendo audio");
        
        for (int i = 0; i < indicadores.Length; i++)
        {
            Renderer renderer = indicadores[i].GetComponent<Renderer>();
            if (renderer != null)
                renderer.material = materialCompletado;
        }
        audioCompletado.PlayOneShot(exito);
        if (botonAActivar != null)
            botonAActivar.SetActive(true);
        
        if (audioCompletado != null)
            objectManager.audioSource.Stop();
            audioCompletado.Play();
    }
}