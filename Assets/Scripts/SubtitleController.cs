using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SubtitleController : MonoBehaviour
{
    [Header("Referencias UI")]
    public Canvas subtitleCanvas;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI speakerText;  // Opcional: texto para el nombre
    
    [Header("Configuración")]
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.2f;
    public float defaultDuration = 3f;
    
    private Coroutine currentSubtitleCoroutine;
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        // Asegurar que existe CanvasGroup
        canvasGroup = subtitleCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = subtitleCanvas.gameObject.AddComponent<CanvasGroup>();
        
        // Ocultar inicialmente
        canvasGroup.alpha = 0;
        subtitleCanvas.enabled = true;
    }
    
    public void PlaySubtitle(SubtitleData subtitle)
    {
        PlaySubtitle(subtitle.message, subtitle.speaker, subtitle.duration);
    }
    
    public void PlaySubtitle(string message, string speaker = "", float duration = -1)
    {
        if (currentSubtitleCoroutine != null)
            StopCoroutine(currentSubtitleCoroutine);
        
        currentSubtitleCoroutine = StartCoroutine(PlaySubtitleCoroutine(message, speaker, duration));
    }
    
    IEnumerator PlaySubtitleCoroutine(string message, string speaker, float duration)
    {
        // Configurar textos
        if (speakerText != null)
            speakerText.text = speaker + (string.IsNullOrEmpty(speaker) ? "" : ":");
        
        subtitleText.text = message;
        
        // Fade in
        float elapsed = 0;
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeInTime);
            yield return null;
        }
        canvasGroup.alpha = 1;
        
        // Esperar duración
        float waitTime = duration > 0 ? duration : defaultDuration;
        yield return new WaitForSeconds(waitTime);
        
        // Fade out
        elapsed = 0;
        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeOutTime);
            yield return null;
        }
        canvasGroup.alpha = 0;
        
        currentSubtitleCoroutine = null;
    }
    
    public void Hide()
    {
        if (currentSubtitleCoroutine != null)
            StopCoroutine(currentSubtitleCoroutine);
        
        canvasGroup.alpha = 0;
    }
}