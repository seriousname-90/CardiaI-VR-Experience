using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public Behaviour componente; // Arrastra cualquier componente (Building Block)

    public void Activar()
    {
        componente.enabled = true;
    }

    public void Desactivar()
    {
        componente.enabled = false;
    }
}