using UnityEngine;

public class AwakeHeart : MonoBehaviour
{
    public HeartAnimations heartAnimations;
    public AudioSource heartAudioSource;

    private void Start()
    {
        // Esperar un frame para asegurar que todo esté inicializado
        StartCoroutine(IniciarConDelay());
    }

    private System.Collections.IEnumerator IniciarConDelay()
    {
        yield return new WaitForSeconds(1.5f);
        
        if (heartAnimations != null)
        {
            heartAnimations.EmpezarLoopCompleto(0.86f);
        }
        else
        {
            Debug.LogError("heartAnimations es NULL en AwakeHeart");
        }
        
        if (heartAudioSource != null)
        {
            heartAudioSource.Play();
        }
        else
        {
            Debug.LogError("heartAudioSource es NULL en AwakeHeart");
        }
    }
}