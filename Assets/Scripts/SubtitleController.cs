using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleController : MonoBehaviour
{
    [Header("Referencias UI")]
    public Canvas subtitleCanvas;
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI speakerText;
    
    [Header("Configuración")]
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 0.2f;
    public float defaultDuration = 3f;
    private float typewriterSpeed = 60f; // Letras por segundo
    
    private Coroutine currentSubtitleCoroutine;
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        canvasGroup = subtitleCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = subtitleCanvas.gameObject.AddComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0;
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
        if (speakerText != null)
            speakerText.text = speaker + (string.IsNullOrEmpty(speaker) ? "" : ":");
        
        // Fade in
        float elapsed = 0;
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeInTime);
            yield return null;
        }
        canvasGroup.alpha = 1;
        
        // Efecto máquina de escribir sin WaitForSeconds
        subtitleText.text = "";
        float timePerLetter = 1f / typewriterSpeed;
        float timer = 0;
        int currentIndex = 0;
        
        while (currentIndex < message.Length)
        {
            timer += Time.deltaTime;
            if (timer >= timePerLetter)
            {
                timer = 0;
                currentIndex++;
                subtitleText.text = message.Substring(0, currentIndex);
            }
            yield return null;
        }
        
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
        
        subtitleText.text = "";
        if (speakerText != null)
            speakerText.text = "";
        
        currentSubtitleCoroutine = null;
    }
    
    public void Hide()
    {
        if (currentSubtitleCoroutine != null)
            StopCoroutine(currentSubtitleCoroutine);
        
        canvasGroup.alpha = 0;
        subtitleText.text = "";
    }
}