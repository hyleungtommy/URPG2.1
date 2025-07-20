using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// <summary> Show rewards after battle </summary>
public class RewardPanel : MonoBehaviour
{
    [SerializeField] Image bannerImage;
    [SerializeField] Sprite YouWinImage;
    [SerializeField] Sprite YouLoseImage;
    [SerializeField] Text money;
    [SerializeField] Text exp;
    [SerializeField] RewardPanelBattleCharacterPanel[] characterPanels;
    [SerializeField] Button buttonRebattle;
    [SerializeField] Button buttonLeave;
    [SerializeField] Button buttonNextZone;
    public void ShowRewardPanel(BattleReward battleReward)
    {
        gameObject.SetActive(true);
        if (battleReward.IsVictory)
        {
            bannerImage.sprite = YouWinImage;
        }
        else
        {
            bannerImage.sprite = YouLoseImage;
        }
        money.text = battleReward.Money.ToString();
        exp.text = battleReward.EXP.ToString();
        List<BattleCharacter> playerParty = BattleSceneLoader.PlayerParty;
        for (int i = 0; i < characterPanels.Length; i++)
        {
            if (i < playerParty.Count)
            {
                characterPanels[i].gameObject.SetActive(true);
                characterPanels[i].Render(playerParty[i], battleReward.isLevelUp[i]);
            }
            else
            {
                characterPanels[i].gameObject.SetActive(false);
            }
        }
        if (battleReward.IsVictory && BattleSceneLoader.CurrentMap.Mode == Map.MapMode.Zone)
        {
            if (BattleSceneLoader.CurrentMap.IsLastZone)
            {
                buttonNextZone.gameObject.SetActive(false);
            }
            else
            {
                buttonNextZone.gameObject.SetActive(true);
            }
            buttonRebattle.gameObject.SetActive(false);
            buttonLeave.gameObject.SetActive(true);
        }
        else if (battleReward.IsVictory && BattleSceneLoader.CurrentMap.Mode == Map.MapMode.Explore)
        {
            buttonNextZone.gameObject.SetActive(false);
            buttonRebattle.gameObject.SetActive(true);
            buttonLeave.gameObject.SetActive(true);
        }
        else if (!battleReward.IsVictory)
        {
            buttonNextZone.gameObject.SetActive(false);
            buttonRebattle.gameObject.SetActive(false);
            buttonLeave.gameObject.SetActive(true);
        }

    }

    public void OnClickRebattle()
    {
        BattleSceneLoader.LoadBattleScene(BattleSceneLoader.CurrentMap);
    }

    public void OnClickLeave()
    {
        if (BattleSceneLoader.CurrentMap.IsLastZone) {
            BattleSceneLoader.CurrentMap.ResetZoneProgress();
        }
        SceneManager.LoadScene("World");
        GameController.Instance.state = GameController.State.Idle;
    }

    public void OnClickNextZone()
    {
        BattleSceneLoader.CurrentMap.ProgressZone();
        Debug.Log("Next zone: " + BattleSceneLoader.CurrentMap.CurrentZone);
        BattleSceneLoader.LoadBattleScene(BattleSceneLoader.CurrentMap);
    }
    
}
