using UnityEngine;

public class CardiOrganicFlight : MonoBehaviour
{
    [Header("Seguimiento de Cámara")]
    public bool seguirCamara = true;
    public float suavizadoRotacion = 2.0f;

    [Header("Personalidad (Rotación)")]
    public float inclinacionAgente = 4.0f;
    public float agilidadRotacion = 0.6f;

    private float semilla;
    private Transform camaraPrincipal;

    void Start()
    {
        semilla = Random.Range(0f, 999f);
        
        // Buscamos la cámara (en VR suele ser la MainCamera del rig)
        if (Camera.main != null)
            camaraPrincipal = Camera.main.transform;
    }

    void Update()
    {
        float t = Time.time + semilla;

        // SOLO ROTACIÓN: Seguimiento de cámara con balanceo orgánico
        if (seguirCamara && camaraPrincipal != null)
        {
            // Calculamos la dirección desde el objeto hacia la cámara
            Vector3 direccionHaciaCamara = transform.position - camaraPrincipal.position;

            if (direccionHaciaCamara != Vector3.zero)
            {
                // Rotación para mirar hacia la cámara
                Quaternion rotacionHaciaCamara = Quaternion.LookRotation(direccionHaciaCamara);
                
                // Balanceo orgánico (ruido de Perlin) para darle vida
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
            // Si no hay cámara o está desactivado, mantenemos el balanceo estándar
            float rotX = (Mathf.PerlinNoise(t * agilidadRotacion, semilla) - 0.5f) * inclinacionAgente * 2;
            float rotZ = (Mathf.PerlinNoise(semilla, t * agilidadRotacion) - 0.5f) * inclinacionAgente * 2;
            float rotY = (Mathf.PerlinNoise(t * 0.1f, t * 0.1f) - 0.5f) * (inclinacionAgente * 0.5f);
            
            transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }
}