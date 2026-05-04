using UnityEngine;
using UnityEngine.UI;
using TMPro;  

public class ButtonStateManager : MonoBehaviour
{
    public Button boton;
    public ObjectManager objectManager;
    public AudioManager audioManager;
    public GlassVisuals glassVisuals; 

    private int contador = 0;

    void Start()
    {
        boton.onClick.AddListener(OnButtonPressed);
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager no asignado en ButtonStateManager.");
        }
    }

    public void OnButtonPressed()
    {
        Debug.Log("Botón presionado. Contador actual: " + contador);
        if (contador == 0)
        {
            if (audioManager != null)
                audioManager.ReproducirLocucion(0); // Locución para primera acción
            objectManager.CambiarBoton(0);
            contador++;
            Debug.Log("Botón presionado por primera vez. Locución inicial reproducida.");
        }
        else if (contador == 1)
        {
            // Verificar que objectManager existe
            if (objectManager == null)
            {
                Debug.LogError("objectManager es NULL!");
                return;
            }

            // Apagamos el vidrio justo aquí para que no estorbe en el siguiente paso
            if (glassVisuals != null)
            {
                glassVisuals.DesactivarVidrioTotal();
            }

            objectManager.ActivarObjetos();
            if (audioManager != null)
                audioManager.ReproducirLocucion(1); // Locución para segunda acción
            contador++;
            Debug.Log("Botón presionado por segunda vez. Objetos activados");
        }
        else if (contador == 2)
        {
            if (audioManager != null)
                audioManager.ReproducirLocucion(3); // Locución para tercera acción
            contador++;
            StartCoroutine(objectManager.CambiarEscenaLobby());
            Debug.Log("Botón presionado por tercera vez, cambio de escena programado.");
        }
         else
        {
            Debug.Log("Botón presionado más de dos veces. No se realizarán más acciones.");
        }
    }
}