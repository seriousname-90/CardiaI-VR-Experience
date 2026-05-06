using System;
using System.Collections;
using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public GameObject componente; // Arrastra cualquier componente (Building Block)
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


    public void DesactivarDespuesDeMedioSegundo()
    {
        StartCoroutine(DesactivarDelay());
    }

    IEnumerator DesactivarDelay()
    {
        yield return new WaitForSeconds(0.01f);
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

    public void ActivarObjecoConDelay(float delay)
    {
        StartCoroutine(ActivarConDelay(delay));
    }

    IEnumerator ActivarConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (componente != null)
            componente.SetActive(true);
    }
}