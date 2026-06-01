using UnityEngine;
using UnityEngine.UI; // REQUISITO: Añadimos esto para manejar los botones de interfaz

public class TabsManager : MonoBehaviour
{
    [System.Serializable]
    public struct TabConfig
    {
        public MeshRenderer mallaBoton;    
        public GameObject panelInfo;       
        public Button componenteInteractable; // El componente "Button" de Unity UI
    }

    [Header("Configuración de Pestañas")]
    public TabConfig[] pestanas;

    [Header("Materiales del Botón")]
    public Material materialNormal;        
    public Material materialSeleccionado;  

    [Header("Flujo de Cierre de Escena")]
    public GameObject botonPreguntarFin;

    private float tiempoProximoClic = 0f;
    private float delayEntreClics = 0.4f; 

    public void SeleccionarBoton(MeshRenderer botonPresionado)
    {
        if (Time.time < tiempoProximoClic) return;
        tiempoProximoClic = Time.time + delayEntreClics;

        if (botonPreguntarFin != null && !botonPreguntarFin.activeSelf)
        {
            botonPreguntarFin.SetActive(true);
        }

        for (int i = 0; i < pestanas.Length; i++)
        {
            if (pestanas[i].mallaBoton != null)
            {
                if (pestanas[i].mallaBoton == botonPresionado)
                {
                    // 1. Cambiamos aspecto visual y encendemos info
                    pestanas[i].mallaBoton.material = materialSeleccionado;
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(true);

                    // 2. BLOQUEO: Desactivamos el botón físico para que no reciba más rayos ni clics
                    if (pestanas[i].componenteInteractable != null)
                    {
                        pestanas[i].componenteInteractable.interactable = false;
                    }
                }
                else
                {
                    // Al cambiar de pestaña, restauramos los demás botones para que vuelvan a ser clickeables
                    pestanas[i].mallaBoton.material = materialNormal;
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(false);

                    if (pestanas[i].componenteInteractable != null)
                    {
                        pestanas[i].componenteInteractable.interactable = true;
                    }
                }
            }
        }
    }
}