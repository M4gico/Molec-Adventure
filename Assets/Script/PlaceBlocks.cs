using UnityEngine;

public class PlaceBlocks : MonoBehaviour {
    public GameObject blockPrefabSchema;
    public GameObject blockPrefabRender;

    private Vector3 cursorPosition = Vector3.zero;
    private GameObject placingBlockInstance = null;

    void Start() {
        // Instantiate block prefab schema
        placingBlockInstance = Instantiate(blockPrefabSchema, Vector3.zero, Quaternion.identity);
        placingBlockInstance.transform.localScale = new Vector3(0.37f, 0.37f, 1.0f);
    }

    void Update() {
        {
            // Get mouse position
            Vector3 mousePos = Input.mousePosition;

            // Convert mouse position to world position
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Round world position to nearest cell
            float cell_size = 10f / (3f * 3f * 3f);
            worldPos.x = Mathf.Round(worldPos.x / cell_size) * cell_size;
            worldPos.y = Mathf.Round(worldPos.y / cell_size) * cell_size;
            worldPos.z = -10.0f;

            // Set block position to cursor position
            cursorPosition = worldPos;
            if (placingBlockInstance != null)
                placingBlockInstance.transform.position = cursorPosition;
        }

        {
            // Place block
            if (Input.GetMouseButtonDown(0)) {
                if (placingBlockInstance == null)
                    return;

                // Destroy block prefab schema
                Destroy(placingBlockInstance);

                // Instantiate block prefab render
                Vector3 pos = new Vector3(cursorPosition.x, cursorPosition.y, -10.0f);
                GameObject renderBlock = Instantiate(blockPrefabRender, pos, Quaternion.identity);
                renderBlock.transform.localScale = new Vector3(0.37f, 0.37f, 1.0f);
            }
        }
    }

    void OnDrawGizmos() {
        // // Draw cursor position
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(cursorPosition, Vector3.one);
    }
}
