using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerMeter : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

  

    public void SetMinAnger(float anger)
    {
        slider.minValue = anger;
        slider.value = anger;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetAnger(float anger)
    {
        slider.value = anger;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
