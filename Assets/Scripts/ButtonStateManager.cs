using UnityEngine;
using UnityEngine.UI;

public class ButtonStateManager : MonoBehaviour
{
    public Button boton;
    public ObjectManager objectManager;
    public AudioManager audioManager;

    [Header("Gestión de Pantallas (NUEVO)")]
    public GameObject[] panelesInstrucciones;

    private int contador = 0;

    void Start()
    {
        boton.onClick.AddListener(OnButtonPressed);
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager no asignado en ButtonStateManager.");
        }
    }

    void OnButtonPressed()
    {
        Debug.Log("Botón presionado. Contador actual: " + contador);

        if (contador == 0)
        {
            ActualizarInterfaz(0, 1); // Pasa del panel 1 al 2

            if (audioManager != null)
                audioManager.ReproducirLocucion(1);
            StartCoroutine(objectManager.ActivarObjetos());
            contador++;
            Debug.Log("Botón presionado por primera vez. Objetos activados.");
        }
        else if (contador == 1)
        {
            ActualizarInterfaz(1, 2); // Pasa del panel 2 al 3

            objectManager.DestruirEInstanciar();
            if (audioManager != null)
                audioManager.ReproducirLocucion(2);
            contador++;
            Debug.Log("Botón presionado por segunda vez. Objetos destruidos e instanciados.");
        }
        else if (contador == 2)
        {
            ActualizarInterfaz(2, 3); // Pasa del panel 3 al 4

            objectManager.ActivarAnimacionMano();
            if (audioManager != null)
                audioManager.ReproducirLocucion(3);
            contador++;
            Debug.Log("Botón presionado por tercera vez, animación de la mano activada.");
        }
        else if (contador == 3)
        {
            StartCoroutine(objectManager.CambiarEscenaLobby());
            Debug.Log("Botón presionado por cuarta vez, cambiando a la escena Assemble.");
        }
    }

    // Método para cambiar los paneles de instrucciones
    void ActualizarInterfaz(int indiceActual, int indiceSiguiente)
    {
        if (panelesInstrucciones != null)
        {
            if (indiceActual < panelesInstrucciones.Length && panelesInstrucciones[indiceActual] != null)
                panelesInstrucciones[indiceActual].SetActive(false);

            if (indiceSiguiente < panelesInstrucciones.Length && panelesInstrucciones[indiceSiguiente] != null)
                panelesInstrucciones[indiceSiguiente].SetActive(true);
        }
    }
}