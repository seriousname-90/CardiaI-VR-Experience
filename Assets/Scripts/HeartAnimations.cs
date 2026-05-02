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
    public Animator bolitaAuricula;
    public Animator bolitaAV;
    public Animator bolitaPurkinjeIzq;
    public Animator bolitaPurkinjeDer;

    [Header("Objetos a desactivar al finalizar la animación")]
    public TrailRenderer[] trails;
    
    [Header("Duración de las animaciones de bolitas (en segundos)")]
    public float duracionBolitaCorta = 1.0f;    // Duración de SA-Auricula y SA-AV
    public float duracionBolitaLarga = 2.0f;    // Duración de Purkinje (67 frames)
    
    private float corteFrame21 = 21f / 67f;
    private float corteFrame38 = 38f / 67f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);
        
        CongelarAnimacionesBolitas();
    }
    
    private void CongelarAnimacionesBolitas()
    {
        if (bolitaAuricula != null) bolitaAuricula.speed = 0f;
        if (bolitaAV != null) bolitaAV.speed = 0f;
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = 0f;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = 0f;
    }
    
    private void OcultarBolita(Animator bolita)
    {
        if (bolita != null) bolita.gameObject.SetActive(false);
    }
    
    private void MostrarBolita(Animator bolita)
    {
        if (bolita != null) bolita.gameObject.SetActive(true);
    }

    public void ReproducirSA()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0f;
        float fin = 0.315f;
        StartCoroutine(ReproducirSegmentoSA(inicio, fin));
        saReproducido = true;
    }

    public void ReproducirAV()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.35f;
        float fin = 0.6f;
        StartCoroutine(ReproducirSegmentoAV(inicio, fin));
        avReproducido = true;
    }

    public void ReproducirHis()
    {
        if (reproduciendoSegmento) return;
        
        float inicio = 0.65f;
        float fin = 1f;
        StartCoroutine(ReproducirSegmentoHis(inicio, fin));
        hisReproducido = true;
    }

    private IEnumerator ReproducirSegmentoSA(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        float duracionNormal = duracionTotal * (fin - inicio);
        float duracionReal = duracionNormal / velocidadSegmentos;
        
        // Reproducir corazón
        animator.speed = velocidadSegmentos;
        animator.Play(nombreAnimacion, 0, inicio);
        
        // Activar bolitas
        MostrarBolita(bolitaAuricula);
        MostrarBolita(bolitaAV);
        MostrarBolita(bolitaPurkinjeIzq);
        MostrarBolita(bolitaPurkinjeDer);
        
        // Calcular velocidad para bolitas cortas: deben completarse en duracionReal
        float velocidadCorta = duracionBolitaCorta / duracionReal;
        
        if (bolitaAuricula != null)
        {
            bolitaAuricula.speed = velocidadCorta;
            bolitaAuricula.Play(0, 0, 0f);
        }
        if (bolitaAV != null)
        {
            bolitaAV.speed = velocidadCorta;
            bolitaAV.Play(0, 0, 0f);
        }
        
        // Calcular velocidad para bolitas largas (deben llegar al frame 21 en duracionReal)
        float tiempoDestino = corteFrame21 * duracionBolitaLarga;
        float velocidadLarga = tiempoDestino / duracionReal;
        
        if (bolitaPurkinjeIzq != null)
        {
            bolitaPurkinjeIzq.speed = velocidadLarga;
            bolitaPurkinjeIzq.Play(0, 0, 0f);
        }
        if (bolitaPurkinjeDer != null)
        {
            bolitaPurkinjeDer.speed = velocidadLarga;
            bolitaPurkinjeDer.Play(0, 0, 0f);
        }
        
        yield return new WaitForSeconds(duracionReal);
        
        // Pausar corazón
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, fin);
        
        // Ocultar bolitas cortas
        OcultarBolita(bolitaAuricula);
        OcultarBolita(bolitaAV);
        
        // Pausar bolitas largas
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = 0f;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = 0f;
        
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private IEnumerator ReproducirSegmentoAV(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        float duracionNormal = duracionTotal * (fin - inicio);
        float duracionReal = duracionNormal / velocidadSegmentos;
        
        // Reproducir corazón
        animator.speed = velocidadSegmentos;
        animator.Play(nombreAnimacion, 0, inicio);
        
        // Calcular velocidad para continuar desde frame 21 hasta frame 38
        float tiempoRecorrido = (corteFrame38 - corteFrame21) * duracionBolitaLarga;
        float velocidadLarga = tiempoRecorrido / duracionReal;
        
        if (bolitaPurkinjeIzq != null)
        {
            bolitaPurkinjeIzq.speed = velocidadLarga;
            bolitaPurkinjeIzq.Play(0, 0, corteFrame21);
        }
        if (bolitaPurkinjeDer != null)
        {
            bolitaPurkinjeDer.speed = velocidadLarga;
            bolitaPurkinjeDer.Play(0, 0, corteFrame21);
        }
        
        yield return new WaitForSeconds(duracionReal);
        
        // Pausar corazón
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, fin);
        
        // Pausar bolitas largas
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = 0f;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = 0f;
        
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private IEnumerator ReproducirSegmentoHis(float inicio, float fin)
    {
        reproduciendoSegmento = true;
        
        float duracionNormal = duracionTotal * (fin - inicio);
        float duracionReal = duracionNormal / velocidadSegmentos;
        
        // Reproducir corazón
        animator.speed = velocidadSegmentos;
        animator.Play(nombreAnimacion, 0, inicio);
        
        // Calcular velocidad para completar desde frame 38 hasta frame 67
        float tiempoRestante = (1f - corteFrame38) * duracionBolitaLarga;
        float velocidadLarga = tiempoRestante / duracionReal;
        
        if (bolitaPurkinjeIzq != null)
        {
            bolitaPurkinjeIzq.speed = velocidadLarga;
            bolitaPurkinjeIzq.Play(0, 0, corteFrame38);
        }
        if (bolitaPurkinjeDer != null)
        {
            bolitaPurkinjeDer.speed = velocidadLarga;
            bolitaPurkinjeDer.Play(0, 0, corteFrame38);
        }
        
        yield return new WaitForSeconds(duracionReal);
        
        // Ocultar bolitas largas al terminar
        OcultarBolita(bolitaPurkinjeIzq);
        OcultarBolita(bolitaPurkinjeDer);
        
        reproduciendoSegmento = false;
        VerificarCompletados();
    }

    private void VerificarCompletados()
    {
        if (saReproducido && avReproducido && hisReproducido)
        {
            EmpezarLoopCompleto();
            DesactivarTrails();
        }
    }

    private void EmpezarLoopCompleto()
    {
        Debug.Log("Los 3 fragmentos completados. Iniciando loop completo");
        
        // Corazón a velocidad normal
        animator.speed = 0.86f;
        animator.Play(nombreAnimacion, 0, 0f);
        
        // Calcular la duración real de un latido completo a velocidad 0.86f
        float duracionLatidoLoop = duracionTotal / 0.86f;  // 0.667 / 0.86 = 0.7756 segundos
        
        // Iniciar el loop de las bolitas sincronizado con el latido
        StartCoroutine(LoopBolitasElectricas(duracionLatidoLoop));
        
        if (audioSource != null) audioSource.Play();
    }

    private IEnumerator LoopBolitasElectricas(float duracionLatido)
    {
        while (true)
        {
            // === FASE SA (0% - 31.5% del latido) ===
            float duracionSA = duracionLatido * 0.315f;  // 0.315 es el fin de SA
            
            // Activar y reproducir bolitas cortas (completas)
            MostrarBolita(bolitaAuricula);
            MostrarBolita(bolitaAV);
            
            float velocidadCorta = duracionBolitaCorta / duracionSA;
            
            if (bolitaAuricula != null)
            {
                bolitaAuricula.speed = velocidadCorta;
                bolitaAuricula.Play(0, 0, 0f);
            }
            if (bolitaAV != null)
            {
                bolitaAV.speed = velocidadCorta;
                bolitaAV.Play(0, 0, 0f);
            }
            
            // Iniciar bolitas largas (deben llegar al frame 21 en duracionSA)
            MostrarBolita(bolitaPurkinjeIzq);
            MostrarBolita(bolitaPurkinjeDer);
            
            float tiempoDestino = corteFrame21 * duracionBolitaLarga;
            float velocidadLarga = tiempoDestino / duracionSA;
            
            if (bolitaPurkinjeIzq != null)
            {
                bolitaPurkinjeIzq.speed = velocidadLarga;
                bolitaPurkinjeIzq.Play(0, 0, 0f);
            }
            if (bolitaPurkinjeDer != null)
            {
                bolitaPurkinjeDer.speed = velocidadLarga;
                bolitaPurkinjeDer.Play(0, 0, 0f);
            }
            
            yield return new WaitForSeconds(duracionSA);
            
            // === FASE AV (31.5% - 60% del latido) ===
            float duracionAV = duracionLatido * (0.6f - 0.315f);  // 0.285 * duracionLatido
            
            float tiempoRecorrido = (corteFrame38 - corteFrame21) * duracionBolitaLarga;
            velocidadLarga = tiempoRecorrido / duracionAV;
            
            if (bolitaPurkinjeIzq != null)
            {
                bolitaPurkinjeIzq.speed = velocidadLarga;
                bolitaPurkinjeIzq.Play(0, 0, corteFrame21);
            }
            if (bolitaPurkinjeDer != null)
            {
                bolitaPurkinjeDer.speed = velocidadLarga;
                bolitaPurkinjeDer.Play(0, 0, corteFrame21);
            }
            
            // Ocultar bolitas cortas (ya terminaron)
            OcultarBolita(bolitaAuricula);
            OcultarBolita(bolitaAV);
            
            yield return new WaitForSeconds(duracionAV);
            
            // === FASE HIS (60% - 100% del latido) ===
            float duracionHis = duracionLatido * (1f - 0.6f);  // 0.4 * duracionLatido
            
            float tiempoRestante = (1f - corteFrame38) * duracionBolitaLarga;
            velocidadLarga = tiempoRestante / duracionHis;
            
            if (bolitaPurkinjeIzq != null)
            {
                bolitaPurkinjeIzq.speed = velocidadLarga;
                bolitaPurkinjeIzq.Play(0, 0, corteFrame38);
            }
            if (bolitaPurkinjeDer != null)
            {
                bolitaPurkinjeDer.speed = velocidadLarga;
                bolitaPurkinjeDer.Play(0, 0, corteFrame38);
            }
            
            yield return new WaitForSeconds(duracionHis);
            
            // Al terminar el latido, las bolitas largas se ocultan y el ciclo se repite
            OcultarBolita(bolitaPurkinjeIzq);
            OcultarBolita(bolitaPurkinjeDer);
        }
    }

    public void Reiniciar()
    {
        saReproducido = false;
        avReproducido = false;
        hisReproducido = false;
        reproduciendoSegmento = false;
        
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);
        
        CongelarAnimacionesBolitas();
        
        OcultarBolita(bolitaAuricula);
        OcultarBolita(bolitaAV);
        OcultarBolita(bolitaPurkinjeIzq);
        OcultarBolita(bolitaPurkinjeDer);
    }

    public void Pausar()
    {
        animator.speed = 0f;
        if (bolitaAuricula != null) bolitaAuricula.speed = 0f;
        if (bolitaAV != null) bolitaAV.speed = 0f;
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = 0f;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = 0f;
    }

    public void Continuar()
    {
        animator.speed = 1f;
    }

    void DesactivarTrails()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.enabled = false;
        }
    }
}