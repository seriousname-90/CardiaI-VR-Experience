using UnityEngine;

public class CardiOrganicFlight : MonoBehaviour
{
    [Header("Seguimiento de Cámara")]
    public bool seguirCamara = true;
    public float suavizadoRotacion = 2.0f;

    [Header("Movimiento Espacial (Inercia)")]
    public float intensidadVuelo = 0.06f; 
    public float velocidadVuelo = 0.4f;

    [Header("Micro-Vibración (Vida)")]
    public float intensidadVibracion = 0.005f;
    public float velocidadVibracion = 15.0f;

    [Header("Personalidad (Rotación)")]
    public float inclinacionAgente = 4.0f;
    public float agilidadRotacion = 0.6f;

    private Vector3 posInicial;
    private float semilla;
    private Transform camaraPrincipal;

    void Start()
    {
        posInicial = transform.localPosition;
        semilla = Random.Range(0f, 999f);
        
        // Buscamos la cámara (en VR suele ser la MainCamera del rig)
        if (Camera.main != null)
            camaraPrincipal = Camera.main.transform;
    }

    void Update()
    {
        float t = Time.time + semilla;

        // 1. POSICIÓN ORGÁNICA
        float x = (Mathf.PerlinNoise(t * velocidadVuelo, 0) - 0.5f) * intensidadVuelo * 2;
        float y = (Mathf.PerlinNoise(0, t * velocidadVuelo) - 0.5f) * intensidadVuelo * 3;
        float z = (Mathf.PerlinNoise(t * velocidadVuelo, t * velocidadVuelo) - 0.5f) * intensidadVuelo;

        float jitterX = Mathf.Sin(t * velocidadVibracion) * intensidadVibracion;
        float jitterY = Mathf.Cos(t * velocidadVibracion * 0.9f) * intensidadVibracion;

        transform.localPosition = posInicial + new Vector3(x + jitterX, y + jitterY, z);

        // 2. ROTACIÓN Y SEGUIMIENTO TOTAL (Ejes X, Y y Z)
        if (seguirCamara && camaraPrincipal != null)
        {
            // Calculamos la dirección (Invertida para que no te dé la espalda)
            Vector3 direccionHaciaCamara = transform.position - camaraPrincipal.position;

            if (direccionHaciaCamara != Vector3.zero)
            {
                // Ahora NO ponemos direccionHaciaCamara.y = 0. 
                // Dejamos que use la altura para rotar en el eje X del robot.
                Quaternion rotacionHaciaCamara = Quaternion.LookRotation(direccionHaciaCamara);
                
                // Aplicamos el balanceo orgánico (ruido de Perlin)
                float rotX = (Mathf.PerlinNoise(t * agilidadRotacion, semilla) - 0.5f) * inclinacionAgente * 2;
                float rotZ = (Mathf.PerlinNoise(semilla, t * agilidadRotacion) - 0.5f) * inclinacionAgente * 2;
                
                // El balanceo se suma a la rotación de "mirada"
                Quaternion balanceoExtra = Quaternion.Euler(rotX, 0, rotZ);

                // Slerp para que el giro sea elegante y no instantáneo
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionHaciaCamara * balanceoExtra, Time.deltaTime * suavizadoRotacion);
            }
        }
        else
        {
            // Si no hay cámara, mantenemos el balanceo estándar
            float rotX = (Mathf.PerlinNoise(t * agilidadRotacion, semilla) - 0.5f) * inclinacionAgente * 2;
            float rotZ = (Mathf.PerlinNoise(semilla, t * agilidadRotacion) - 0.5f) * inclinacionAgente * 2;
            float rotY = (Mathf.PerlinNoise(t * 0.1f, t * 0.1f) - 0.5f) * (inclinacionAgente * 0.5f);
            
            transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }
}