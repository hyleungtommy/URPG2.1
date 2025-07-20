using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonGroup : MonoBehaviour
{
    [SerializeField] Button buttonAttack;
    [SerializeField] Button buttonItem;
    [SerializeField] Button buttonSkill;
    [SerializeField] Button buttonEscape;
    [SerializeField] BattleScene battleScene;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void OnClickAttack()
    {
        Debug.Log("OnClickAttack");
        battleScene.OnClickAttack();
    }
    public void OnClickItem()
    {
        battleScene.OnClickItem();
    }
    public void OnClickSkill()
    {
        battleScene.OnClickSkill();
    }
    public void OnClickEscape()
    {
        battleScene.OnClickEscape();
    }
}
