using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image image;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        image.color = gradient.Evaluate(slider.normalizedValue) ;
    }

    public void SetText(int health, int maxHealth)
    {
        text.text = health + "/" + maxHealth;
    }
}
