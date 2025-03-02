using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad del personaje
    private Vector3 movement;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Usamos Rigidbody en 3D
    }

    void Update()
    {
        // Captura el input de movimiento (X = izquierda/derecha, Z = adelante/atr�s)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        // Normaliza el vector para evitar velocidad extra en diagonal
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Aplicamos movimiento en el plano XZ, sin afectar Y
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }

}
