using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    [Header("Objetos a activar (primera presión)")]
    public GameObject[] objetosAActivar;
    public AudioClip lobby4;        // Tercer audio 
    public AudioClip lobby9;       // Cuarto audio 
    public float tiempo_lobby4 = 3f;

    [Header("Objetos a instanciar (segunda presión)")]
    public GameObject[] objetosAInstanciar;
    
    [Header("Objetos a destruir (segunda presión)")]
    public GameObject[] objetosADestruir = new GameObject[4];
    [Header("Objetos a instanciar (tercera presión)")]
    public GameObject animacionMano;
    public GameObject[] objetosAInstanciar2;
    public GameObject[] objetosADestruir2;

    public IEnumerator ActivarObjetos()
    {
        yield return new WaitForSeconds(tiempo_lobby4);

        // Reproducir audio inicial (37 seg)
        if (lobby4 != null)
        {
            audioSource.PlayOneShot(lobby4);
            Debug.Log("Reproduciendo audio lobby4 (3 seg después de la primera acción)");
        }

        foreach (GameObject obj in objetosAActivar)
        {
            if (obj != null) obj.SetActive(true);
            Debug.Log("Activando: " + obj.name);
        }
    }

    public IEnumerator DestruirEInstanciar()
    {
        Debug.Log("=== INICIANDO DESTRUIR E INSTANCIAR ===");
        // Destruir
        // Verificar si hay objetos para destruir
        if (objetosADestruir == null || objetosADestruir.Length == 0)
        {
            Debug.LogError("objetosADestruir está VACÍO o es NULL. Asigna objetos en el Inspector.");
        }
        else
        {
            Debug.Log($"Hay {objetosADestruir.Length} objetos para destruir");
            
            // Destruir
            foreach (GameObject obj in objetosADestruir)
            {
                if (obj != null)
                {
                    Destroy(obj);
                    Debug.Log("Destruyendo: " + obj.name);
                }
                else
                {
                    Debug.LogWarning("Un objeto en objetosADestruir es NULL");
                }
            }
        }

        yield return new WaitForSeconds(2);
        
        // Verificar si hay objetos para instanciar
        if (objetosAInstanciar == null || objetosAInstanciar.Length == 0)
        {
            Debug.LogError("objetosAInstanciar está VACÍO o es NULL. Asigna objetos en el Inspector.");
        }
        else
        {
            Debug.Log($"Hay {objetosAInstanciar.Length} objetos para instanciar");
            
            // Instanciar nuevos
            foreach (GameObject obj in objetosAInstanciar)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    Debug.Log("Activando: " + obj.name);
                }
                else
                {
                    Debug.LogWarning("Un objeto en objetosAInstanciar es NULL");
                }
            }
        }
    
        Debug.Log("=== FIN DESTRUIR E INSTANCIAR ===");
    }

    public void ActivarAnimacionMano()
    {
        if (animacionMano != null) animacionMano.SetActive(true);
            Debug.Log("Activando: " + animacionMano.name);
        // Aquí puedes agregar el código para activar la animación de la mano
        Debug.Log("Activando animación de la mano.");
    }

    public void DestruirEInstanciarPortales()
    {
        // Destruir
        foreach (GameObject obj in objetosADestruir2)
        {
            if (obj != null) Destroy(obj);
            Debug.Log("Destruyendo: " + obj.name);
        }

        // Instanciar nuevos
        foreach (GameObject obj in objetosAInstanciar2)
        {
            if (obj != null) obj.SetActive(true);
            Debug.Log("Activando: " + obj.name);
        }
    }

    public IEnumerator CambiarEscenaLobby()
    {
         if (lobby9 != null)
        {
            audioSource.PlayOneShot(lobby9);
            Debug.Log("Reproduciendo audio lobby9");
        }
        yield return new WaitForSeconds(tiempo_lobby4 + 1);
        Debug.Log("Cambiando a la escena Assemble.");
        SceneManager.LoadScene("Assemble");
    }
}