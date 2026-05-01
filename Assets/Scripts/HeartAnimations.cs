using UnityEngine;
using System.Collections;

public class HeartAnimations : MonoBehaviour
{
    private Animator animator;
    private string nombreAnimacion = "Veins_And_Arteries";
    private float duracionTotal = 0.667f;
    
    private bool saReproducido = false;
    private bool avReproducido = false;
    private bool hisReproducido = false;
    
    private bool reproduciendoSegmento = false;
    
    [Header("Velocidad de los segmentos")]
    public float velocidadSegmentos = 0.3f;  // 0.3 = más lento (prueba 0.3, 0.4, 0.5)
    public AudioSource audioSource; 

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);
    }

    public void ReproducirSA()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0f;
        float fin = 0.315f;
        StartCoroutine(ReproducirSegmento(inicio, fin));
        saReproducido = true;
    }

    public void ReproducirAV()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.35f;
        float fin = 0.6f;
        StartCoroutine(ReproducirSegmento(inicio, fin));
        avReproducido = true;
    }

    public void ReproducirHis()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.65f;
        float fin = 1f;
        StartCoroutine(ReproducirHisSegmento(inicio, fin));
        hisReproducido = true;
    }

    private IEnumerator ReproducirSegmento(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        // Calcular duración REAL a velocidad normal
        float duracionNormal = duracionTotal * (fin - inicio);
        // Ajustar por la velocidad lenta
        float duracionReal = duracionNormal / velocidadSegmentos;
        
        animator.speed = velocidadSegmentos;
        animator.Play(nombreAnimacion, 0, inicio);
        
        yield return new WaitForSeconds(duracionReal);
        
        // Pausar al final del segmento
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, fin);
        
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private IEnumerator ReproducirHisSegmento(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        // Calcular duración REAL a velocidad normal
        float duracionNormal = duracionTotal * (fin - inicio);
        // Ajustar por la velocidad lenta
        float duracionReal = duracionNormal / velocidadSegmentos;
        
        animator.speed = velocidadSegmentos;
        animator.Play(nombreAnimacion, 0, inicio);
        
        yield return new WaitForSeconds(0.23f);
        
        // NO pausar His, dejar que siga PERO cambiar a velocidad normal
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private void VerificarCompletados()
    {
        if (saReproducido && avReproducido && hisReproducido)
        {
            EmpezarLoopCompleto();
        }
    }

    private void EmpezarLoopCompleto()
    {
        Debug.Log("Los 3 fragmentos completados. Iniciando loop completo a velocidad normal");
        animator.speed = 0.86f;
        animator.Play(nombreAnimacion, 0, 0f);
        audioSource.Play(); 
    }

    public void Reiniciar()
    {
        saReproducido = false;
        avReproducido = false;
        hisReproducido = false;
        reproduciendoSegmento = false;
        
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);
    }

    public void Pausar()
    {
        animator.speed = 0f;
        Debug.Log("Animación pausada");
    }

    public void Continuar()
    {
        animator.speed = 1f;
        Debug.Log("Animación continuada");
    }
}