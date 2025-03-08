using UnityEngine;

public class DDOLManager : MonoBehaviour
{
    [SerializeField] private GameObject[] DDOLObjects;

    private void Awake()
    {
        foreach (var obj in DDOLObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
