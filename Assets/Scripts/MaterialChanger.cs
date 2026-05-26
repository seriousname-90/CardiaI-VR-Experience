using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [Header("Objeto a modificar")]
    public Renderer objetoZ;
    
    [Header("Materiales")]
    public Material materialCuandoTrue;
    public Material materialCuandoFalse;
    
    void OnEnable()
    {
        if (objetoZ != null && materialCuandoTrue != null)
            objetoZ.material = materialCuandoTrue;
    }
    
    void OnDisable()
    {
        if (objetoZ != null && materialCuandoFalse != null)
            objetoZ.material = materialCuandoFalse;
    }
}