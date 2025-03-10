using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Kill x velocity
            Debug.Log("Ladder collision");
            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0.2f);
            collision.GetComponent<MunchMovementV2>().isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MunchMovementV2>().isOnLadder = false;
            //MunchMovementV2.instance.isOnLadder = false;
        }
    }
}
