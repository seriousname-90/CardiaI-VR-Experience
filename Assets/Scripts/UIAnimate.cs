using System.Collections;
using UnityEngine;

public class UIAnimate : MonoBehaviour
{
    private Vector3 escalaOriginal;
    public float duracion = 0.2f;

    void Awake()
    {
        escalaOriginal = transform.localScale;
    }

    // Se ejecuta automáticamente cuando haces un .SetActive(true)
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(AnimarAparecer());
    }

    IEnumerator AnimarAparecer()
    {
        float tiempo = 0;
        // 1. De 0 a un poco más grande que la original (Efecto rebote)
        Vector3 escalaGrande = escalaOriginal * 1.1f;

        while (tiempo < duracion)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, escalaGrande, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        // 2. Volver a la escala original
        tiempo = 0;
        while (tiempo < duracion * 0.5f)
        {
            transform.localScale = Vector3.Lerp(escalaGrande, escalaOriginal, tiempo / (duracion * 0.5f));
            tiempo += Time.deltaTime;
            yield return null;
        }
        transform.localScale = escalaOriginal;
    }

    // Método para llamar antes de desactivar el objeto
    public IEnumerator AnimarDesaparecer()
    {
        float tiempo = 0;
        Vector3 escalaGrande = escalaOriginal * 1.1f;
        
        // 1. Crecer un pelín primero
        while (tiempo < duracion * 0.5f)
        {
            transform.localScale = Vector3.Lerp(escalaOriginal, escalaGrande, tiempo / (duracion * 0.5f));
            tiempo += Time.deltaTime;
            yield return null;
        }

        // 2. Encoger hasta desaparecer
        tiempo = 0;
        while (tiempo < duracion)
        {
            transform.localScale = Vector3.Lerp(escalaGrande, Vector3.zero, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}