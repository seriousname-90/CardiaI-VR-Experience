using UnityEngine;

public class AudioActivator : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private GameObject objectToActivate;
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
        
        // Activar el GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log($"GameObject '{objectToActivate.name}' activado.");
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