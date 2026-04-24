using UnityEngine;
using TMPro;

public class AccionBoton : MonoBehaviour
{
    public TextMeshProUGUI textoParaCambiar;
    public Color colorNormal = Color.white;
    public Color colorPresionado = Color.red;

    private bool estaRojo = false;

    public void CambiarColor()
    {
        if (textoParaCambiar != null)
        {
            if (estaRojo)
            {
                textoParaCambiar.color = colorNormal;
                estaRojo = false;
                Debug.Log("Texto vuelve a color normal");
            }
            else
            {
                textoParaCambiar.color = colorPresionado;
                estaRojo = true;
                Debug.Log("Texto cambió a rojo");
            }
        }
    }
}