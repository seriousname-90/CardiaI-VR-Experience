using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [Header("Objeto a modificar")]
    public Renderer objetoZ;
    
    [Header("Materiales")]
    public Material materialCuandoTrue;
    public Material materialCuandoFalse;
    
    private Material materialOriginal;
    
    void Start()
    {
        if (objetoZ != null)
            materialOriginal = objetoZ.material;
    }
    
    void Update()
    {
        if (objetoZ != null)
        {
            if (gameObject.activeSelf)
                objetoZ.material = materialCuandoTrue;
            else
                objetoZ.material = materialCuandoFalse;
        }
    }
}