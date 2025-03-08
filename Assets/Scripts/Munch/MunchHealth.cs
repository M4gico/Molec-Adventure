using UnityEngine;

public class MunchHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private AnimationCurve healthColorCurve;
    public float decreaseRate;

    private float currentHealth;
    private SpriteRenderer spriteRenderer;

    //public static MunchHealth instance;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        TakeDamage(decreaseRate * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        float colorValue = (currentHealth / maxHealth);
        colorValue = healthColorCurve.Evaluate(colorValue);
        //Debug.Log("Color value: " + colorValue);
        spriteRenderer.color = new Color(1, colorValue, colorValue);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
