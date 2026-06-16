using UnityEngine;
using UnityEngine.UI; // REQUISITO: Añadimos esto para manejar los botones de interfaz
using System.Collections.Generic;

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

    [Header("Alternancia de Componentes Externos")]
    // Arrastra aquí el contenedor padre que agrupa visualmente todos tus botones de pestañas en la escena
    public GameObject contenedorPestañas;
    // Arrastra aquí el componente externo X que quieres activar en su lugar
    public GameObject componenteExterno;

    private float tiempoProximoClic = 0f;
    private float delayEntreClics = 0.4f; 

    // Conjunto para guardar las mallas de los botones que el usuario ya presionó
    private HashSet<MeshRenderer> botonesPresionadosAnteriores = new HashSet<MeshRenderer>();

    public void SeleccionarBoton(MeshRenderer botonPresionado)
    {
        if (Time.time < tiempoProximoClic) return;
        tiempoProximoClic = Time.time + delayEntreClics;

        // Registramos el botón actual en nuestro contador de visitados
        if (!botonesPresionadosAnteriores.Contains(botonPresionado))
        {
            botonesPresionadosAnteriores.Add(botonPresionado);
        }

        // El botón de terminar experiencia aparece solo si ya se presionaron los 3 tabs únicos
        if (botonesPresionadosAnteriores.Count >= 3)
        {
            if (botonPreguntarFin != null && !botonPreguntarFin.activeSelf)
            {
                botonPreguntarFin.SetActive(true);
            }
        }

        for (int i = 0; i < pestanas.Length; i++)
        {
            if (pestanas[i].mallaBoton != null)
            {
                if (pestanas[i].mallaBoton == botonPresionado)
                {
                    // Cambiamos aspecto visual al seleccionado y encendemos su info
                    pestanas[i].mallaBoton.material = materialSeleccionado;
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(true);

                    // Ya NO ponemos el interactable en false. Sigue activo para clics.
                    if (pestanas[i].componenteInteractable != null)
                    {
                        pestanas[i].componenteInteractable.interactable = true;
                    }
                }
                else
                {
                    if (pestanas[i].panelInfo != null) pestanas[i].panelInfo.SetActive(false);

                    // Si este botón NO es el actual, pero YA fue presionado en el pasado,
                    // mantiene el aspecto visual de seleccionado (usando tu material "GlassShader Selected")
                    if (botonesPresionadosAnteriores.Contains(pestanas[i].mallaBoton))
                    {
                        pestanas[i].mallaBoton.material = materialSeleccionado;
                    }
                    else
                    {
                        // Si nunca ha sido presionado, vuelve a su estado normal de fábrica
                        pestanas[i].mallaBoton.material = materialNormal;
                    }

                    if (pestanas[i].componenteInteractable != null)
                    {
                        pestanas[i].componenteInteractable.interactable = true;
                    }
                }
            }
        }
    }

    // FUNCIÓN 1: Oculta las pestañas de la interfaz y muestra el componente externo
    public void OcultarTabsYMostrarComponente()
    {
        // Apagamos el contenedor global de las pestañas
        if (contenedorPestañas != null)
        {
            contenedorPestañas.SetActive(false);
        }

        // Encendemos el componente externo X que tiene los nuevos botones
        if (componenteExterno != null)
        {
            componenteExterno.SetActive(true);
        }
    }

    // FUNCIÓN 2: Se llama desde el botón interno del componente X para regresar a las pestañas
    public void MostrarTabsYOcultarComponente()
    {
        // Apagamos el componente externo X
        if (componenteExterno != null)
        {
            componenteExterno.SetActive(false);
        }

        // Volvemos a encender el contenedor original con las pestañas
        if (contenedorPestañas != null)
        {
            contenedorPestañas.SetActive(true);
        }
    }
}