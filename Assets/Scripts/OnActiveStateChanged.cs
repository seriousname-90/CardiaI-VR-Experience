using UnityEngine;
using UnityEngine.Events;

public class OnActiveStateChanged : MonoBehaviour
{
    [Header("Configuración")]
    public UnityEvent onBecameActive;
    public bool executeOnlyOnce = true;  // Por defecto, solo una vez
    
    private bool hasExecuted = false;     // Controla si ya se ejecutó alguna vez
    private bool wasActive = false;       // Controla el cambio de estado
    
    void OnEnable()
    {
        // Verificar si ya se ejecutó (cuando executeOnlyOnce está activado)
        if (executeOnlyOnce && hasExecuted)
            return;
        
        // Se ejecuta SOLO cuando cambia de false a true
        if (!wasActive)
        {
            if (onBecameActive != null)
            {
                onBecameActive.Invoke();
                hasExecuted = true;  // Marcar como ejecutado
            }
        }
        wasActive = true;
    }
    
    void OnDisable()
    {
        wasActive = false;
    }
    
    // Método público para resetear manualmente (opcional)
    public void ResetExecution()
    {
        hasExecuted = false;
        wasActive = false;
    }
    
    // Método para verificar si ya se ejecutó
    public bool HasExecuted()
    {
        return hasExecuted;
    }
}