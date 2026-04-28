using System;
using System.Collections;
using UnityEngine;

public class ActivarComponente : MonoBehaviour
{
    public GameObject componente; // Arrastra cualquier componente (Building Block)

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
        yield return new WaitForSeconds(0.5f);
        if (componente != null)
            componente.SetActive(false);
    }
}