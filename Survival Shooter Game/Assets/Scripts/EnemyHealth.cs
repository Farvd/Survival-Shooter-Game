using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Enemy hit! Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
