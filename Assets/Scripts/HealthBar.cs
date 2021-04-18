using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] Image bar;
    [SerializeField] private Gradient gradient;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeSliderColor()
    {
        bar.color = gradient.Evaluate(slider.normalizedValue);
    }

    internal void SetVal(int hp)
    {
        slider.value = hp;
    }
    internal void SetMaxVal(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

}
