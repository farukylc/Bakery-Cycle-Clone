using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI moneyText;
    public int moneyAmount;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Customer":
                moneyAmount = moneyAmount + 5;
                moneyText.text = moneyAmount.ToString();
                break;
        }
    }
}
