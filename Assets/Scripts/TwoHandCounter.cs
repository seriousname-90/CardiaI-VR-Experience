using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class TwoHandCounter : MonoBehaviour
{
    [Header("Configuración de Tiempos")]
    [Tooltip("Tiempo de carga inicial (segundos)")]
    public float chargeTime;
    
    [Tooltip("Tiempo del video/acción final (segundos)")]
    public float actionTime;
    
    [Header("Referencias UI")]
    public Image chargeProgressBar;    // Barra circular de carga
    public Image actionProgressBar;    // Barra circular opcional para la acción
    
    [Header("Video")]
    public VideoPlayer videoPlayer;     // El VideoPlayer que reproducirá el video
    [Header("Audio")]
    public AudioManager audioManager;   // Para reproducir locuciones (opcional)
    
    [Header("Eventos")]
    public UnityEngine.Events.UnityEvent OnChargeCompleted;   // Cuando terminan los 5 seg
    public UnityEngine.Events.UnityEvent OnActionCompleted;   // Cuando terminan los 10 seg
    public UnityEngine.Events.UnityEvent OnActionPaused;      // Cuando se pausa por salir dedos
    public UnityEngine.Events.UnityEvent OnActionResumed;     // Cuando se reanuda
    
    // Variables internas
    private int handsInside = 0;
    private float currentChargeTime = 0f;
    private float currentActionTime = 0f;
    private bool isCharging = false;
    private bool isActionPhase = false;
    private bool chargeCompleted = false;  // ← La clave: la carga NO se repite
    private Coroutine phaseCoroutine;
    
    // Métodos públicos para los detectores
    public void AddHand()
    {
        handsInside++;
        Debug.Log($"Mano agregada. Total: {handsInside}");
        CheckState();
    }
    
    public void RemoveHand()
    {
        handsInside--;
        Debug.Log($"Mano removida. Total: {handsInside}");
        CheckState();
    }
    
    private void CheckState()
    {
        // Si ya completó la carga, solo manejamos la fase de acción
        if (chargeCompleted)
        {
            HandleActionPhase();
            return;
        }
        
        // Fase de carga (solo si no se completó)
        if (handsInside >= 2 && !isCharging && !chargeCompleted)
        {
            StartCharge();
        }
        else if (handsInside < 2 && isCharging)
        {
            PauseCharge();
        }
    }
    
    private void HandleActionPhase()
    {
        // Manejo de la fase de acción (video de 10 segundos)
        if (handsInside >= 2 && !isActionPhase && chargeCompleted)
        {
            StartAction();
        }
        else if (handsInside < 2 && isActionPhase)
        {
            PauseAction();
        }
    }
    
    private void StartCharge()
    {
        isCharging = true;
        phaseCoroutine = StartCoroutine(ChargeCoroutine());
        audioManager.ReproducirLocucion(5); // Reproducir locución de carga (opcional)
        Debug.Log($"Carga iniciada. Necesitas {chargeTime} segundos");
    }
    
    private void PauseCharge()
    {
        if (phaseCoroutine != null)
            StopCoroutine(phaseCoroutine);
        
        isCharging = false;
        Debug.Log($"Carga pausada en {currentChargeTime:F1}/{chargeTime} segundos");
    }
    
    private IEnumerator ChargeCoroutine()
    {
        while (currentChargeTime < chargeTime)
        {
            // Verificar que sigan ambas manos
            if (handsInside < 2)
            {
                PauseCharge();
                yield break;
            }
            
            currentChargeTime += Time.deltaTime;
            
            if (chargeProgressBar != null)
                chargeProgressBar.fillAmount = currentChargeTime / chargeTime;
            
            yield return null;
        }
        
        // ¡CARGA COMPLETADA!
        isCharging = false;
        chargeCompleted = true;  // ← Nunca más se reinicia la carga
        
        // Ocultar barra de carga (opcional)
        if (chargeProgressBar != null)
            chargeProgressBar.gameObject.SetActive(false);
        
        OnChargeCompleted?.Invoke();
        Debug.Log("¡CARGA COMPLETADA! Iniciando fase de acción...");
        
        // Verificar si podemos iniciar la acción inmediatamente
        HandleActionPhase();
    }
    
    private void StartAction()
    {
        isActionPhase = true;
        phaseCoroutine = StartCoroutine(ActionCoroutine());
        
        // Reproducir video
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            Debug.Log("Video reproducido");
        }
        
        OnActionResumed?.Invoke();
        Debug.Log($"Acción iniciada. Duración: {actionTime} segundos");
    }
    
    private void PauseAction()
    {
        if (phaseCoroutine != null)
            StopCoroutine(phaseCoroutine);
        
        isActionPhase = false;
        
        // Pausar video
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            Debug.Log("Video pausado");
        }
        
        OnActionPaused?.Invoke();
        Debug.Log($"Acción pausada en {currentActionTime:F1}/{actionTime} segundos");
    }
    
    private IEnumerator ActionCoroutine()
    {
        float targetTime = actionTime;
        
        while (currentActionTime < targetTime)
        {
            // Verificar que sigan ambas manos
            if (handsInside < 2)
            {
                PauseAction();
                yield break;
            }
            
            currentActionTime += Time.deltaTime;
            
            // Barra de progreso para la acción (opcional)
            if (actionProgressBar != null)
                actionProgressBar.fillAmount = currentActionTime / targetTime;
            
            // Sincronizar el video (por si acaso)
            if (videoPlayer != null && videoPlayer.isPlaying)
            {
                // Si el video se desincroniza, lo ajustamos (opcional)
                if (Mathf.Abs((float)videoPlayer.time - currentActionTime) > 0.5f)
                {
                    videoPlayer.time = currentActionTime;
                }
            }
            
            yield return null;
        }
        
        // ¡ACCIÓN COMPLETADA!
        isActionPhase = false;
        
        if (actionProgressBar != null)
            actionProgressBar.fillAmount = 1f;
        
        // Asegurar que el video termine
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        
        OnActionCompleted?.Invoke();
        Debug.Log("¡ACCIÓN COMPLETADA! Video terminado.");
    }
    
    // Método para reiniciar TODO (si lo necesitas)
    public void FullReset()
    {
        handsInside = 0;
        currentChargeTime = 0f;
        currentActionTime = 0f;
        isCharging = false;
        isActionPhase = false;
        chargeCompleted = false;
        
        if (phaseCoroutine != null)
            StopCoroutine(phaseCoroutine);
        
        if (chargeProgressBar != null)
        {
            chargeProgressBar.fillAmount = 0f;
            chargeProgressBar.gameObject.SetActive(true);
        }
        
        if (actionProgressBar != null)
            actionProgressBar.fillAmount = 0f;
        
        if (videoPlayer != null)
            videoPlayer.Stop();
        
        Debug.Log("Sistema completamente reiniciado");
    }
}