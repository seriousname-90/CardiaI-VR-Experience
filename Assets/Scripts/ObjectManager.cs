using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Objetos a Destruir")]
    [Tooltip("Arrastra aquí los 4 objetos que serán destruidos al inicio")]
    public GameObject[] objetosADestruir = new GameObject[4];

    [Header("Objeto a Instanciar")]
    [Tooltip("Arrastra aquí el prefab 'Distant Grab Object' que se creará")]
    public GameObject distantGrabObjectPrefab;

    [Header("Posiciones para los nuevos objetos")]
    [Tooltip("Define las 3 posiciones donde se instanciarán los objetos")]
    public Vector3[] posicionesPersonalizadas = new Vector3[3];

    void Start()
    {
        // 1. Destruir los 4 objetos existentes
        DestruirObjetos();

        // 2. Crear los 3 nuevos objetos en posiciones personalizadas
        InstanciarNuevosObjetos();
    }

    private void DestruirObjetos()
    {
        // Verificar que el array no esté vacío
        if (objetosADestruir == null || objetosADestruir.Length == 0)
        {
            Debug.LogWarning("El array 'objetosADestruir' está vacío. Asigna los 4 objetos desde el Inspector.");
            return;
        }

        // Recorrer y destruir cada objeto
        for (int i = 0; i < objetosADestruir.Length && i < 4; i++)
        {
            if (objetosADestruir[i] != null)
            {
                Destroy(objetosADestruir[i]);
                Debug.Log($"Objeto '{objetosADestruir[i].name}' destruido (junto con sus hijos)");
            }
            else
            {
                Debug.LogWarning($"El objeto en la posición {i} del array es null. Asígnalo en el Inspector.");
            }
        }
    }

    private void InstanciarNuevosObjetos()
    {
        // Verificar que el prefab existe
        if (distantGrabObjectPrefab == null)
        {
            Debug.LogError("No se asignó el prefab 'Distant Grab Object' en el Inspector.");
            return;
        }

        // Verificar que tenemos posiciones definidas
        if (posicionesPersonalizadas == null || posicionesPersonalizadas.Length == 0)
        {
            Debug.LogWarning("No hay posiciones definidas en 'posicionesPersonalizadas'. Creando objetos en (0,0,0)");
            // Crear al menos un objeto en (0,0,0) como fallback
            Instantiate(distantGrabObjectPrefab, Vector3.zero, Quaternion.identity);
            return;
        }

        // Crear objetos en cada posición personalizada
        for (int i = 0; i < posicionesPersonalizadas.Length && i < 3; i++)
        {
            GameObject nuevoObjeto = Instantiate(
                distantGrabObjectPrefab,
                posicionesPersonalizadas[i],
                Quaternion.identity
            );
            
            // Opcional: Renombrar para identificar fácilmente
            nuevoObjeto.name = $"Distant Grab Object_{i + 1}";
            
            Debug.Log($"Objeto instanciado en posición {i + 1}: {posicionesPersonalizadas[i]}");
        }
    }
}