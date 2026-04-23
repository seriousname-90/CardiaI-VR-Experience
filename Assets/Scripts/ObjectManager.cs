using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    [Header("Objetos a destruir (segunda presión)")]
    public GameObject[] objetosADestruir = new GameObject[4];

    [Header("Objetos a activar (primera presión)")]
    public GameObject[] objetosAActivar;

    [Header("Objetos a instanciar (segunda presión)")]
    public GameObject prefabDistantGrab;
    public Vector3[] posicionesDistantGrab = new Vector3[3];

    [Header("Objetos a instanciar (tercera presión)")]
    public GameObject animacionMano;

    public void ActivarObjetos()
    {
        foreach (GameObject obj in objetosAActivar)
        {
            if (obj != null) obj.SetActive(true);
            Debug.Log("Activando: " + obj.name);
        }
    }

    public void DestruirEInstanciar()
    {
        // Destruir
        foreach (GameObject obj in objetosADestruir)
        {
            if (obj != null) Destroy(obj);
            Debug.Log("Destruyendo: " + obj.name);
        }

        // Instanciar nuevos
        foreach (Vector3 pos in posicionesDistantGrab)
        {
            Instantiate(prefabDistantGrab, pos, Quaternion.identity);
            Debug.Log("Instanciando: " + prefabDistantGrab.name);
        
        }
    }

    public void ActivarAnimacionMano()
    {
        if (animacionMano != null) animacionMano.SetActive(true);
            Debug.Log("Activando: " + animacionMano.name);
        // Aquí puedes agregar el código para activar la animación de la mano
        Debug.Log("Activando animación de la mano.");
    }

    public void CambiarEscenaLobby()
    {
        Debug.Log("Cambiando a la escena Assemble.");
        SceneManager.LoadScene("Assemble");
    }
}