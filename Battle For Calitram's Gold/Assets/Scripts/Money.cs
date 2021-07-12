using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    Spawner spawner;

    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        moneyText.text = spawner.GetMoney().ToString();
    }
}
