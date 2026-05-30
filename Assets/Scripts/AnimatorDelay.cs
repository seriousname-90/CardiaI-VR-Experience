using UnityEngine;

public class AnimatorDelay : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Delay en segundos antes de reproducir la animación")]
    public float delayAntesDeAnimacion = 0f;
    
    private Animator animator;
    
    void Start()
    {
        // Obtener el componente Animator del mismo objeto
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("No se encontró un componente Animator en este objeto.");
        }
    }
    
    /// <summary>
    /// Cambia el valor del delay en tiempo de ejecución
    /// </summary>
    /// <param name="nuevoDelay">Nuevo tiempo de espera en segundos</param>
    public void CambiarDelay(float nuevoDelay)
    {
        delayAntesDeAnimacion = nuevoDelay;
        Debug.Log($"Delay cambiado a: {delayAntesDeAnimacion} segundos");
    }
    
    /// <summary>
    /// Reproduce una animación con el delay configurado
    /// </summary>
    /// <param name="nombreAnimacion">Nombre del estado de animación a reproducir</param>
    public void ReproducirConDelay(string nombreAnimacion)
    {
        if (animator == null)
        {
            Debug.LogError("Animator no disponible.");
            return;
        }
        
        StartCoroutine(ReproducirDespuesDelay(nombreAnimacion));
    }
    
    private System.Collections.IEnumerator ReproducirDespuesDelay(string nombreAnimacion)
    {
        // Esperar el tiempo configurado
        if (delayAntesDeAnimacion > 0f)
        {
            Debug.Log($"Esperando {delayAntesDeAnimacion} segundos antes de reproducir: {nombreAnimacion}");
            yield return new WaitForSeconds(delayAntesDeAnimacion);
        }
        
        // Reproducir la animación
        animator.Play(nombreAnimacion);
        Debug.Log($"Reproduciendo animación: {nombreAnimacion}");
    }
}