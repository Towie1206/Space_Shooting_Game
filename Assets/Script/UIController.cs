using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TMP_Text experienceText;

    public GameObject pausePanel;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateEnergySlider(float currentEnergy, float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;
        energySlider.value = Mathf.RoundToInt(currentEnergy);
        energyText.text = energySlider.value + "/" + energySlider.maxValue;
    }

    public void UpdateHealthSlider(float currentHealth, float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = Mathf.RoundToInt(currentHealth);
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }

    public void UpdateExperienceSlider(float currentEXP, float maxEXP)
    {
        experienceSlider.maxValue = maxEXP;
        experienceSlider.value = Mathf.RoundToInt(currentEXP);
        experienceText.text = experienceSlider.value + "/" + experienceSlider.maxValue;
    }
}
