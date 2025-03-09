using UnityEngine;
using FMODUnity;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private laserType type;
    private enum laserType
    {
        RedLaser,
        GreenLaser,
        BlueLaser
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RuntimeManager.PlayOneShot("Events/Level Music");
            switch (type)
            {
                case laserType.RedLaser:
                    collision.GetComponent<MunchHealth>().AttackByLaser("RedMunch");
                    break;
                case laserType.GreenLaser:
                    collision.GetComponent<MunchHealth>().AttackByLaser("GreenMunch");
                    break;
                case laserType.BlueLaser:
                    collision.GetComponent<MunchHealth>().AttackByLaser("BlueMunch");
                    break;
            }
        }
    }

}
