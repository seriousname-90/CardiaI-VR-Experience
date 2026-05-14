using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Configuración")]
    public string sceneName;
    
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
}