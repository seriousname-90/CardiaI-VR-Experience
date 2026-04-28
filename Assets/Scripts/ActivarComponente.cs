using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public GameObject componente; // Arrastra cualquier componente (Building Block)

    public void Activar()
    {
        componente.SetActive(true);
    }

    public void Desactivar()
    {
        componente.SetActive(false);
    }
}