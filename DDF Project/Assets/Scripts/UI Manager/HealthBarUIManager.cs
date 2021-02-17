using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class HealthBarUIManager : MonoBehaviour
{
    [Required] [SerializeField] private Text healthText;
    [Required] [SerializeField] private Slider slider;
    public void Initialize(int min, int max)
    {
        slider.minValue = min;
        slider.maxValue = max;
        slider.wholeNumbers = true;
        slider.interactable = false;
        SetHealth(max);
    }

    public void SetHealth(int value)
    {
        slider.value = value;
        healthText.text = value.ToString();
    }
}
