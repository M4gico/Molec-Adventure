using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private laserType type;
    private enum laserType
    {
        RedLaser,
        GreenLaser,
        BlueLaser
    }
    [SerializeField] private float changeValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case laserType.RedLaser:
                    
                    break;
                case laserType.GreenLaser:
                    break;
                case laserType.BlueLaser:
                    break;
            }
        }
    }

}
