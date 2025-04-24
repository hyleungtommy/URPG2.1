using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBar : MonoBehaviour
{
    public Text textMoney;
    // Start is called before the first frame update
    void Start()
    {
        Render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Render()
    {
        textMoney.text = "Money: " + GameController.Instance.money;
    }

    public void OnClickStatus()
    {
    }

    public void OnClickUpgrade()
    {
    }

    public void OnClickSkill()
    {
    }

    public void OnClickEquipment()
    {
    }

    public void OnClickPartyManagement()
    {
    }

    public void OnClickInventory()
    {

    }

    public void OnClickQuestJournal()
    {
    }

    public void OnClickSettings()
    {
    }
}
