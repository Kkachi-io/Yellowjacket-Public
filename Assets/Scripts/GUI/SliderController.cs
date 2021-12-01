using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderController : MonoBehaviour
{
    #region Variables

        [SerializeField] private Slider slider;
        [SerializeField] private Slider slowSlider;

        [SerializeField] private float sliderSpeed;
        [SerializeField] private float slowSliderSpeed;

        private string sequenceName = "sliderAnimation";

    #endregion

    public void SetSliderValues(int currentValue, int maxValue)
    {
        SetSliderValues((float)currentValue, (float)maxValue);
    }

    public void SetSliderValues(float currentValue, float maxValue)
    {
        slider.maxValue = maxValue;
        slowSlider.maxValue = maxValue;

        slider.value = currentValue;
        slowSlider.value = currentValue;
    }

    public void UpdateValue(int newValue)
    {
        UpdateValue((float)newValue);
    }

    public void UpdateValue(float newValue)
    {
        if(DOTween.IsTweening(sequenceName))
            DOTween.Kill(sequenceName);

        Sequence sliderAnimation = DOTween.Sequence().SetId(sequenceName);
        sliderAnimation.Append(slider.DOValue(newValue, sliderSpeed))
                        .Insert(0, slowSlider.DOValue(newValue, slowSliderSpeed));

    }


}
