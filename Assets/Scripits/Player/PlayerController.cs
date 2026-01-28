using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    [SerializeField] private float moveSpeed = 10f;
    Rigidbody rb;
    Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        Vector3 currentPostion = rb.position;
        Vector3 movement = new(movementInput.x, 0f, movementInput.y);
        Vector3 newPosition = currentPostion + moveSpeed * Time.fixedDeltaTime * movement;

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        rb.MovePosition(newPosition);
    }


}
