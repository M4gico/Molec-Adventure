using UnityEngine;

public class MunchMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float velocityThreshold = 0.1f;
    [SerializeField] private float jumpForce = 0.1f;
    [SerializeField] private float jumpThreshold = 0.1f;

    private bool isWalkLayer;
    private bool canWalk;
    private bool isWaiting;

    private Rigidbody2D rb;
    private RaycastHit2D hit;

    public static MunchMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MunchMovement found!");
            return;
        }
        instance = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    [ContextMenu("Walk")]
    private void Walk()
    {
        if (isWalkLayer)
        {
            if (canWalk)
            {
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            }
            else if (!isWaiting && Mathf.Abs(rb.linearVelocity.x) < velocityThreshold && !canWalk)
            {
                rb.AddForceY(jumpForce);
                isWaiting = true;
            }
            else if (isWaiting && !canWalk)
            {
                Debug.DrawRay(transform.position, Vector2.down * jumpThreshold, Color.red);
                hit = Physics2D.Raycast(transform.position, Vector2.down, jumpThreshold);
                Debug.Log("distance: " + hit.distance + "collider:" + hit.collider);
                if ((hit.distance < jumpThreshold) && (hit.collider != null))
                {
                    canWalk = true;
                    isWaiting = false;
                }
            }
        }
    }

    [ContextMenu("CanMoveFalse")]
    private void CanMoveFalse()
    {
        canWalk = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer 7 is the ramp layer
        if (collision.gameObject.layer == 7)
        {
            isWalkLayer = false;
            canWalk = false;
        }
        //Layer 6 is the ground layer
        else if (collision.gameObject.layer == 6)
        {
            isWalkLayer = true;
        }
    }
}
