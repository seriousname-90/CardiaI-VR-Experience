using UnityEngine;
using System;

[Serializable]
public struct SubtitleData
{
    public string speaker;      // Nombre del que habla
    public string message;      // Mensaje o clave de localización
    public float duration;      // Duración en segundos
}