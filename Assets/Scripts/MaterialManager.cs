using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [Header("Configuración")]
    public Material materialSecundario;
    
    private Renderer objectRenderer;
    private Material materialOriginal;
    private bool secundarioActivo = false;
    
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        
        // Guardar el material original (índice 1)
        if (objectRenderer.materials.Length > 1)
            materialOriginal = objectRenderer.materials[1];
    }
    
    public void ActivarMaterialSecundario()
    {
        if (!secundarioActivo && materialSecundario != null)
        {
            Material[] mats = objectRenderer.materials;
            
            if (mats.Length > 1)
            {
                mats[1] = materialSecundario;
                objectRenderer.materials = mats;
                secundarioActivo = true;
            }
        }
    }
    
    public void DesactivarMaterialSecundario()
    {
        if (secundarioActivo && materialOriginal != null)
        {
            Material[] mats = objectRenderer.materials;
            
            if (mats.Length > 1)
            {
                mats[1] = materialOriginal;
                objectRenderer.materials = mats;
                secundarioActivo = false;
            }
        }
    }
}