using UnityEngine;

public class FirstTeleportFlag : MonoBehaviour
{
    private static bool yaReproducido = false;
    public AudioSource audioSource;
    public AudioClip audioUnico;
    public AudioManager audioManager;

    // Asigna este método a CADA punto de teletransporte en su evento OnSelected
    public void ReproducirPrimerTeleport()
    {
        if (!yaReproducido)
        {
            yaReproducido = true;
            
            if (audioSource != null && audioUnico != null)
            {
                audioManager.audioSource.Stop();
                audioSource.PlayOneShot(audioUnico);
                Debug.Log("Primer teletransporte - audio reproducido");
            }
        }
    }
}