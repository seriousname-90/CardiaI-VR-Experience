using UnityEngine;

public class GlassVisuals : MonoBehaviour
{
    public Renderer glassRenderer; // El bloque de vidrio
    public Color colorNormal = Color.black; // Apagado
    public Color colorHover = Color.cyan; // Color suave al acercar la mano
    
    // Esto lo llama el Event Trigger cuando la mano entra
    public void ResaltarVidrio()
    {
        if (glassRenderer != null)
        {
            // Cambiamos el color en el shader
            glassRenderer.material.SetColor("_ColorResaltado", colorHover);
        }
    }

    // Esto lo llama el Event Trigger cuando la mano sale
    public void ApagarVidrio()
    {
        if (glassRenderer != null)
        {
            glassRenderer.material.SetColor("_ColorResaltado", colorNormal);
        }
    }

    public void DesactivarVidrioTotal()
    {
        if (glassRenderer != null)
        {
            glassRenderer.gameObject.SetActive(false); // Apaga el bloque físico
        }
    }
}