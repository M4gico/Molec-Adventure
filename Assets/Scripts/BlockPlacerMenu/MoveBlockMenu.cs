using UnityEngine;

public class MoveBlockMenu : MonoBehaviour
{
    public GameObject blockMenuGO;
    public float closedValueX;
    public float openValueX;
    public float speed;

    private bool isOpen = false;
    private Vector3 targetPosition;

    void Start()
    {
        blockMenuGO.transform.position = new Vector3(closedValueX, blockMenuGO.transform.position.y, blockMenuGO.transform.position.z);
        targetPosition = blockMenuGO.transform.position;
    }

    void Update()
    {
        {
            // Move block menu
            blockMenuGO.transform.position = Vector3.Lerp(blockMenuGO.transform.position, targetPosition, speed * Time.deltaTime);
        }

        {
            // Get block menu transform
            Transform blockMenu = blockMenuGO.transform;

            // Get mouse position
            Vector3 mousePos = Input.mousePosition;

            // Convert mouse position to world position
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Check for collision
            bool collides = false;
            for (int i = 0; i < blockMenu.childCount; i++)
            {
                // CHeck if has collision
                Transform child = blockMenu.GetChild(i);
                if (!child.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    continue;
                }

                worldPos.z = child.position.z;
                if (child.GetComponent<Collider2D>().bounds.Contains(worldPos))
                {
                    collides = true;
                    break;
                }
            }

            // Handle menu movement
            if (collides) {
                if (!isOpen) {
                    // Open menu by lerping
                    targetPosition = new Vector3(openValueX, blockMenu.position.y, blockMenu.position.z);
                    isOpen = true;
                }
                else {
                    // Keep menu open
                    targetPosition = new Vector3(openValueX, blockMenu.position.y, blockMenu.position.z);
                }
            }
            else {
                if (isOpen) {
                    // Close menu by lerping
                    targetPosition = new Vector3(closedValueX, blockMenu.position.y, blockMenu.position.z);
                    if (blockMenu.position.x <= closedValueX + 0.1f) {
                        isOpen = false;
                    }
                }
            }
        }
    }
}
