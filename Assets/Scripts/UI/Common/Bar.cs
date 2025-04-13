using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image barStart;
    [SerializeField] private Image barEnd;
    [SerializeField] private Text amount;

    public void Render(int currentValue, int maxValue)
    {
        fill.fillAmount = currentValue / (float)maxValue;
        if (amount != null){
            amount.text = $"{currentValue}/{maxValue}";
        }
        barStart.enabled = currentValue > 0;
        barEnd.enabled = currentValue == maxValue;
    }
}
