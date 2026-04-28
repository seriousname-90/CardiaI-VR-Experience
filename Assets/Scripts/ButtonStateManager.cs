using UnityEngine;
using UnityEngine.UI;

public class ButtonStateManager : MonoBehaviour
{
    public Button boton;
    public ObjectManager objectManager;
    public AudioManager audioManager;

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
            if (audioManager != null)
                audioManager.ReproducirLocucion(1); // Locución para primera acción
            StartCoroutine(objectManager.ActivarObjetos());
            contador++;
            Debug.Log("Botón presionado por primera vez. Objetos activados.");
        }
        else if (contador == 1)
        {
            // Verificar que objectManager existe
            if (objectManager == null)
            {
                Debug.LogError("objectManager es NULL!");
                return;
            }
            StartCoroutine(objectManager.DestruirEInstanciar());
            if (audioManager != null)
                Debug.Log("AUDIOMANAGER NO ES NULL, PORQUE NO FUNCIONA?");
                audioManager.ReproducirLocucion(2); // Locución para segunda acción
            contador++;
            Debug.Log("Botón presionado por segunda vez. Objetos destruidos e instanciados.");
        }
        else if (contador == 2)
        {
            objectManager.ActivarAnimacionMano();
            objectManager.DestruirEInstanciarPortales();
            if (audioManager != null)
                audioManager.ReproducirLocucion(3); // Locución para tercera acción
            contador++;
            Debug.Log("Botón presionado por tercera vez, animación de la mano activada.");
        }
        else if (contador == 3)
        {
            StartCoroutine(objectManager.CambiarEscenaLobby());
            Debug.Log("Botón presionado por cuarta vez, cambiando a la escena Assemble.");
        }
         else
        {
            Debug.Log("Botón presionado más de dos veces. No se realizarán más acciones.");
        }
    }
}