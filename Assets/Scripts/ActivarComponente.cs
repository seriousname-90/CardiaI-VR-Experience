using System;
using System.Collections;
using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public GameObject componente; // Arrastra cualquier componente (Building Block)
    public AudioSource audioSource; // Arrastra el AudioSource que deseas reproducir  
 
    public bool activarAlIniciar; // Si quieres que se active al iniciar

    void Start()
    {
        if (activarAlIniciar)
        {
            ActivarDespuesDeSegundos();
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

    public void ActivarDespuesDeSegundos()
    {
        StartCoroutine(ActivarDelay());
    }

    IEnumerator ActivarDelay()
    {
        yield return new WaitForSeconds(2.5f);
        if (audioSource != null)
            audioSource.PlayOneShot(audioSource.clip);

        yield return new WaitForSeconds(audioSource.clip.length);
        Activar();
    }
}