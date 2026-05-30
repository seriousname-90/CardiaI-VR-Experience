using UnityEngine;

public class SpawnStart : MonoBehaviour
{
    [Header("Posición Destino")]
    public Vector3 posicionDestino; // Sin valor por defecto, la defines desde el Inspector

    void Start()
    {
        transform.position = posicionDestino;
    }
}