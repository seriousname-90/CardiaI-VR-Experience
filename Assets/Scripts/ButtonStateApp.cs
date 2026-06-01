using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ButtonStateApp : MonoBehaviour
{
    public Button boton;
    
    public AudioManager audioManager;
    public Animator caiAnimator;
    public GameObject circleIndicator;
    public ActivarComponente activarCilindro;
    public ActivarComponente botonSiguiente;
    public SceneChanger sceneChanger;
   
    
    [Header("Configuración de Bloqueo")]
    public float cooldownTime = 2f; // Tiempo mínimo entre presiones
    
    private int contador = 0;
    private bool isOnCooldown = false;
    
    void Start()
    {
        boton.onClick.AddListener(OnButtonPressed);
    }
    
    public void OnButtonPressed()
    {
        // Si está en cooldown, ignorar
        if (isOnCooldown)
        {
            Debug.Log("Botón en cooldown, ignorando presión");
            return;
        }
        
        // Iniciar cooldown
        StartCoroutine(Cooldown());
        
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
            circleIndicator.SetActive(true);
            activarCilindro.ActivarObjetoConDelay(11f);
            contador++;
            Debug.Log("Botón presionado por primera vez. Locución inicial reproducida.");
        }
        else if (contador == 1)
        {
            if (sceneChanger != null)
                sceneChanger.CambiarEscena();
            contador++;
            Debug.Log("Botón presionado por segunda vez. Objetos activados");
        }
        else
        {
            Debug.Log("Botón presionado más de dos veces. No se realizarán más acciones.");
        }
    }
    
    IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}