using UnityEngine;

public class VRSubtitleFollower : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform targetCamera;
    public Vector3 forwardOffset = new Vector3(0, -0.2f, 1.5f);
    public float followSpeed = 15f;
    
    [Header("Rotación")]
    public float smoothYTime = 0.1f;
    public float smoothXTime = 0.1f;
    
    private float currentYAngle;
    private float currentXAngle;
    private float yVelocity;
    private float xVelocity;
    
    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main.transform;
    }
    
    void LateUpdate()
    {
        if (targetCamera == null) return;
        
        // 1. MOVIMIENTO: Seguir a la cámara con offset
        Vector3 targetPosition = targetCamera.position + 
                                 targetCamera.forward * forwardOffset.z +
                                 targetCamera.up * forwardOffset.y +
                                 targetCamera.right * forwardOffset.x;
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        
        // 2. ROTACIÓN Y (Horizontal)
        float targetYAngle = targetCamera.eulerAngles.y;
        currentYAngle = Mathf.SmoothDampAngle(currentYAngle, targetYAngle, ref yVelocity, smoothYTime);
        
        // 3. ROTACIÓN X (Vertical) - Inversa a la cámara
        float targetXRotation = targetCamera.eulerAngles.x;
        
        // Normalizar ángulo (0 a 360)
        if (targetXRotation > 180f)
            targetXRotation -= 360f;
        
        // Invertir: si cámara mira arriba (-90), panel mira abajo (90) para seguir de frente
        float targetXAngle = +targetXRotation;
        
        // Aplicar suavizado
        currentXAngle = Mathf.SmoothDamp(currentXAngle, targetXAngle, ref xVelocity, smoothXTime);
        
        // Aplicar rotación final
        transform.rotation = Quaternion.Euler(currentXAngle, currentYAngle, 0);
    }
}