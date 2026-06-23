using UnityEngine;

public class CardiOrganicFlight : MonoBehaviour
{
    [Header("Seguimiento de Cámara")]
    public bool seguirCamara = true;
    public float suavizadoRotacion = 2.0f;

    [Header("Balanceo Orgánico")]
    public float inclinacionAgente = 4.0f;
    public float velocidadBalanceo = 0.6f;

    private float semilla;
    private Transform camaraPrincipal;
    private Quaternion rotacionInicial;

    void Start()
    {
        semilla = Random.Range(0f, 999f);
        rotacionInicial = transform.rotation;
        
        if (Camera.main != null)
            camaraPrincipal = Camera.main.transform;
    }

    void Update()
    {
        float t = Time.time + semilla;

        // Balanceo orgánico base (siempre presente)
        float rotX = Mathf.Sin(t * velocidadBalanceo) * inclinacionAgente;
        float rotZ = Mathf.Cos(t * velocidadBalanceo * 0.7f) * inclinacionAgente;
        Quaternion balanceo = Quaternion.Euler(rotX, 0, rotZ);

        if (seguirCamara && camaraPrincipal != null)
        {
            // SOLO USAMOS LA POSICIÓN DE LA CÁMARA, NO LA DEL OBJETO
            // Miramos desde el objeto hacia la cámara
            Vector3 direccion = transform.position - camaraPrincipal.position;
            
            if (direccion != Vector3.zero)
            {
                // Rotación para mirar a la cámara
                Quaternion rotacionBase = Quaternion.LookRotation(direccion);
                
                // Aplicamos el balanceo ENCIMA de la rotación base
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    rotacionBase * balanceo, 
                    Time.deltaTime * suavizadoRotacion
                );
            }
        }
        else
        {
            // Solo balanceo local
            transform.localRotation = balanceo;
        }
    }
}