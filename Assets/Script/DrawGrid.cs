using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    public Material lineMaterial;

    public Color[] colors = new Color[3];

    public GameObject debugLinesGO;

    private void Start()
    {

        gen_for(0);
        gen_for(1);
    }

    void gen_for(int axis)
    {
        // Get go transform
        Transform transform = GetComponent<Transform>();

        int max = axis == 0 ? 4 * 3 * 3 : 3 * 3 * 3;
        for (int it = 0; it < max + 1; it++)
        {
            // Create line renderer object
            GameObject lineRendererObject = new GameObject("LineRenderer");
            lineRendererObject.transform.parent = transform;
            LineRenderer lineRenderer = lineRendererObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;

            bool not_set = true;
            float level = 0;
            if (it % (3 * 3) == 0)
            {
                // Set line renderer properties
                lineRenderer.startWidth = 0.08f;
                lineRenderer.endWidth = 0.08f;
                lineRenderer.startColor = colors[0];
                lineRenderer.endColor = colors[0];
                not_set = false;
                level = -3.0f;
            }

            if (it % 3 == 0 && not_set)
            {
                // Set line renderer properties
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.startColor = colors[1];
                lineRenderer.endColor = colors[1];
                not_set = false;
                level = -2.0f;
            }

            if (not_set)
            {   
                // Set line renderer properties
                lineRenderer.startWidth = 0.02f;
                lineRenderer.endWidth = 0.02f;
                lineRenderer.startColor = colors[2];
                lineRenderer.endColor = colors[2];
                level = -1.0f;
            }

            // Set line renderer points
            float z = -level;
            lineRenderer.positionCount = 2;
            if (axis == 0)
            {
                lineRenderer.SetPosition(0, to_world(new Vector3(it, 0, 0)));
                lineRenderer.SetPosition(1, to_world(new Vector3(it, 3 * 3 * 3, 0)));
            }
            else
            {
                lineRenderer.SetPosition(0, to_world(new Vector3(0, it, 0)));
                lineRenderer.SetPosition(1, to_world(new Vector3(4 * 3 * 3, it, 0)));
            }
            lineRendererObject.transform.position = new Vector3(0, 0, z);
        }

        // Fix lines sprites
        for (int i = 0; i < debugLinesGO.transform.childCount; i++)
        {
            Transform line = debugLinesGO.transform.GetChild(i);
            line.GetComponent<SpriteRenderer>().color = colors[0];
        }
    }

    // Convert 2D position to 3D world position
    Vector3 to_world(Vector3 position)
    {
        Vector3 wsp = new Vector3(position.x / (4.0f * 3.0f * 3.0f * 0.999f) * 10.0f * 16.0f / 9.0f, ((float) (position.y / (3 * 3 * 3) * 10)) * 0.999f, 0);
        wsp = wsp - new Vector3(5.0f * 16.0f/9.0f, 5, 0);
        return wsp;
    }
}
