using UnityEngine;
using Microlight.MicroBar;

public class Stamina : MonoBehaviour
{
    [SerializeField] MicroBar staminaBar;

    float regenAmount = 10f;

    private void Start()
    {
        staminaBar.Initialize(100f);
    }

    public void Deplete(float damageAmount)
    {
        // float damageAmount = Random.Range(5f, 15f);
        staminaBar.UpdateBar(staminaBar.CurrentValue - damageAmount);
    }

    public void Regen(float healAmount)
    {
        staminaBar.UpdateBar(staminaBar.CurrentValue + healAmount);
    }

    private void Update()
    {
        if (staminaBar.CurrentValue < 100)
        {
            Regen(regenAmount * Time.deltaTime);
        }
        
    }
}