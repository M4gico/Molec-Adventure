using UnityEngine;

public class MunchMovement : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float moveSpeed = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask walkLayer;
    [SerializeField] private LayerMask rollLayer;

    [HideInInspector] public bool isWalk { set; private get; }

    private RaycastHit2D hit;
    private Rigidbody2D rb;

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
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
