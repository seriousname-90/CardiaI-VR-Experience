using UnityEngine;

public class LockGrabbedObject : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private bool lockeado = false;
    private Rigidbody rb;
    
    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }
    
    public void ActivarLock()
    {
        lockeado = true;
        
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    
    public void DesactivarLock()
    {
        lockeado = false;
        
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
    
    void LateUpdate()
    {
        if (lockeado)
        {
            transform.position = posicionInicial;
            transform.rotation = rotacionInicial;
        }
    }
    
    void FixedUpdate()
    {
        if (lockeado)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = posicionInicial;
                rb.rotation = rotacionInicial;
            }
        }
    }
}