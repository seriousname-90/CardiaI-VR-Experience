using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MoveObject2 : MonoBehaviour
{
    public Vector3 posicionDestino;
    
    [Header("Rotación (opcional)")]
    public bool rotar = false; // Desmarcado por defecto: solo mueve posición
    public Transform centerEyeAnchor; // Necesario solo si rotar = true
    public float rotacionYDestino; // Ángulo Y absoluto deseado, solo si rotar = true

    public float velocidad = 2f;
    
    [Header("Delay antes del movimiento")]
    public float delayAntesMovimiento = 0f;
    public UnityEvent alIniciarDelay;
    
    [Header("Eventos de Finalización")]
    public UnityEvent alTerminarMovimiento;
    
    private Vector3 posicionInicial;
    private float tiempoRecorrido = 0f;
    private bool moviendo = false;
    private Coroutine movimientoCoroutine = null;
    
    public void IniciarMovimiento()
    {
        if (movimientoCoroutine != null)
        {
            StopCoroutine(movimientoCoroutine);
            moviendo = false;
        }
        movimientoCoroutine = StartCoroutine(MoverConDelay());
    }
    
    public void IniciarMovimientoR()
    {
        IniciarMovimiento();
    }
    
    IEnumerator MoverConDelay()
    {
        if (alIniciarDelay != null) alIniciarDelay.Invoke();
        
        if (delayAntesMovimiento > 0f)
            yield return new WaitForSeconds(delayAntesMovimiento);
        
        posicionInicial = transform.position;
        tiempoRecorrido = 0f;
        moviendo = true;

        // Si vamos a rotar, calculamos el delta UNA sola vez al inicio,
        // rotando alrededor de la cabeza real del jugador, no del pivote del rig.
        float rotacionOffset = 0f;
        if (rotar && centerEyeAnchor != null)
        {
            rotacionOffset = rotacionYDestino - centerEyeAnchor.eulerAngles.y;
        }
        float rotacionAcumulada = 0f;
        
        while (moviendo)
        {
            tiempoRecorrido += Time.deltaTime * velocidad;
            float t = Mathf.Clamp01(tiempoRecorrido);
            
            transform.position = Vector3.Lerp(posicionInicial, posicionDestino, t);
            
            if (rotar && centerEyeAnchor != null)
            {
                // Aplicamos solo la porción del delta que corresponde a este frame
                float rotacionObjetivoAcumulada = rotacionOffset * t;
                float deltaFrame = rotacionObjetivoAcumulada - rotacionAcumulada;
                transform.RotateAround(centerEyeAnchor.position, Vector3.up, deltaFrame);
                rotacionAcumulada = rotacionObjetivoAcumulada;
            }
            
            if (tiempoRecorrido >= 1f)
            {
                transform.position = posicionDestino;
                moviendo = false;
                
                if (alTerminarMovimiento != null)
                    alTerminarMovimiento.Invoke();
            }
            
            yield return null;
        }
        
        movimientoCoroutine = null;
    }
    
    public void CancelarMovimiento()
    {
        if (movimientoCoroutine != null)
        {
            StopCoroutine(movimientoCoroutine);
            movimientoCoroutine = null;
            moviendo = false;
        }
    }
}