using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Vector3 posicionDestino;
    public Vector3 rotacionDestino;
    public float velocidad = 2f;
    
    [Header("Delay antes del movimiento")]
    public float delayAntesMovimiento = 0f; // Tiempo en segundos antes de empezar
    public UnityEvent alIniciarDelay; // Evento que se dispara cuando comienza el delay
    
    [Header("Eventos de Finalización")]
    public UnityEvent alTerminarMovimiento;
    
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private float tiempoRecorrido = 0f;
    private bool moviendo = false;
    private Coroutine movimientoCoroutine = null;
    
    public void IniciarMovimiento()
    {
        // Si ya hay un movimiento en curso, lo detenemos
        if (movimientoCoroutine != null)
        {
            StopCoroutine(movimientoCoroutine);
            moviendo = false;
        }
        
        // Iniciamos la coroutine que maneja el delay + movimiento
        movimientoCoroutine = StartCoroutine(MoverConDelay());
    }
    
    public void IniciarMovimientoR()
    {
        IniciarMovimiento(); // Mismo comportamiento que IniciarMovimiento
    }
    
    IEnumerator MoverConDelay()
    {
        // Disparamos evento al inicio del delay
        if (alIniciarDelay != null)
        {
            alIniciarDelay.Invoke();
        }
        
        // Esperamos el tiempo configurado
        if (delayAntesMovimiento > 0f)
        {
            yield return new WaitForSeconds(delayAntesMovimiento);
        }
        
        // Guardamos posiciones iniciales
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        tiempoRecorrido = 0f;
        moviendo = true;
        
        // Movimiento principal
        while (moviendo)
        {
            tiempoRecorrido += Time.deltaTime * velocidad;
            
            // Movimiento de posición
            transform.position = Vector3.Lerp(posicionInicial, posicionDestino, tiempoRecorrido);
            
            // Movimiento de rotación
            Quaternion rotacionDestinoQuaternion = Quaternion.Euler(rotacionDestino);
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionDestinoQuaternion, tiempoRecorrido);
            
            if (tiempoRecorrido >= 1f)
            {
                transform.position = posicionDestino;
                transform.rotation = Quaternion.Euler(rotacionDestino);
                moviendo = false;
                
                if (alTerminarMovimiento != null)
                {
                    alTerminarMovimiento.Invoke();
                }
            }
            
            yield return null; // Esperamos un frame
        }
        
        movimientoCoroutine = null;
    }
    
    // Método opcional para cancelar el movimiento durante el delay
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