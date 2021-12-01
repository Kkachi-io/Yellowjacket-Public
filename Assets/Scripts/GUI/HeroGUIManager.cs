using UnityEngine;

public class HeroGUIManager : MonoBehaviour
{
    #region Variables

        [SerializeField] private SliderController healthbar;
        [SerializeField] private DeathGUIController deathScreen;

    #endregion

    private void OnEnable()
    {
        Hero.HealthUpdate += UpdateHeathBar;
    }

    private void OnDisable()
    {
        Hero.HealthUpdate -= UpdateHeathBar;
    }

    private void Start()
    {
        var hero = FindObjectOfType<Hero>();
        SetHealthBar(hero.Health, hero.StartingHealth);        
    }

    private void SetHealthBar(int currentHealth, int maxHealth)
    {
        healthbar.SetSliderValues(currentHealth, maxHealth);
    }

    private void UpdateHeathBar(int newHealthvalue)
    {
        healthbar.UpdateValue(newHealthvalue);

        if(newHealthvalue > 0) return;

        deathScreen.DeathOn();
    }


}
