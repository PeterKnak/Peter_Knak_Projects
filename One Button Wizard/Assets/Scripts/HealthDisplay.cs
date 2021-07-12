using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float newHealth)
    {
        slider.value = newHealth;
    }

}
