using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderBar : UIBar
{
    private Slider slider;
    private float start;
    private float end;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        start = 0;
        end = 1;
    }

    public override void Scale(float percentage)
    {
        slider.value = ScaleWithinBounds(start,end,percentage);
    }
}
