using UnityEngine;
using FMODUnity;

public class MunchHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private AnimationCurve healthColorCurve;
    [SerializeField] private float laserDamage = 20f;
    public float decreaseRate = 4f;

    private float decreaseRateInitial;

    [SerializeField] private EventReference healSound;
    [SerializeField] private EventReference damageSound;


    [SerializeField] private float currentHealth;
    private SpriteRenderer spriteRenderer;
    private MunchMovementV2 munchMovementV2;
    private string color;

    private void Awake()
    {
        munchMovementV2 = GetComponent<MunchMovementV2>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = munchMovementV2.munchType.ToString();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        decreaseRateInitial = decreaseRate;
    }

    private void FixedUpdate()
    { 
       TakeDamage(decreaseRate * Time.fixedDeltaTime);
    }

    public void AttackByLaser(string laserColor)
    {
        if(laserColor == color)
        {
            Heal(laserDamage);
        }
        else
        {
            TakeDamage(laserDamage);
        }
    }

    private void TakeDamage(float damage)
    {
        RuntimeManager.PlayOneShot(damageSound);
        currentHealth -= damage;
        float colorValue = (currentHealth / maxHealth);
        colorValue = healthColorCurve.Evaluate(colorValue);
        spriteRenderer.color = new Color(1, colorValue, colorValue);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Heal(float healAmount)
    {
        RuntimeManager.PlayOneShot(healSound);
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
