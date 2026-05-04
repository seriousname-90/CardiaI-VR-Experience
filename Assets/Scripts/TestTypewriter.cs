using UnityEngine;
using TMPro;
using System.Collections;

public class TestTypewriter : MonoBehaviour
{
    public TextMeshProUGUI testText;
    
    void Start()
    {
        StartCoroutine(Test());
    }
    
    IEnumerator Test()
    {
        string texto = "Este es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOO Este es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOEste es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOEste es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOEste es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOEste es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOEste es un texto de prueba aaaaaaaaaaaaaaaaaaaaaaaaaa loloolool llllllllllolooooooolllllllll WASOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOO";
        testText.text = "";
        
        for (int i = 0; i <= texto.Length; i++)
        {
            testText.text = texto.Substring(0, i);
            yield return new WaitForSecondsRealtime(0.000000001f); // 0.01 segundos = muy rápido
        }
    }
}