using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    private Vector2 direction = Vector2.zero;      // 当前移动方向
    private Vector2 nextDirection = Vector2.zero;  // 下一个方向
    private bool isMoving = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            Vector3 target = transform.position + (Vector3)nextDirection;
            if (CanMove(target))
            {
                direction = nextDirection;
                StartCoroutine(MoveTo(transform.position + (Vector3)direction));
            }
            else if (direction != Vector2.zero)
            {
                // 如果下一个方向不能走，尝试继续原方向
                target = transform.position + (Vector3)direction;
                if (CanMove(target))
                {
                    StartCoroutine(MoveTo(target));
                }
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) nextDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) nextDirection = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) nextDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) nextDirection = Vector2.right;
    }

    bool CanMove(Vector3 target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, nextDirection, 1f, LayerMask.GetMask("Wall"));
        return hit.collider == null;
    }

    IEnumerator MoveTo(Vector3 target)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }

    void UpdateAnimator()
    {
        if (animator == null) return;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}
