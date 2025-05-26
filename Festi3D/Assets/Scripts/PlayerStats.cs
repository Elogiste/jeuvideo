using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Other elements references")]
    [SerializeField] private Animator animator;
    [SerializeField] private MoveBehaviour playerMovementScript;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private float healthDecreaseRateForHungerAndThirst;

    [Header("Hunger")]
    [SerializeField] private float maxHunger = 100f;
    public float currentHunger;
    [SerializeField] private Image hungerBarFill;
    [SerializeField] private float hungerDecreaseRate;

    [Header("Thirst")]
    [SerializeField] private float maxThirst = 100f;
    public float currentThirst;
    [SerializeField] private Image thirstBarFill;
    [SerializeField] private float thirstDecreaseRate;

    public float currentArmorPoints;
    [HideInInspector] public bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }

    void Update()
    {
        UpdateHungerAndThirstBarsFill();

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50f);
        }
    }

    public void TakeDamage(float damage, bool overTime = false)
    {
        if (overTime)
        {
            currentHealth -= damage * Time.deltaTime;
        }
        else
        {
            currentHealth -= damage * (1 - (currentArmorPoints / 100));
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }

        UpdateHealthBarFill();
    }

    private void Die()
    {
        Debug.Log("Player died !");
        isDead = true;

        playerMovementScript.canMove = false;
        hungerDecreaseRate = 0;
        thirstDecreaseRate = 0;

        animator.SetTrigger("Die");

        // ✅ Informe le GameManager
        Gestion.instance.PlayerDied();
    }

    public void ConsumeItem(float health, float hunger, float thirst)
    {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
        currentHunger = Mathf.Min(currentHunger + hunger, maxHunger);
        currentThirst = Mathf.Min(currentThirst + thirst, maxThirst);

        UpdateHealthBarFill();
    }

    public void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    void UpdateHungerAndThirstBarsFill()
    {
        currentHunger -= hungerDecreaseRate * Time.deltaTime;
        currentThirst -= thirstDecreaseRate * Time.deltaTime;

        currentHunger = Mathf.Max(currentHunger, 0);
        currentThirst = Mathf.Max(currentThirst, 0);

        hungerBarFill.fillAmount = currentHunger / maxHunger;
        thirstBarFill.fillAmount = currentThirst / maxThirst;

        if (currentHunger <= 0 || currentThirst <= 0)
        {
            float dmg = (currentHunger <= 0 && currentThirst <= 0)
                ? healthDecreaseRateForHungerAndThirst * 2
                : healthDecreaseRateForHungerAndThirst;

            TakeDamage(dmg, true);
        }
    }
}
