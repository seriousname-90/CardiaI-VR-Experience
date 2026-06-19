using System.Collections;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketCounter : MonoBehaviour
{
    [Header("Sockets a monitorear")]
    public XRSocketInteractor[] sockets;
    [Header("Objetos a desactivar gravedad")]
    public GameObject[] grabElectrodes;

    [Header("Objeto a activar cuando ambos estén llenos")]
    public ActivarComponente objetoAActivar;
    public AudioManager audioManager; 
    public Animator animator; 

    private int contador = 0;

    void Start()
    {
        // Suscribirse a eventos de cada socket
        foreach (var socket in sockets)
        {
            socket.selectEntered.AddListener(OnObjectPlaced);
        }
    }

    void OnObjectPlaced(SelectEnterEventArgs args)
    {
        contador++;
        VerificarEstado();
        Debug.Log($"Objeto colocado en socket. Contador: {contador}");
    }

    void VerificarEstado()
    {
        if (contador >= 2)
        {
            if (objetoAActivar != null)
                objetoAActivar.ActivarObjetoConDelay(23.5f);
            if (audioManager != null)
                audioManager.ReproducirLocucion(7);
                animator.Play("buildCompleted"); 
                Debug.Log("Reproduciendo locución de éxito por colocar ambos objetos en los sockets."); 
                audioManager.ReproducirLocucionConDelay(8, 13.5f); // Reproducir locución final cuando termine la anterior

        }
    }

}