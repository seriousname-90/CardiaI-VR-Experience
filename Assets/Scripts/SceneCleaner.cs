using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneCleaner : MonoBehaviour
{
    [Header("Configuración")]
    public string escenaQueNecesitaLimpieza = "HeartActivation"; // El nombre de tu escena 4
    public bool limpiarAlCargar = true;
    public bool limpiarAntesDeCargar = true;

    void Start()
    {
        if (limpiarAlCargar)
        {
            // Limpiar inmediatamente al cargar esta escena
            StartCoroutine(LimpiarRecursos());
        }
    }

    // Este método se llama DESPUÉS de que la escena se haya cargado completamente
    IEnumerator LimpiarRecursos()
    {
        // Esperar un frame para que Unity termine de cargar todo
        yield return null;
        
        // Forzar liberación de recursos no usados
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        
        Debug.Log("[SceneCleaner] Recursos liberados después de cargar la escena.");
    }

    // Este método se llama ANTES de cambiar a la escena problemática
    public static void LimpiarAntesDeCambiar(string nombreEscena)
    {
        // Buscar el SceneCleaner en la escena actual
        SceneCleaner cleaner = FindFirstObjectByType<SceneCleaner>();
        if (cleaner != null)
        {
            cleaner.StartCoroutine(cleaner.LimpiarYCambiar(nombreEscena));
        }
        else
        {
            // Si no hay cleaner, hacer limpieza básica
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            SceneManager.LoadScene(nombreEscena);
        }
    }

    IEnumerator LimpiarYCambiar(string nombreEscena)
    {
        if (limpiarAntesDeCargar)
        {
            // Liberar recursos antes de cargar
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            Debug.Log("[SceneCleaner] Recursos liberados ANTES de cambiar de escena.");
            
            yield return null; // Esperar un frame
        }
        
        // Cambiar de escena
        SceneManager.LoadScene(nombreEscena);
    }
}