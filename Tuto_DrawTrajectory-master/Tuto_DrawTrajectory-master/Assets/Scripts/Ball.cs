using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    public Puntos puntos; // Referencia a tu script Puntos

    public Transform centerPosition; // La posición inicial o centro

    void Awake()
    {
        // Inicializar componentes
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        // Validar que los componentes existen
        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody2D en la pelota.");
        }

        if (col == null)
        {
            Debug.LogError("No se encontró CircleCollider2D en la pelota.");
        }

        // Validar que la posición central no sea nula
        if (centerPosition == null)
        {
            Debug.LogError("No se ha asignado 'centerPosition' en el inspector.");
        }

        // Validar que el script Puntos no sea nulo
        if (puntos == null)
        {
            Debug.LogError("No se ha asignado el script 'Puntos' en el inspector.");
        }
    }

    void Start()
    {
        // Coloca la pelota en el centro al inicio
        ResetPosition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto con el que colisionó tiene el tag "Vaso"
        if (other.CompareTag("Vaso"))
        {
            // Incrementar el puntaje utilizando el script Puntos
            puntos.IncreaseScore();

            // Reiniciar la posición de la pelota al centro
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // Verificar que la posición central no sea nula
        if (centerPosition == null) return;

        // Desactivar la física momentáneamente
        DesactivateRb();

        // Coloca la pelota en la posición definida como el centro
        transform.position = centerPosition.position;

        // Reactivar la física (si es necesario)
        ActivateRb();
    }

    public void Push(Vector2 force)
    {
        if (rb.isKinematic == false)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("El Rigidbody está en modo Kinematic. No se puede aplicar la fuerza.");
        }
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
}
