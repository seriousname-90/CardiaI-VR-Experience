using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TwoHandCounter : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tiempo necesario en segundos")]
    public float requiredTime = 5f;
    
    [Header("Barra de Progreso (Opcional)")]
    public Image progressBar;
    
    [Header("Eventos")]
    public UnityEngine.Events.UnityEvent OnCountdownComplete;
    public UnityEngine.Events.UnityEvent OnCountdownStart;
    public UnityEngine.Events.UnityEvent OnCountdownStop;
    
    // Variables internas
    private int handsInside = 0;
    private float currentTime = 0f;
    private bool isCountingDown = false;
    private Coroutine countdownCoroutine;
    
    // Métodos públicos para asignar en el Inspector
    public void AddHand()
    {
        handsInside++;
        Debug.Log($"Mano agregada. Total: {handsInside}");
        CheckCountdown();
    }
    
    public void RemoveHand()
    {
        handsInside--;
        Debug.Log($"Mano removida. Total: {handsInside}");
        CheckCountdown();
    }
    
    private void CheckCountdown()
    {
        if (handsInside >= 2 && !isCountingDown)
        {
            // Ambas manos dentro → iniciar/reanudar cuenta regresiva
            StartCountdown();
        }
        else if (handsInside < 2 && isCountingDown)
        {
            // Una mano salió → pausar cuenta regresiva
            StopCountdown();
        }
    }
    
    private void StartCountdown()
    {
        isCountingDown = true;
        OnCountdownStart?.Invoke();
        
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);
        
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
        Debug.Log($"Cuenta regresiva iniciada desde {currentTime:F1} segundos");
    }
    
    private void StopCountdown()
    {
        isCountingDown = false;
        OnCountdownStop?.Invoke();
        
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);
        
        Debug.Log($"Cuenta regresiva pausada en {currentTime:F1} segundos");
    }
    
    private IEnumerator CountdownCoroutine()
    {
        while (currentTime < requiredTime)
        {
            // Verificar que sigan ambas manos
            if (handsInside < 2)
            {
                StopCountdown();
                yield break;
            }
            
            // Avanzar el tiempo
            currentTime += Time.deltaTime;
            
            // Actualizar barra de progreso
            if (progressBar != null)
            {
                progressBar.fillAmount = currentTime / requiredTime;
            }
            
            yield return null;
        }
        
        // ¡Completado!
        currentTime = 0f;
        isCountingDown = false;
        
        if (progressBar != null)
            progressBar.fillAmount = 0f;
        
        OnCountdownComplete?.Invoke();
        Debug.Log("¡CUENTA REGRESIVA COMPLETADA!");
    }
    
    // Método para reiniciar manualmente si es necesario
    public void ResetCounter()
    {
        handsInside = 0;
        currentTime = 0f;
        
        if (isCountingDown)
            StopCountdown();
        
        if (progressBar != null)
            progressBar.fillAmount = 0f;
        
        Debug.Log("Contador reiniciado");
    }
}