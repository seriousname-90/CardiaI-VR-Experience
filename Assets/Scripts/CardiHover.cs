using UnityEngine;

public class CardiOrganicFlight : MonoBehaviour
{
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
    private Quaternion rotInicial;
    private float semilla;

    void Start()
    {
        posInicial = transform.localPosition;
        rotInicial = transform.localRotation;
        // Semilla única para que su patrón sea irrepetible
        semilla = Random.Range(0f, 999f);
    }

    void Update()
    {
        float t = Time.time + semilla;

        // 1. POSICIÓN ORGÁNICA (Uso de Perlin para evitar la "linealidad")
        // Calculamos un desfase en los 3 ejes para que flote en una "nube" de puntos
        float x = (Mathf.PerlinNoise(t * velocidadVuelo, 0) - 0.5f) * intensidadVuelo * 2;
        float y = (Mathf.PerlinNoise(0, t * velocidadVuelo) - 0.5f) * intensidadVuelo * 3; // Más rango en Y
        float z = (Mathf.PerlinNoise(t * velocidadVuelo, t * velocidadVuelo) - 0.5f) * intensidadVuelo;

        // 2. MICRO-JITTER (Simula motores/hélices internas)
        float jitterX = Mathf.Sin(t * velocidadVibracion) * intensidadVibracion;
        float jitterY = Mathf.Cos(t * velocidadVibracion * 0.9f) * intensidadVibracion;

        transform.localPosition = posInicial + new Vector3(x + jitterX, y + jitterY, z);

        // 3. ROTACIÓN "HUMANA" (Inclinación por inercia)
        // Cardi se inclina un poco hacia donde "cree" que se mueve la deriva
        float rotX = (Mathf.PerlinNoise(t * agilidadRotacion, semilla) - 0.5f) * inclinacionAgente * 2;
        float rotZ = (Mathf.PerlinNoise(semilla, t * agilidadRotacion) - 0.5f) * inclinacionAgente * 2;
        float rotY = (Mathf.PerlinNoise(t * 0.1f, t * 0.1f) - 0.5f) * (inclinacionAgente * 0.5f);

        transform.localRotation = rotInicial * Quaternion.Euler(rotX, rotY, rotZ);
    }
}