using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

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
    public float typewriterSpeed = 60f; // Letras por segundo
    
    [Header("Configuración por Speaker")]
    public List<SpeakerConfig> speakerConfigs = new List<SpeakerConfig>();
    
    private Coroutine currentSubtitleCoroutine;
    private CanvasGroup canvasGroup;
    private Dictionary<string, SpeakerConfig> speakerConfigMap;
    private SpeakerConfig currentConfig;
    
    [System.Serializable]
    public class SpeakerConfig
    {
        public string speakerName;
        public Color textColor = Color.white;
        public GameObject componentToActivate;
    }
    
    void Awake()
    {
        canvasGroup = subtitleCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = subtitleCanvas.gameObject.AddComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0;
        
        speakerConfigMap = new Dictionary<string, SpeakerConfig>();
        foreach (var config in speakerConfigs)
        {
            speakerConfigMap[config.speakerName.ToLower()] = config;
        }
    }
    
    public void PlaySubtitle(SubtitleData subtitle)
    {
        PlaySubtitle(subtitle.message, subtitle.speaker, subtitle.duration);
    }
    
    public void PlaySubtitle(string message, string speaker = "", float duration = -1)
    {
        if (currentSubtitleCoroutine != null)
        {
            if (currentConfig != null && currentConfig.componentToActivate != null)
                currentConfig.componentToActivate.SetActive(false);
            
            StopCoroutine(currentSubtitleCoroutine);
        }
        
        currentSubtitleCoroutine = StartCoroutine(PlaySubtitleCoroutine(message, speaker, duration));
    }
    
    IEnumerator PlaySubtitleCoroutine(string message, string speaker, float duration)
    {
        // Obtener configuración del speaker
        currentConfig = GetSpeakerConfig(speaker);
        message = message.Replace("\\n", "\n"); // convertir \n literal a salto de línea real
        
        // Configurar speaker
        if (speakerText != null)
        {
            speakerText.text = speaker + (string.IsNullOrEmpty(speaker) ? "" : ":");
            speakerText.color = currentConfig.textColor;
        }
        
        // Activar componente
        if (currentConfig.componentToActivate != null)
            currentConfig.componentToActivate.SetActive(true);
        
        // Fade in
        float elapsed = 0;
        while (elapsed < fadeInTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeInTime);
            yield return null;
        }
        canvasGroup.alpha = 1;
        
        // Efecto máquina de escribir
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
        
        // Desactivar componente
        if (currentConfig.componentToActivate != null)
            currentConfig.componentToActivate.SetActive(false);
        
        // Limpiar textos
        subtitleText.text = "";
        if (speakerText != null)
            speakerText.text = "";
        
        currentSubtitleCoroutine = null;
        currentConfig = null;
    }
    
    private SpeakerConfig GetSpeakerConfig(string speaker)
    {
        if (string.IsNullOrEmpty(speaker))
            return new SpeakerConfig { textColor = Color.white };
        
        string key = speaker.ToLower();
        if (speakerConfigMap.ContainsKey(key))
            return speakerConfigMap[key];
        
        return new SpeakerConfig { textColor = Color.white };
    }
    
    public void Hide()
    {
        if (currentSubtitleCoroutine != null)
        {
            if (currentConfig != null && currentConfig.componentToActivate != null)
                currentConfig.componentToActivate.SetActive(false);
            
            StopCoroutine(currentSubtitleCoroutine);
        }
        
        canvasGroup.alpha = 0;
        subtitleText.text = "";
        if (speakerText != null)
            speakerText.text = "";
        
        currentSubtitleCoroutine = null;
        currentConfig = null;
    }
}