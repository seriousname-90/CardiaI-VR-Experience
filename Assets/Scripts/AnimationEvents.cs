using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Animaciones a asignar manualmente")]
    public string nombreAnimacion1;
    public string nombreAnimacion2;
    
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // Método que llamará el evento de animación
    public void CambiarAnimacion()
    {
        if (animator != null && !string.IsNullOrEmpty(nombreAnimacion1))
        {
            animator.Play(nombreAnimacion1);
            Debug.Log("Cambiando a: " + nombreAnimacion1);
        }
    }
    
    public void CambiarAnimacion2()
    {
        if (animator != null && !string.IsNullOrEmpty(nombreAnimacion2))
        {
            animator.Play(nombreAnimacion2);
            Debug.Log("Cambiando a: " + nombreAnimacion2);
        }
    }
    
    // Método genérico que recibe el nombre por parámetro
    public void CambiarA(string nombreAnimacion)
    {
        if (animator != null)
        {
            animator.Play(nombreAnimacion);
            Debug.Log("Cambiando a: " + nombreAnimacion);
        }
    }
}