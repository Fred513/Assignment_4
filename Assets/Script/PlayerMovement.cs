using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Vector2 inputDir;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ≥÷–¯ºÏ≤‚ ‰»Î
        if (Input.GetKey(KeyCode.W)) inputDir = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) inputDir = Vector2.down;
        else if (Input.GetKey(KeyCode.A)) inputDir = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) inputDir = Vector2.right;
        else inputDir = Vector2.zero;

        UpdateAnimator();
    }

    void FixedUpdate()
    {
        if (inputDir != Vector2.zero)
        {
            Vector2 move = inputDir.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }

    void UpdateAnimator()
    {
        if (animator == null) return;
        animator.SetFloat("Horizontal", inputDir.x);
        animator.SetFloat("Vertical", inputDir.y);
        animator.SetBool("isMoving", inputDir != Vector2.zero);
    }
}
