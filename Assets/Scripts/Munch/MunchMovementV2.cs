using UnityEngine;

public class MunchMovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    public bool isOnLadder { private get; set; }

    private Rigidbody2D rb;

    //public static MunchMovementV2 instance;

    private void Awake()
    {
        /*if (instance != null)
        {
            Debug.LogWarning("More than one instance of MunchMovement found!");
            return;
        }
        instance = this;*/

        rb = GetComponent<Rigidbody2D>();
        isOnLadder = false;
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        if (!isOnLadder)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, climbSpeed);
        }

    }
}
