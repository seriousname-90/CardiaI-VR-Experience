using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Collections;

public class ButtonStateApp : MonoBehaviour
{
    public Button boton;

    public AudioManager audioManager;
    public Animator caiAnimator;
    public ActivarComponente activarCilindro;
    public ActivarComponente botonSiguiente;
    public SceneChanger sceneChanger;

    private int contador = 0;

    void Start()
    {
        boton.onClick.AddListener(OnButtonPressed);
    }

    public void OnButtonPressed()
    {
        Debug.Log("Botón presionado. Contador actual: " + contador);

        UIAnimate anim = boton.GetComponent<UIAnimate>();

        if (anim != null) 
            StartCoroutine(anim.AnimarDesaparecer());
        else 
            boton.gameObject.SetActive(false); 
        
        if (contador == 0)
        {
            if (audioManager != null)
                audioManager.ReproducirLocucion(2); 
            caiAnimator.Play("scalingLarge");
            activarCilindro.ActivarObjetoConDelay(11f);
            contador++;
            Debug.Log("Botón presionado por primera vez. Locución inicial reproducida.");
        }
        else if (contador == 1)
        {
            // Verificar que objectManager existe
            if (audioManager != null)
                audioManager.ReproducirLocucion(7); 
            botonSiguiente.ActivarObjetoConDelay(17f);
            contador++;
            Debug.Log("Botón presionado por segunda vez. Objetos activados");
        }
        else if (contador == 2)
        {
            if (sceneChanger != null)
                sceneChanger.CambiarEscena();
            contador++;
            Debug.Log("Botón presionado por tercera vez. Objetos activados");
        }
         else
        {
            Debug.Log("Botón presionado más de tres veces. No se realizarán más acciones.");
        }
    }


    
}