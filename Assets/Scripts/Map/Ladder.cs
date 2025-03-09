using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private int jumpForce = 200;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
