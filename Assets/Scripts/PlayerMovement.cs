using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    public float moveSpeed;

    private float horizontalMovement;
    private float verticalMovement;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerRigidbody.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("eee Im walkin ere");
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;
    }
}
