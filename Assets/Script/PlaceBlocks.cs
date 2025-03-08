using UnityEngine;

public class PlaceBlocks : MonoBehaviour
{
    public GameObject availableBlocksGO;
    public GameObject placedBlocksGO;
    public GameObject blockMenu;

    public float deltaMouseX;
    public float deltaMouseY;

    private Vector3 cursorPosition = Vector3.zero;
    private GameObject selectedBlock = null;


    void Update()
    {
        // Select block
        if (selectedBlock == null && Input.GetMouseButtonDown(0))
        {
            if (select_block())
            {
                return;
            }
        }

        // Show hover block
        if (selectedBlock != null)
        {
            show_hover_block();
        }

        // Place block
        if (selectedBlock != null && Input.GetMouseButtonDown(0))
        {
            place_block();
        }
    }

    bool select_block()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;

        // Convert mouse position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Check if mouse is over a block in the available blocks
        for (int i = 0; i < availableBlocksGO.transform.childCount; i++)
        {
            if (select_child(availableBlocksGO.transform.GetChild(i), worldPos)) {
                return true;
            }
        }
        // Check if mouse is over a block in the placed blocks
        for (int i = 0; i < placedBlocksGO.transform.childCount; i++)
        {
            if (select_child(placedBlocksGO.transform.GetChild(i), worldPos))
            {
                return true;
            }
        }
        return false;
    }

    bool select_child(Transform block, Vector3 worldPos)
    {
        worldPos.z = block.position.z;
        if (block.GetChild(0).transform.GetComponent<Collider2D>().bounds.Contains(worldPos))
        {
            // Instantiate block prefab schema
            selectedBlock = block.gameObject;
            selectedBlock.transform.parent = transform;
            selectedBlock.transform.localScale = new Vector3(0.37f, 0.37f, 1.0f);

            // Change sprite renderer material
            Material wireframeMaterial = selectedBlock.GetComponent<BlockGOHolder>().wireframeMaterial;
            selectedBlock.GetComponentsInChildren<SpriteRenderer>()[0].material = wireframeMaterial;
            return true;
        }
        return false;
    }

    void show_hover_block()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.x += deltaMouseX;
        mousePos.y += deltaMouseY;

        // Convert mouse position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Round world position to nearest cell
        float cell_size_x = 16f / 9f * 10f / (4f * 3f * 3f);
        float cell_size_y = 10f / (3f * 3f * 3f);
        worldPos.x = Mathf.Round(worldPos.x / cell_size_x) * cell_size_x;
        worldPos.y = Mathf.Round(worldPos.y / cell_size_y) * cell_size_y;
        worldPos.z = -10.0f;

        // Set block position to cursor position
        cursorPosition = worldPos;
        if (selectedBlock != null)
            selectedBlock.transform.position = cursorPosition;
    }

    void place_block()
    {
        bool collidesWithMenu = false;
        {
            // Get mouse position
            Vector3 mousePos = Input.mousePosition;

            // Convert mouse position to world position
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Check for collision
            for (int i = 0; i < blockMenu.transform.childCount; i++)
            {
                // CHeck if has collision
                Transform child = blockMenu.transform.GetChild(i);
                if (!child.TryGetComponent<Collider2D>(out Collider2D collider))
                {
                    continue;
                }

                worldPos.z = child.position.z;
                if (child.GetComponent<Collider2D>().bounds.Contains(worldPos))
                {
                    collidesWithMenu = true;
                    break;
                }
            }
        }

        if (collidesWithMenu)
        {
            // Set block as child of block menu
            selectedBlock.transform.parent = availableBlocksGO.transform;
            selectedBlock = null;
        }
        else {
            // Place block
            selectedBlock.layer = 6;

            // Change sprite renderer material
            Material renderMaterial = selectedBlock.GetComponent<BlockGOHolder>().renderMaterial;
            selectedBlock.GetComponentsInChildren<SpriteRenderer>()[0].material = renderMaterial;
            selectedBlock.transform.parent = placedBlocksGO.transform;

            selectedBlock = null;
        }




    }
}
