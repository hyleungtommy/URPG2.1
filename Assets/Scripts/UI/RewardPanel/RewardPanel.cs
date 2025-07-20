using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI component responsible for displaying battle results and rewards to the player.
/// Shows victory/defeat status, money/EXP gained, character level-ups, and provides navigation options.
/// </summary>
public class RewardPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image bannerImage;
    [SerializeField] private Sprite YouWinImage;
    [SerializeField] private Sprite YouLoseImage;
    [SerializeField] private Text money;
    [SerializeField] private Text exp;
    [SerializeField] private RewardPanelBattleCharacterPanel[] characterPanels;
    
    [Header("Action Buttons")]
    [SerializeField] private Button buttonRebattle;
    [SerializeField] private Button buttonLeave;
    [SerializeField] private Button buttonNextZone;

    /// <summary>
    /// Displays the reward panel with battle results and configures appropriate action buttons.
    /// Sets up victory/defeat banner, reward amounts, character level-up notifications, and button states.
    /// </summary>
    /// <param name="battleReward">The battle reward data containing victory status, money, EXP, and level-up information</param>
    /// <exception cref="System.ArgumentNullException">Thrown when battleReward is null</exception>
    public void ShowRewardPanel(BattleReward battleReward)
    {
        if (battleReward == null)
        {
            throw new System.ArgumentNullException(nameof(battleReward), "Battle reward cannot be null");
        }

        gameObject.SetActive(true);
        
        // Set victory/defeat banner
        bannerImage.sprite = battleReward.IsVictory ? YouWinImage : YouLoseImage;
        
        // Display reward amounts
        money.text = battleReward.Money.ToString();
        exp.text = battleReward.EXP.ToString();
        
        // Setup character panels
        SetupCharacterPanels(battleReward);
        
        // Configure action buttons based on battle outcome and map mode
        ConfigureActionButtons(battleReward.IsVictory);
    }

    /// <summary>
    /// Sets up character panels to display level-up notifications for each party member.
    /// Shows only panels for active party members and hides unused panels.
    /// </summary>
    /// <param name="battleReward">The battle reward containing level-up information</param>
    private void SetupCharacterPanels(BattleReward battleReward)
    {
        List<BattleCharacter> playerParty = BattleSceneLoader.PlayerParty;
        
        if (playerParty == null)
        {
            Debug.LogWarning("Player party is null, cannot setup character panels");
            return;
        }

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
    }

    /// <summary>
    /// Configures the visibility and availability of action buttons based on battle outcome and map mode.
    /// Different button combinations are shown for victory/defeat and zone/explore modes.
    /// </summary>
    /// <param name="battleReward">The battle reward containing victory status</param>
    private void ConfigureActionButtons(bool IsVictory)
    {
        if (IsVictory && BattleSceneLoader.CurrentMap?.Mode == Map.MapMode.Zone)
        {
            // Zone mode victory - show next zone button if not on last zone
            buttonNextZone.gameObject.SetActive(!BattleSceneLoader.CurrentMap.IsLastZone);
            buttonRebattle.gameObject.SetActive(false);
            buttonLeave.gameObject.SetActive(true);
        }
        else if (IsVictory && BattleSceneLoader.CurrentMap?.Mode == Map.MapMode.Explore)
        {
            // Explore mode victory - allow re-battling
            buttonNextZone.gameObject.SetActive(false);
            buttonRebattle.gameObject.SetActive(true);
            buttonLeave.gameObject.SetActive(true);
        }
        else if (!IsVictory)
        {
            // Defeat - only allow leaving
            buttonNextZone.gameObject.SetActive(false);
            buttonRebattle.gameObject.SetActive(false);
            buttonLeave.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Handles the re-battle button click event.
    /// Reloads the current battle scene with the same map configuration.
    /// </summary>
    public void OnClickRebattle()
    {
        if (BattleSceneLoader.CurrentMap == null)
        {
            Debug.LogError("Current map is null, cannot start re-battle");
            return;
        }

        BattleSceneLoader.LoadBattleScene(BattleSceneLoader.CurrentMap);
    }

    /// <summary>
    /// Handles the leave button click event.
    /// Resets zone progress if on last zone, then returns to the world scene.
    /// Clears battle data to prevent memory leaks.
    /// </summary>
    public void OnClickLeave()
    {
        if (BattleSceneLoader.CurrentMap?.IsLastZone == true)
        {
            BattleSceneLoader.CurrentMap.ResetZoneProgress();
        }
        
        SceneManager.LoadScene("World");
        GameController.Instance.state = GameController.State.Idle;
        BattleSceneLoader.ClearBattleData();
    }

    /// <summary>
    /// Handles the next zone button click event.
    /// Advances to the next zone and loads the battle scene for that zone.
    /// </summary>
    public void OnClickNextZone()
    {
        if (BattleSceneLoader.CurrentMap == null)
        {
            Debug.LogError("Current map is null, cannot progress to next zone");
            return;
        }

        BattleSceneLoader.CurrentMap.ProgressZone();
        Debug.Log($"Next zone: {BattleSceneLoader.CurrentMap.CurrentZone}");
        BattleSceneLoader.LoadBattleScene(BattleSceneLoader.CurrentMap);
    }
}
