using UnityEngine;


public class TwoFingerDetector : MonoBehaviour
{
    [Header("Configuración de Dedos")]
    [Tooltip("Asigna el OVRSkeleton de la mano que quieres detectar (Left o Right)")]
    public OVRSkeleton handSkeleton;

    [Header("Configuración de Zona")]
    [Tooltip("El Collider (Trigger) que actuará como detector")]
    public Collider detectionZone;

    [Header("Eventos (Opcional)")]
    public UnityEngine.Events.UnityEvent OnTwoFingersEnter;
    public UnityEngine.Events.UnityEvent OnTwoFingersExit;

    private bool wereBothInside = false;

    // Mapeo de los dedos para OVR (Meta XR)
    // Los índices correctos según OVRSkeleton
    private const int INDEX_TIP = (int)OVRPlugin.BoneId.Hand_IndexTip;
    private const int MIDDLE_TIP = (int)OVRPlugin.BoneId.Hand_MiddleTip;

    void Update()
    {
        // 1. Validación: Si falta algún componente, no hacemos nada.
        if (handSkeleton == null || detectionZone == null || !handSkeleton.IsInitialized || !handSkeleton.IsDataValid)
            return;

        // Verificar que los huesos existan
        if (handSkeleton.Bones.Count <= INDEX_TIP || handSkeleton.Bones.Count <= MIDDLE_TIP)
            return;

        // 2. Obtenemos las posiciones de las puntas de los dedos
        Vector3 indexTip = handSkeleton.Bones[INDEX_TIP].Transform.position;
        Vector3 middleTip = handSkeleton.Bones[MIDDLE_TIP].Transform.position;

        // 3. Comprobamos si cada dedo está DENTRO del Collider
        bool isIndexInside = detectionZone.bounds.Contains(indexTip);
        bool isMiddleInside = detectionZone.bounds.Contains(middleTip);

        // 4. Evaluamos la condición principal: ¿Están los DOS dentro?
        bool areBothInside = isIndexInside && isMiddleInside;

        // 5. Lógica de activación UNA SOLA VEZ
        if (areBothInside && !wereBothInside)
        {
            Debug.Log("¡ACTIVADO! Índice y Corazón están dentro de la caja.");
            OnTwoFingersEnter?.Invoke();
        }
        else if (!areBothInside && wereBothInside)
        {
            Debug.Log("DESACTIVADO: Al menos uno de los dedos salió de la caja.");
            OnTwoFingersExit?.Invoke();
        }

        wereBothInside = areBothInside;
    }

    // --- Dibujar el rango en la escena para depuración ---
    void OnDrawGizmos()
    {
        if (detectionZone != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(detectionZone.bounds.center, detectionZone.bounds.size);
            
            if (Application.isPlaying && handSkeleton != null && handSkeleton.IsInitialized)
            {
                if (handSkeleton.Bones.Count > INDEX_TIP && handSkeleton.Bones.Count > MIDDLE_TIP)
                {
                    Vector3 index = handSkeleton.Bones[INDEX_TIP].Transform.position;
                    Vector3 middle = handSkeleton.Bones[MIDDLE_TIP].Transform.position;
                    
                    Gizmos.color = detectionZone.bounds.Contains(index) ? Color.green : Color.red;
                    Gizmos.DrawSphere(index, 0.01f);
                    
                    Gizmos.color = detectionZone.bounds.Contains(middle) ? Color.green : Color.red;
                    Gizmos.DrawSphere(middle, 0.01f);
                }
            }
        }
    }
}