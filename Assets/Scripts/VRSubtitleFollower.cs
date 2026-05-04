using UnityEngine;

public class VRSubtitleFollower : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform targetCamera;          // La cámara del VR
    public Vector3 forwardOffset = new Vector3(0, -0.2f, 1.5f);  // Offset hacia adelante
    public float followSpeed = 5f;          // Velocidad de movimiento
    
    [Header("Rotación Y (Horizontal)")]
    public float maxYAngleDistance = 30f;    // Ángulo máximo para activar rotación
    public float smoothYTime = 0.3f;         // Suavizado de rotación Y
    private float currentYAngle;
    private float yVelocity;
    private bool isAligned = true;
    
    [Header("Rotación X (Vertical - Clamp)")]
    public float minXAngle;           // Límite inferior (mirando abajo)
    public float maxXAngle;            // Límite superior (mirando arriba)
    
    private RectTransform rectTransform;
    private Canvas canvas;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        
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
        
        // 2. ROTACIÓN Y (Horizontal) - SmoothDampAngle
        float targetYAngle = targetCamera.eulerAngles.y;
        float angleDelta = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetYAngle));
        
        if (angleDelta > maxYAngleDistance)
        {
            isAligned = false;
        }
        
        if (!isAligned)
        {
            currentYAngle = Mathf.SmoothDampAngle(currentYAngle, targetYAngle, ref yVelocity, smoothYTime);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, currentYAngle, transform.eulerAngles.z);
            
            if (Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle)) < 1f)
                isAligned = true;
        }
        
        // 3. ROTACIÓN X (Vertical) - Clamp
        float targetXRotation = targetCamera.eulerAngles.x;
        
        // Normalizar ángulo para clamp
        if (targetXRotation > 180f)
            targetXRotation -= 360f;
        
        float clampedX = Mathf.Clamp(targetXRotation, minXAngle, maxXAngle);
        
        // Aplicar rotación X sin afectar la rotación Y actual
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = -clampedX;  // Negativo para que mire hacia el jugador
        transform.eulerAngles = currentRotation;
    }
}