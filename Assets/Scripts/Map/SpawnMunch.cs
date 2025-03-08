using System.Collections;
using UnityEngine;

public class SpawnMunch : MonoBehaviour
{
    [SerializeField] private GameObject munchPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private int numberSpawn;

    private void Start()
    {
        StartCoroutine(SpawnEntities());
    }

    private IEnumerator SpawnEntities()
    {
        for(int i = 0; i < numberSpawn; i++)
        {
            Instantiate(munchPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
