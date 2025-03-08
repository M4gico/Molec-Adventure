using UnityEngine;

public class MunchGroundDetection : MonoBehaviour
{
    [SerializeField] private LayerMask walkLayer;
    [SerializeField] private LayerMask rollLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == walkLayer)
        {
            MunchMovement.instance.isWalk = true;
        }
        else if (collision.gameObject.layer == rollLayer)
        {
            MunchMovement.instance.isWalk = false;
        }
    }
}
