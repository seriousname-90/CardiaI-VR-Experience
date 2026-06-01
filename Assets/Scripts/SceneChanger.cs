using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // REQUISITO: Necesario para usar Corrutinas (IEnumerator)

public class SceneChanger : MonoBehaviour
{
    [Header("Configuración")]
    public string sceneName;

    [Header("Cambio Automático (Opcional)")]
    // Si marcas esto, la cuenta atrás iniciará sola al entrar a la escena
    public bool cambiarAutomaticoAlEntrar = false;
    public float segundosDeEspera = 3f;

    void Start()
    {
        // Si la casilla está marcada, arranca la cuenta atrás de inmediato
        if (cambiarAutomaticoAlEntrar)
        {
            CambiarEscenaConRetraso(segundosDeEspera);
        }
    }
    
    public void CambiarEscena()
    {
        // Guardar la escena actual como "última escena"
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        
        // Cambiar a la nueva escena
        SceneManager.LoadScene(sceneName);
    }
    
    public void CambiarEscenaHeart()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("HeartActivation");
    }
    
    public void CambiarEscenaResultados()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("AppResults");
    }

    // NUEVA FUNCIÓN: Se puede llamar desde un botón o dejar que actúe sola
    public void CambiarEscenaConRetraso(float segundos)
    {
        StartCoroutine(EsperaYCambia(segundos));
    }

    private IEnumerator EsperaYCambia(float segundos)
    {
        // Detiene la ejecución aquí durante los segundos indicados
        yield return new WaitForSeconds(segundos);

        // Ejecuta el flujo normal de guardado y carga
        CambiarEscena();
    }
}