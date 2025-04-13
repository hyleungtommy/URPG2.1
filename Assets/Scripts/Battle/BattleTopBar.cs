using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTopBar : MonoBehaviour
{
    [SerializeField] Text topBarText;
    public void SetTextAndShow(string entityName, string text)
    {
        topBarText.text = $"{entityName}: {text}";
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
