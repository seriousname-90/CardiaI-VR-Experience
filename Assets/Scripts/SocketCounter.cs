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
    public GameObject objetoAActivar;
    public AudioManager audioManager; 
    public SceneChanger sceneChanger;
    public Animator animator; 

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
        Debug.Log($"Objeto colocado en socket. Contador: {contador}");
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
            if (audioManager != null)
                audioManager.ReproducirLocucion(7);
                animator.Play("buildCompleted"); 
                Debug.Log("Reproduciendo locución de éxito por colocar ambos objetos en los sockets."); 
                audioManager.ReproducirLocucionConDelay(8, 14f); // Reproducir locución final cuando termine la anterior
                StartCoroutine(waitForAudioToFinish(22f)); // TODO: CAMBIAR A BOTOON O NOSE 
        }
    }

    private IEnumerator waitForAudioToFinish(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        sceneChanger.CambiarEscena();
    }

}