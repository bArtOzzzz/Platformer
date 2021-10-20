using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour, IDamagable
{
	[Header("GUI Settings")]
	public Image healthBar;
	public Image healthBarEffect;

	[Header("Settings")]
	public float maxHealth;
	public float healthSpeed;

	// other
	private TextMeshProUGUI healthText;
	private float _currentHealth;
	private bool isAlive;

    private void Start()
    {
        isAlive = true;
        _currentHealth = maxHealth;
        healthText = healthBar.GetComponentInChildren<TextMeshProUGUI>();
        SetHealth();
    }

    public void TakeDamage(float damage)
    {
        if (!isAlive)
            return;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            isAlive = false;
            SceneManager.LoadScene("Scene1");
        }
        else
            _currentHealth -= damage;
        
        SetHealth();
    }

    private void SetHealth()
    {
        healthBar.fillAmount = _currentHealth / maxHealth;
        healthText.text = "" + (_currentHealth / maxHealth) * 100 + "%";
    }

    private void Update()
    {
        if (healthBarEffect.fillAmount > healthBar.fillAmount)
            healthBarEffect.fillAmount -= healthSpeed;
        else
            healthBarEffect.fillAmount = healthBar.fillAmount;
    }
}
