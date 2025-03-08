using UnityEngine;

public class MunchHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    public static MunchHealth instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of MunchHealth found!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Munch died!");
    }


}
