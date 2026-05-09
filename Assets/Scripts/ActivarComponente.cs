using System;
using System.Collections;
using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public GameObject componente; // Arrastra cualquier componente
    public GameObject[] componentes; // Arrastra aquí el objeto "Button" de la jerarquía 
    public AudioManager audiomanager; // Arrastra el AudioManager 
 
    public bool activarAlIniciar; // Si quieres que se active al iniciar

    void Start()
    {
        if (activarAlIniciar)
        {
            ActivarAudioConDelay();
        }
    }

    public void Activar()
    {
        componente.SetActive(true);
    }

    public void Desactivar()
    {
        componente.SetActive(false);
    }


    public void DesactivarConDelay(float delay)
    {
        StartCoroutine(DesactivarDelay(delay));
    }

    IEnumerator DesactivarDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (componente != null)
            componente.SetActive(false);
    }

    public void ActivarAudioConDelay()
    {
        StartCoroutine(ActivarDelay());
    }

    IEnumerator ActivarDelay()
    {
        yield return new WaitForSeconds(2.5f);
        if (audiomanager != null)
            audiomanager.ReproducirLocucion(0);
            Debug.Log("Reproduciendo locución inicial con delay");
    }

    public void ActivarObjetoConDelay(float delay)
    {
        StartCoroutine(ActivarConDelay(delay));
    }

    IEnumerator ActivarConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (componente != null)
            componente.SetActive(true);
    }

    public void ActivarObjetosConDelay(float delay)
    {
        StartCoroutine(ActivarObsConDelay(delay));
    }

    IEnumerator ActivarObsConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject obj in componentes)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}