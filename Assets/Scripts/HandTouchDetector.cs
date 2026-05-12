using UnityEngine;

public class HandTouchDetector : MonoBehaviour
{
    [Header("Configuración de Manos")]
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;
    
    [Header("Configuración de Zona")]
    public Collider detectionZone;
    
    [Header("Eventos")]
    public UnityEngine.Events.UnityEvent OnHandEnter;
    public UnityEngine.Events.UnityEvent OnHandExit;
    
    private bool handInside = false;
    
    // Puntas de los dedos que se detectarán
    private const int INDEX_TIP = (int)OVRPlugin.BoneId.Hand_IndexTip;
    private const int MIDDLE_TIP = (int)OVRPlugin.BoneId.Hand_MiddleTip;
    private const int THUMB_TIP = (int)OVRPlugin.BoneId.Hand_ThumbTip;
    private const int RING_TIP = (int)OVRPlugin.BoneId.Hand_RingTip;
    private const int PINKY_TIP = (int)OVRPlugin.BoneId.Hand_PinkyTip;
    
    void Update()
    {
        bool algunaManoDentro = false;
        
        // Verificar mano izquierda
        if (leftHandSkeleton != null && leftHandSkeleton.IsInitialized && leftHandSkeleton.IsDataValid)
        {
            algunaManoDentro = algunaManoDentro || IsHandInsideZone(leftHandSkeleton);
        }
        
        // Verificar mano derecha
        if (rightHandSkeleton != null && rightHandSkeleton.IsInitialized && rightHandSkeleton.IsDataValid)
        {
            algunaManoDentro = algunaManoDentro || IsHandInsideZone(rightHandSkeleton);
        }
        
        // Lógica de eventos
        if (algunaManoDentro && !handInside)
        {
            handInside = true;
            Debug.Log("Mano detectada dentro");
            OnHandEnter?.Invoke();
        }
        else if (!algunaManoDentro && handInside)
        {
            handInside = false;
            Debug.Log("Mano salió");
            OnHandExit?.Invoke();
        }
    }
    
    bool IsHandInsideZone(OVRSkeleton skeleton)
    {
        if (detectionZone == null) return false;
        if (skeleton.Bones.Count <= PINKY_TIP) return false;
        
        // Verificar puntas de los 5 dedos
        Vector3 indexPos = skeleton.Bones[INDEX_TIP].Transform.position;
        Vector3 middlePos = skeleton.Bones[MIDDLE_TIP].Transform.position;
        Vector3 thumbPos = skeleton.Bones[THUMB_TIP].Transform.position;
        Vector3 ringPos = skeleton.Bones[RING_TIP].Transform.position;
        Vector3 pinkyPos = skeleton.Bones[PINKY_TIP].Transform.position;
        
        bool indexInside = detectionZone.bounds.Contains(indexPos);
        bool middleInside = detectionZone.bounds.Contains(middlePos);
        bool thumbInside = detectionZone.bounds.Contains(thumbPos);
        bool ringInside = detectionZone.bounds.Contains(ringPos);
        bool pinkyInside = detectionZone.bounds.Contains(pinkyPos);
        
        // Al menos un dedo dentro = mano detectada
        return indexInside || middleInside || thumbInside || ringInside || pinkyInside;
    }
    
    void OnDrawGizmos()
    {
        if (detectionZone != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(detectionZone.bounds.center, detectionZone.bounds.size);
        }
    }
}