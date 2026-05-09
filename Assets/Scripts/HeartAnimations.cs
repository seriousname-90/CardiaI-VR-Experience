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
    private float velocidadSegmentos = 0.1f;
    public AudioSource audioSource;
    
    [Header("Bolitas eléctricas (Animators)")]
    public Animator bolitaAuricula;      // 12 frames
    public Animator bolitaAV;            // 12 frames
    public Animator bolitaPurkinjeIzq;   // 40 frames
    public Animator bolitaPurkinjeDer;   // 40 frames

    [Header("Objetos a desactivar al finalizar la animación")]
    public TrailRenderer[] trails;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);

        DetenerBolitas();
    }

    void DetenerBolitas()
    {
        if (bolitaAuricula != null) bolitaAuricula.speed = 0f;
        if (bolitaAV != null) bolitaAV.speed = 0f;
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = 0f;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = 0f;
    }

    public void ReproducirSA()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0f;
        float fin = 0.315f;
        StartCoroutine(ReproducirSegmento(inicio, fin));
        saReproducido = true;
        StartCoroutine(ReproducirBolitasSA(inicio, fin));
    }

    public void ReproducirAV()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.35f;
        float fin = 0.6f;
        StartCoroutine(ReproducirSegmento(inicio, fin));
        avReproducido = true;
        StartCoroutine(ReproducirBolitasAV(inicio, fin));
    }

    public void ReproducirHis()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.65f;
        float fin = 1f;
        StartCoroutine(ReproducirHisSegmento(inicio, fin));
        hisReproducido = true;
        StartCoroutine(ReproducirBolitasHis(inicio, fin));
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

    private IEnumerator ReproducirBolitasSA(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        // Calcular duración REAL a velocidad normal
        float duracionNormal = duracionTotal * 2 * (fin - inicio);
        // Ajustar por la velocidad lenta
        float duracionReal = duracionNormal / velocidadSegmentos / 2;
        
        bolitaAuricula.speed = velocidadSegmentos;
        bolitaAuricula.Play("SA-Auricula", 0, inicio);

        bolitaAV.speed = velocidadSegmentos;
        bolitaAV.Play("SA-AV", 0, inicio);

        bolitaPurkinjeIzq.speed = velocidadSegmentos;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, inicio);

        bolitaPurkinjeDer.speed = velocidadSegmentos;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, inicio);
        
        yield return new WaitForSeconds(duracionReal);

        // Pausar al final del segmento
        bolitaAuricula.speed = 0f;
        bolitaAuricula.Play("SA-Auricula", 0, fin);

        bolitaAV.speed = 0f;
        bolitaAV.Play("SA-AV", 0, fin);

        // Pausar al final del segmento
        bolitaPurkinjeIzq.speed = 0f;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, fin);

        bolitaPurkinjeDer.speed = 0f;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, fin);
        
        reproduciendoSegmento = false;
    }

    private IEnumerator ReproducirBolitasAV(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        // Calcular duración REAL a velocidad normal
        float duracionNormal = duracionTotal * 2 * (fin - inicio);
        // Ajustar por la velocidad lenta
        float duracionReal = duracionNormal / velocidadSegmentos / 2;

        bolitaAuricula.speed = velocidadSegmentos;
        bolitaAuricula.Play("SA-Auricula", 0, inicio);

        bolitaAV.speed = velocidadSegmentos;
        bolitaAV.Play("SA-AV", 0, inicio);

        bolitaPurkinjeIzq.speed = velocidadSegmentos;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, inicio);

        bolitaPurkinjeDer.speed = velocidadSegmentos;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, inicio);
        
        yield return new WaitForSeconds(duracionReal);

        // Pausar al final del segmento
        bolitaAuricula.speed = 0f;
        bolitaAuricula.Play("SA-Auricula", 0, fin);

        bolitaAV.speed = 0f;
        bolitaAV.Play("SA-AV", 0, fin);
        
        // Pausar al final del segmento
        bolitaPurkinjeIzq.speed = 0f;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, fin);

        bolitaPurkinjeDer.speed = 0f;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, fin);
        
        reproduciendoSegmento = false;
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
        
        yield return new WaitForSeconds(duracionReal);
        
        // NO pausar His, dejar que siga PERO cambiar a velocidad normal
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private IEnumerator ReproducirBolitasHis(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        // Calcular duración REAL a velocidad normal
        float duracionNormal = duracionTotal * 2 * (fin - inicio);
        // Ajustar por la velocidad lenta
        float duracionReal = duracionNormal / velocidadSegmentos / 2;
        
        bolitaAuricula.speed = velocidadSegmentos;
        bolitaAuricula.Play("SA-Auricula", 0, inicio);

        bolitaAV.speed = velocidadSegmentos;
        bolitaAV.Play("SA-AV", 0, inicio);

        bolitaPurkinjeIzq.speed = velocidadSegmentos;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, inicio);

        bolitaPurkinjeDer.speed = velocidadSegmentos;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, inicio);
        
        yield return new WaitForSeconds(duracionReal);
        
        // NO pausar His, dejar que siga PERO cambiar a velocidad normal
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private void VerificarCompletados()
    {
        if (saReproducido && avReproducido && hisReproducido)
        {
            Debug.Log("Los 3 segmentos (SA, AV y His) han sido reproducidos. Iniciando loop completo a velocidad normal");
            EmpezarLoopCompleto(0.86f);
            audioSource.Play();
        }
    }

    public void EmpezarLoopCompleto(float velocidad)
    {
        Debug.Log("Los 3 fragmentos completados. Iniciando loop completo a velocidad normal");
        animator.speed = velocidad;
        animator.Play(nombreAnimacion, 0, 0f);

        Mostrar(bolitaAuricula);
        Mostrar(bolitaAV);
        bolitaAuricula.speed = velocidad;
        bolitaAuricula.Play("SA-Auricula", 0, 0f);

        bolitaAV.speed = velocidad;
        bolitaAV.Play("SA-AV", 0, 0f);

        bolitaPurkinjeIzq.speed = velocidad;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, 0f);

        bolitaPurkinjeDer.speed = velocidad;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, 0f);
        DesactivarTrails();
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

    void DesactivarTrails()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.time = 0.05f; // Reducir el tiempo de vida del trail para que desaparezca rápidamente
        }
    } 

    public void Mostrar(Animator obj)

    {

        obj.gameObject.SetActive(true);

    }
}