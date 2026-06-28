using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [Header("Configuración")]
    public CanvasGroup canvasGroup; // Arrastra el Canvas Group aquí
    public float fadeInDuration = 1.0f; // Duración del Fade In al iniciar
    public float fadeOutDuration = 1.0f; // Duración del Fade Out al salir

    void Start()
    {
        // Al iniciar, hacer Fade In (de opaco a transparente)
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // Empezar completamente opaco
            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.LogWarning("[SceneFader] Canvas Group no asignado.");
        }
    }

    // ==================== FADE IN ====================
    // De opaco (1) a transparente (0)
    IEnumerator FadeIn()
    {
        float tiempo = 0f;
        float alphaInicial = canvasGroup.alpha;

        while (tiempo < fadeInDuration)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / fadeInDuration;
            canvasGroup.alpha = Mathf.Lerp(alphaInicial, 0f, progreso);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        Debug.Log("[SceneFader] Fade In completado.");
    }

    // ==================== FADE OUT ====================
    // De transparente (0) a opaco (1)
    public void FadeOut()
    {
        if (canvasGroup != null)
        {
            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            Debug.LogWarning("[SceneFader] Canvas Group no asignado.");
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float tiempo = 0f;
        float alphaInicial = canvasGroup.alpha;

        while (tiempo < fadeOutDuration)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / fadeOutDuration;
            canvasGroup.alpha = Mathf.Lerp(alphaInicial, 1f, progreso);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        Debug.Log("[SceneFader] Fade Out completado.");
    }
}