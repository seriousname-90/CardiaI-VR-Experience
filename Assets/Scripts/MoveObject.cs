using UnityEngine;
using UnityEngine.Events;

public class MoveObject : MonoBehaviour
{
    public Vector3 posicionDestino;
    public Vector3 rotacionDestino;
    public float velocidad = 2f;
    
    [Header("Eventos de Finalización")]
    public UnityEvent alTerminarMovimiento;
    
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private float tiempoRecorrido = 0f;
    private bool moviendo = false;
    
    public void IniciarMovimiento()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        tiempoRecorrido = 0f;
        moviendo = true;
    }
    
    public void IniciarMovimientoR()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        tiempoRecorrido = 0f;
        moviendo = true;
    }
    
    void Update()
    {
        if (moviendo)
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
        }
    }
}