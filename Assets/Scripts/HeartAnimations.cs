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
    private Coroutine fibrilacionCoroutine;
    
    private bool reproduciendoSegmento = false;
    
    [Header("Velocidad de los segmentos")]
    private float velocidadSegmentos = 0.1f;
    public AudioSource audioSource;
    
    [Header("Bolitas eléctricas (Animators)")]
    public Animator bolitaAuricula;      // 12 frames
    public Animator bolitaAV;            // 12 frames
    public Animator bolitaPurkinjeIzq;   // 40 frames
    public Animator bolitaPurkinjeDer;   // 40 frames

    [Header("Animators FA")]
    public Animator FAauricula; // animación temblorosa para fibrilación auricular
    public GameObject FABall1; // animación temblorosa para fibrilación auricular
    public GameObject FABall2; // animación temblorosa para fibrilación auricular
    public Animator cortina1;
    public Animator cortina2;
    public bool cerrarCortinas;
    public AudioClip courtains;


    [Header("Objetos a desactivar al finalizar la animación")]
    public TrailRenderer[] trails;
    
    void Start()
    {
        DetenerBolitas();
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        animator.Play(nombreAnimacion, 0, 0f);
        if (cerrarCortinas){
            CerrarCortinas();
        }
            
    }

    private void CerrarCortinas()
    {
        cortina1.Play("closeCurtain");
        cortina2.Play("closeCurtain2");
        audioSource.PlayOneShot(courtains);
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
        bolitaAuricula.Play("SA-Auricula", 0, 0f);

        bolitaAV.speed = velocidadSegmentos;
        bolitaAV.Play("SA-AV", 0, 0f);

        bolitaPurkinjeIzq.speed = velocidadSegmentos;
        bolitaPurkinjeIzq.Play("SA-Pur1", 0, 0f);

        bolitaPurkinjeDer.speed = velocidadSegmentos;
        bolitaPurkinjeDer.Play("SA-Pur2", 0, 0f);
        
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
        Debug.Log("Iniciando loop completo a velocidad " + velocidad);
        
        // Corazón
        animator.speed = velocidad;
        animator.Play(nombreAnimacion, 0, 0f);
        
        // Forzar reinicio de cada bolita
        ReiniciarAnimator(bolitaAuricula, "SA-Auricula", velocidad);
        ReiniciarAnimator(bolitaAV, "SA-AV", velocidad);
        ReiniciarAnimator(bolitaPurkinjeIzq, "SA-Pur1", velocidad);
        ReiniciarAnimator(bolitaPurkinjeDer, "SA-Pur2", velocidad);
        
        DesactivarTrails();
    }

    private void ReiniciarAnimator(Animator anim, string estado, float velocidad)
    {
        if (anim == null) return;
        
        // Desactivar y reactivar para forzar el reinicio
        anim.gameObject.SetActive(false);
        anim.speed = velocidad;
        anim.Play(estado, 0, 0f);
        anim.gameObject.SetActive(true);
    }

    private void CambiarVelocidad(Animator anim, string estado, float velocidad)
    {
        if (anim == null) return;
        
        animator.speed = velocidad;
        anim.speed = velocidad;
    }

    public void ReproducirTaquicardia()
    {
        DetenerFibrilacionAtrial();
        CambiarVelocidad(bolitaAuricula, "SA-Auricula", 1.5f);
        CambiarVelocidad(bolitaAV, "SA-AV", 1.5f);
        CambiarVelocidad(bolitaPurkinjeIzq, "SA-Pur1", 1.5f);
        CambiarVelocidad(bolitaPurkinjeDer, "SA-Pur2", 1.5f); // Aumentar la velocidad para simular taquicardia
        // función para modificar la velociidad del audio source y el pitch para que suene más rápido
        audioSource.pitch = 1.74f; // Aumentar el pitch para que suene más rápido
        FAauricula.Play("idleAtrial", 0, 0f); // Detener animación temblorosa de fibrilación auricular
        FABall1.SetActive(false); // Desactivar bolita temblorosa 1
        FABall2.SetActive(false); // Desactivar bolita temblorosa 2
    }

    public void ReproducirBradicardia()
    {
        DetenerFibrilacionAtrial();
        CambiarVelocidad(bolitaAuricula, "SA-Auricula", 0.5733f);
        CambiarVelocidad(bolitaAV, "SA-AV", 0.5733f);
        CambiarVelocidad(bolitaPurkinjeIzq, "SA-Pur1", 0.5733f);
        CambiarVelocidad(bolitaPurkinjeDer, "SA-Pur2", 0.5733f); // Reducir la velocidad para simular bradicardia (40 latidos por minuto es 0.5733 veces la velocidad normal)
        // función para modificar la velociidad del audio source y el pitch para que suene más lento
        audioSource.pitch = 0.6666f; // Reducir el pitch para que suene más lento
        FAauricula.Play("idleAtrial", 0, 0f); // Detener animación temblorosa de fibrilación auricular
        FABall1.SetActive(false); // Desactivar bolita temblorosa 1
        FABall2.SetActive(false); // Desactivar bolita temblorosa 2
    }

    public void ReproducirFibrilacionAtrial()
    {
        // Detener fibrilación anterior si existe
        if (fibrilacionCoroutine != null)
            StopCoroutine(fibrilacionCoroutine);
        
        // Iniciar fibrilación
        fibrilacionCoroutine = StartCoroutine(FibrilacionLoop());
        FAauricula.Play("FA", 0, 0f); // Iniciar animación temblorosa de fibrilación auricular
        FABall1.SetActive(true); // Activar bolita temblorosa 1
        FABall2.SetActive(true); // Activar bolita temblorosa 2
        FABall1.GetComponent<Animator>().Play("FAball1", 0, 0f); // Iniciar animación de bolita temblorosa 1
        FABall2.GetComponent<Animator>().Play("FAball2", 0, 0f); // Iniciar animación de bolita temblorosa 2
    }

    public void DetenerFibrilacionAtrial()
    {
        if (fibrilacionCoroutine != null)
        {
            StopCoroutine(fibrilacionCoroutine);
            fibrilacionCoroutine = null;
        }
        
        // Restaurar velocidad normal
        float velocidadNormal = 0.86f;
        animator.speed = velocidadNormal;
        if (bolitaAuricula != null) bolitaAuricula.speed = velocidadNormal;
        if (bolitaAV != null) bolitaAV.speed = velocidadNormal;
        if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = velocidadNormal;
        if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = velocidadNormal;
        if (audioSource != null) audioSource.pitch = 1f;
    }

    private IEnumerator FibrilacionLoop()
    {
        while (true)
        {
            // Generar velocidad aleatoria entre 0.5 y 1.8 (ritmo caótico)
            float velocidadAleatoria = Random.Range(0.5f, 1.8f);
            
            // Calcular pitch correspondiente: pitch = velocidadAnimacion / 0.86
            // porque velocidad 0.86 = pitch 1, velocidad 0.5733 = pitch 0.6666
            float pitchCalculado = velocidadAleatoria / 0.86f;
            
            // Aplicar velocidad a todas las animaciones
            animator.speed = velocidadAleatoria;
            if (bolitaAuricula != null) bolitaAuricula.speed = velocidadAleatoria;
            if (bolitaAV != null) bolitaAV.speed = velocidadAleatoria;
            if (bolitaPurkinjeIzq != null) bolitaPurkinjeIzq.speed = velocidadAleatoria;
            if (bolitaPurkinjeDer != null) bolitaPurkinjeDer.speed = velocidadAleatoria;
            
            // Aplicar pitch al audio
            if (audioSource != null) audioSource.pitch = pitchCalculado;
            
            // Esperar entre 0.2 y 1.5 segundos antes del próximo cambio
            float tiempoEspera = Random.Range(0.2f, 1.5f);
            yield return new WaitForSeconds(tiempoEspera);
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