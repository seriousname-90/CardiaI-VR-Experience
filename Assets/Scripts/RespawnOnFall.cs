using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    [Header("Objetos que pueden caer")]
    public GameObject[] objetos;

    [Header("Posiciones iniciales (opcional)")]
    public Vector3[] posicionesIniciales;
    public AudioSource sonidoRespawn;

    void Start()
    {
        // Guardar posiciones iniciales si no están asignadas
        if (posicionesIniciales == null || posicionesIniciales.Length == 0)
        {
            posicionesIniciales = new Vector3[objetos.Length];
            for (int i = 0; i < objetos.Length; i++)
            {
                if (objetos[i] != null)
                    posicionesIniciales[i] = objetos[i].transform.position;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i] != null && other.gameObject == objetos[i])
            {
                objetos[i].transform.position = posicionesIniciales[i];
                objetos[i].transform.rotation = Quaternion.identity; 
                // Opcional: resetear velocidad si tiene Rigidbody
                Rigidbody rb = objetos[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                if (sonidoRespawn != null)
                {
                    sonidoRespawn.PlayOneShot(sonidoRespawn.clip);
                }

                break;
            }
        }
    }
}