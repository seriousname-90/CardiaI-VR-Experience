using UnityEngine;

public class XROriginSpawn : MonoBehaviour
{
    [Header("Configuración")]
    public bool usarSpawnPoint = true;
    public Vector3 posicionPorDefecto = new Vector3(0, 0, 0);
    public Vector3 rotacionPorDefecto = new Vector3(0, 0, 0);
    
    private Rigidbody rb;
    
    void Start()
    {
        // Obtener el Rigidbody si existe
        rb = GetComponent<Rigidbody>();
        
        if (usarSpawnPoint)
        {
            // Buscar objeto con tag "SpawnPoint"
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            
            if (spawnPoint != null)
            {
                // Mover el XR Origin al spawn point
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
                
                Debug.Log($"XR Origin movido a spawn point: {spawnPoint.name}");
            }
            else
            {
                Debug.LogWarning("No se encontró objeto con tag 'SpawnPoint'. Usando posición por defecto.");
                transform.position = posicionPorDefecto;
                transform.rotation = Quaternion.Euler(rotacionPorDefecto);
            }
        }
        else
        {
            // Usar posición por defecto
            transform.position = posicionPorDefecto;
            transform.rotation = Quaternion.Euler(rotacionPorDefecto);
        }
        
        // Limpiar velocidades del Rigidbody para evitar movimientos extraños
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}