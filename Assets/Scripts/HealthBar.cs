using UnityEngine;
using Microlight.MicroBar;

public class HealthBar : MonoBehaviour
{
    [SerializeField] MicroBar healthBar;

    private void Start()
    {
        healthBar.Initialize(100f);
    }

    public void Damage(float damageAmount)
    {
        healthBar.UpdateBar(healthBar.CurrentValue - damageAmount);
    }

    public void Heal(float healAmount)
    {
        healthBar.UpdateBar(healthBar.CurrentValue + healAmount);
    }
}
