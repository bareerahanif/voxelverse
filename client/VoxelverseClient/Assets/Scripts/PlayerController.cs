using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = new Vector3(moveX, 0f, moveZ);
        Vector3 newPos = rb.position + move * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPos);
    }
}
