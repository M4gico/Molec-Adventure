using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MunchMovementV2.instance.isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MunchMovementV2.instance.isOnLadder = false;
        }
    }
}
