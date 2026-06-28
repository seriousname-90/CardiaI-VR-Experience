using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [Header("Configuración")]
    public string sceneName;
    public SceneFader sceneFader;

    [Header("Cambio Automático (Opcional)")]
    public bool cambiarAutomaticoAlEntrar = false;
    public float segundosDeEspera = 3f;

    void Start()
    {
        if (cambiarAutomaticoAlEntrar)
        {
            CambiarEscenaConRetraso(segundosDeEspera);
        }
    }

    void Awake()
    {
        // Limpia todos los volumes heredados de escenas anteriores
        UnityEngine.Rendering.VolumeManager.instance.ResetMainStack();
        
        // Fuerza limpieza de lightmaps
        LightmapSettings.lightmaps = LightmapSettings.lightmaps;
    }
    
    public void CambiarEscena()
    {
        // Guardar la escena actual como "última escena"
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        
 
        // Cambio normal
        SceneManager.LoadScene(sceneName);
    }
    
    public void CambiarEscenaHeart()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        
        // Siempre usar el limpiador cuando vamos a HeartActivation
        Resources.UnloadUnusedAssets();
    }
    
    public void CambiarEscenaResultados()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("AppResults");
    }

    public void CambiarEscenaConRetraso(float segundos)
    {
        StartCoroutine(EsperaYCambia(segundos));
    }

    private IEnumerator EsperaYCambia(float segundos)
    {
        sceneFader.FadeOut();
        yield return new WaitForSeconds(segundos);
        CambiarEscena();
    }
}