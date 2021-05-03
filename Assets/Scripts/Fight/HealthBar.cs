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

    public void ChangeSliderColor()// Called on value change
    {
        bar.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetVal(int hp)
    {
        slider.value = hp;
    }
    public void SetMaxVal(int hp) //Starting setup for Health bar
    {
        slider.maxValue = hp;
    }

}
