using UnityEngine;

public class TabsManager : MonoBehaviour
{
    [System.Serializable]
    public struct TabConfig
    {
        public MeshRenderer mallaBoton;    
        public GameObject panelInfo;       
    }

    [Header("Configuración de Pestañas")]
    public TabConfig[] pestanas;

    [Header("Materiales del Botón")]
    public Material materialNormal;        
    public Material materialSeleccionado;  

    [Header("Flujo de Cierre de Escena")]
    // Arrastra aquí el objeto "ButtonAskEndExperience" padre desde la jerarquía
    public GameObject botonPreguntarFin;

    public void SeleccionarBoton(MeshRenderer botonPresionado)
    {
        // En cuanto toques cualquier pestaña válida, activamos el botón de finalizar
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
                    pestanas[i].mallaBoton.material = materialSeleccionado;
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(true);
                }
                else
                {
                    pestanas[i].mallaBoton.material = materialNormal;
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(false);
                }
            }
        }
    }
}