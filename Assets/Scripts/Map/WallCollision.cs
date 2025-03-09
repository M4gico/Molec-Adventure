using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MunchMovementV2>().isTouchingWall = true;
        }
    }
}
