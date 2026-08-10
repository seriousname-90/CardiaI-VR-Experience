using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Punto de Destino")]
    public Transform destino; // Un GameObject vacío con la posición Y rotación deseada

    [Header("Referencias OVR")]
    public Transform cameraRig;      // El objeto OVRCameraRig / OVR Interaction Rig (el que se mueve)
    public Transform centerEyeAnchor; // OVRCameraRig/TrackingSpace/CenterEyeAnchor

    void Start()
    {
        StartCoroutine(TeleportarAlInicio());
    }

    System.Collections.IEnumerator TeleportarAlInicio()
    {
        // Esperamos un frame para asegurarnos que el tracking del headset ya inicializó
        yield return null;

        Teleport(destino);
    }

    public void Teleport(Transform spawnPoint)
    {
        // 1. Corregimos rotación (solo el eje Y / yaw)
        float rotationOffset = spawnPoint.eulerAngles.y - centerEyeAnchor.eulerAngles.y;
        cameraRig.RotateAround(centerEyeAnchor.position, Vector3.up, rotationOffset);

        // 2. Corregimos posición (delta entre la cabeza actual y el destino)
        Vector3 positionOffset = spawnPoint.position - centerEyeAnchor.position;
        cameraRig.position += positionOffset;
    }
}