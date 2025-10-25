using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public enum GhostState { Chase, Scatter, Frightened, Dead }

[RequireComponent(typeof(Rigidbody2D))]
public class Ghost : MonoBehaviour
{
    public GhostState state = GhostState.Scatter;
    public Transform target;
    public Transform ghostHome;
    public float speed = 3f;
    public float chaseDistance = 5f;
    public Tilemap wallTilemap;

    private Rigidbody2D rb;
    private Vector2 inputDir = Vector2.zero;
    private Vector2 lastDir = Vector2.zero;
    private bool isMoving = false;
    private Vector3 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic; 
        rb.gravityScale = 0f; 
        rb.freezeRotation = true;
        targetPos = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            if (state != GhostState.Frightened && state != GhostState.Dead)
            {
                float distance = Vector2.Distance(transform.position, target.position);
                state = distance <= chaseDistance ? GhostState.Chase : GhostState.Frightened;
            }

            switch (state)
            {
                case GhostState.Chase:
                    SetDirectionTowards(target.position);
                    break;
                case GhostState.Frightened:
                    SetRandomDirection();
                    break;
                case GhostState.Scatter:
                    SetDirectionTowards(GetScatterCorner());
                    break;
                case GhostState.Dead:
                    SetDirectionTowards(ghostHome.position);
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isMoving && inputDir != Vector2.zero)
            TryMove(inputDir);
    }

    void SetDirectionTowards(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        Vector2 preferredDir = Mathf.Abs(dir.x) > Mathf.Abs(dir.y)
            ? (dir.x > 0 ? Vector2.right : Vector2.left)
            : (dir.y > 0 ? Vector2.up : Vector2.down);

        inputDir = ChooseDirectionAvoidBack(preferredDir);
    }

    void SetRandomDirection()
    {
        Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        inputDir = ChooseDirectionAvoidBack(dirs[Random.Range(0, dirs.Length)]);
    }

    Vector2 ChooseDirectionAvoidBack(Vector2 dir)
    {
        if (dir == -lastDir)
        {
            Vector2[] alternatives = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            List<Vector2> valid = new List<Vector2>();
            foreach (var d in alternatives)
            {
                if (d != -lastDir) valid.Add(d);
            }
            if (valid.Count > 0)
                dir = valid[Random.Range(0, valid.Count)];
        }
        return dir;
    }

    void TryMove(Vector2 dir)
    {
        Vector3 nextPos = transform.position + new Vector3(dir.x, dir.y, 0f);
        Vector3Int cellPos = wallTilemap.WorldToCell(nextPos);

        if (wallTilemap.GetTile(cellPos) == null)
        {
            targetPos = wallTilemap.GetCellCenterWorld(cellPos);
            StartCoroutine(MoveTo(targetPos, dir));
        }
        else
        {
            inputDir = Vector2.zero;
        }
    }

    IEnumerator MoveTo(Vector3 pos, Vector2 dir)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            rb.MovePosition(Vector3.Lerp(start, pos, t));
            yield return null;
        }

        rb.MovePosition(pos);
        isMoving = false;
        lastDir = dir;
    }

    Vector3 GetScatterCorner()
    {
        return ghostHome.position + new Vector3(5, 5, 0);
    }

    public void SetFrightened()
    {
        state = GhostState.Frightened;
        speed = 1.5f;
    }

    public void Respawn()
    {
        transform.position = ghostHome.position;
        state = GhostState.Scatter;
        speed = 3f;
    }
}
