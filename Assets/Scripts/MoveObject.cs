using UnityEngine;
using UnityEngine.Events; // Necesario para usar eventos

public class MoveObject : MonoBehaviour
{
    public Vector3 posicionDestino;
    public float velocidad = 2f;
    
    [Header("Eventos de Finalización")]
    public UnityEvent alTerminarMovimiento;
    
    private Vector3 posicionInicial;
    private float tiempoRecorrido = 0f;
    private bool moviendo = false;
    
    public void IniciarMovimiento()
    {
        posicionInicial = transform.position;
        tiempoRecorrido = 0f;
        moviendo = true;
    }
    
    void Update()
    {
        if (moviendo)
        {
            tiempoRecorrido += Time.deltaTime * velocidad;
            transform.position = Vector3.Lerp(posicionInicial, posicionDestino, tiempoRecorrido);
            
            if (tiempoRecorrido >= 1f)
            {
                transform.position = posicionDestino;
                moviendo = false;
                
                if (alTerminarMovimiento != null)
                {
                    alTerminarMovimiento.Invoke();
                }
            }
        }
    }
}