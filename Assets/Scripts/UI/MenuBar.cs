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
        textMoney.text = "Money: " + Game.Money;
    }

    public void OnClickStatus()
    {
        UIController.Instance.OpenUIScene("Status");
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
        UIController.Instance.OpenUIScene("Inventory");
    }

    public void OnClickQuestJournal()
    {
        UIController.Instance.OpenUIScene("QuestBoard");
    }

    public void OnClickSettings()
    {
    }
}
