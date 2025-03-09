using UnityEngine;

public class MunchMovementV2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private typeMunch munchType;
    private enum typeMunch
    {
        BlueMunch,
        RedMunch,
        GreenMunch
    }

    public bool isOnLadder { private get; set; }
    public void StopMovement() {
        Vector2 new_vel = transform.GetComponent<Rigidbody2D>().linearVelocity * 0.1f;
        finalPosition = transform.position + new Vector3(new_vel.x, new_vel.y, 0);
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
        transform.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        shouldMove = false;
        transform.GetComponent<MunchHealth>().decreaseRate = 0;

        // TODO: Add animation for munch
    }

    private Rigidbody2D rb;
    private bool shouldMove = true;
    private Vector3 finalPosition;

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

        /*switch (munchType)
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
        }*/
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            Walk();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, moveSpeed * Time.deltaTime);
        }
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
