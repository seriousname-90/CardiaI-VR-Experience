using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketCounter : MonoBehaviour
{
    [Header("Sockets a monitorear")]
    public XRSocketInteractor[] sockets;

    [Header("Objeto a activar cuando ambos estén llenos")]
    public GameObject objetoAActivar;

    private int contador = 0;

    void Start()
    {
        // Suscribirse a eventos de cada socket
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnObjectPlaced);
            socket.selectExited.AddListener(OnObjectRemoved);
        }
    }

    void OnObjectPlaced(SelectEnterEventArgs args)
    {
        contador++;
        VerificarEstado();
    }

    void OnObjectRemoved(SelectExitEventArgs args)
    {
        contador--;
        VerificarEstado();
    }

    void VerificarEstado()
    {
        if (contador >= 2)
        {
            if (objetoAActivar != null)
                objetoAActivar.SetActive(true);
        }
        else
        {
            // Opcional: desactivar si se quita algún objeto
            // if (objetoAActivar != null)
            //     objetoAActivar.SetActive(false);
        }
    }
}