using UnityEngine;

public class AudioActivator : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private AudioSource audioSource;
    
    private bool hasBeenCalled = false;

    private void Awake()
    {
        // Si no asignaste un AudioSource en el Inspector, se crea uno automáticamente
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    public void PlayAllAudiosAndActivate()
    {
        // Verificar si ya fue llamado antes
        if (hasBeenCalled)
        {
            Debug.LogWarning("Esta función ya fue llamada anteriormente. Solo se puede llamar UNA vez.");
            return;
        }
        
        // Marcar como llamada
        hasBeenCalled = true;
        
        // Activar los GameObjects
        if (objectsToActivate != null)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                    Debug.Log($"GameObject '{obj.name}' activado.");
                }
            }
        }
        else
        {
            Debug.LogWarning("No hay GameObject asignado en 'objectToActivate'.");
        }
        
        // Reproducir todos los audios en orden
        if (audioClips != null && audioClips.Length > 0)
        {
            foreach (AudioClip clip in audioClips)
        {
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
                Debug.Log($"Reproduciendo: {clip.name}");
            }
        }
        }
        else
        {
            Debug.LogWarning("No hay AudioClips asignados en el array.");
        }
    }
}