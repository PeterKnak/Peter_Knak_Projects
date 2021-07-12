using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    int health = 200;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
    }

    public void UpdateHealth(int newHealth)
    {
        health = Mathf.Max(0, newHealth);
    }
}
