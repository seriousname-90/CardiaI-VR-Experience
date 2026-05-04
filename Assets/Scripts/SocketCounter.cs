using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketCounter : MonoBehaviour
{
    [Header("Sockets a monitorear")]
    public XRSocketInteractor[] sockets;

    [Header("Objeto a activar cuando ambos estén llenos")]
    public GameObject objetoAActivar;
    public AudioSource audioSource; 
    public AudioSource ultimaLocución;  

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
            ultimaLocución.Stop(); // Detener la última locución si está sonando
            if (objetoAActivar != null)
                objetoAActivar.SetActive(true);
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.PlayOneShot(audioSource.clip);
            
        }
        else
        {
            // Opcional: desactivar si se quita algún objeto
            // if (objetoAActivar != null)
            //     objetoAActivar.SetActive(false);
        }
    }
}