using UnityEngine;

public class MunchMovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Animator animator;
    public typeMunch munchType { get; private set; }
    public enum typeMunch
    {
        BlueMunch,
        RedMunch,
        GreenMunch
    }

    public bool isOnLadder { private get; set; }
    public bool isGoingLeft { get; private set; }
    public bool isTouchingWall{ private get; set; }

    public Rigidbody2D rb { get; set; }

    //public static MunchMovementV2 instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isOnLadder = false;

        int randomNumber = Random.Range(0, 3);
        switch (randomNumber)
        {
            case 0:
                animator.SetTrigger("BlueMunch");
                break;
            case 1:
                animator.SetTrigger("RedMunch");
                break;
            case 2:
                animator.SetTrigger("GreenMunch");
                break;
        }

        switch (munchType)
        {
            case typeMunch.BlueMunch:
                animator.SetTrigger("BlueMunch");
                break;
            case typeMunch.RedMunch:
                animator.SetTrigger("RedMunch");
                break;
            case typeMunch.GreenMunch:
                animator.SetTrigger("GreenMunch");
                break;
        }
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        if (!isOnLadder)
        {
            if(isTouchingWall)
            {
                isTouchingWall = false;
                ChangeDirection();
            }
            rb.AddForceX(moveSpeed);
            animator.SetBool("isRolling", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, climbSpeed);
            animator.SetBool("isRolling", false);
        }

    }

    private void ChangeDirection()
    {
        isGoingLeft = !isGoingLeft;
        moveSpeed *= -1;
        rb.AddForceX(moveSpeed*10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool isPlayerGoingLeft = collision.gameObject.GetComponent<MunchMovementV2>().isGoingLeft;
            if(isPlayerGoingLeft && !isGoingLeft)
            {
                ChangeDirection();
            }
        }
    }
}
