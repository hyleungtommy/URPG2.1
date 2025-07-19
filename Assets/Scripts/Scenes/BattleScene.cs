using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    [SerializeField] BattleTopBar battleTopBar;
    [SerializeField] ZonePanel zonePanel;
    [SerializeField] BattlePlayerList battlePlayerList;
    [SerializeField] BattleEnemyList battleEnemyList;
    [SerializeField] BattleBossList battleBossList;
    [SerializeField] Image[] playerImages;
    [SerializeField] ActionOrder actionOrder;
    [SerializeField] ActionButtonGroup actionButtonGroup;
    [SerializeField] bool testMode = false;
    [SerializeField] bool testBossBattle = false;
    [SerializeField] MapTemplate testMap;
    [SerializeField] PartyTemplate testPartyTemplate;
    [SerializeField] RewardPanel rewardPanel;
    [SerializeField] ItemPanel itemPanel;
    [SerializeField] BattleSkillPanel skillPanel;
    BattleManager manager;
    private bool isWaitingForPlayerInput = false;
    public SelectionMode selectionMode = SelectionMode.None;
    public static BattleScene Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PrepareBattle();
    }

    void Update()
    {
        // For testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            SkipBattle();
        }
    }

    public void PrepareBattle()
    {
        if (testMode)
        {
            BattleSceneLoader.LoadTestBattleScene(testMap, testPartyTemplate, testBossBattle);
        }
        manager = new BattleManager(this);

        if (BattleSceneLoader.CurrentMap == null)
        {
            Debug.LogError("BattleManager failed to initialize properly.");
            return;
        }
        if (BattleSceneLoader.CurrentMap.Mode == Map.MapMode.Zone)
        {
            zonePanel.gameObject.SetActive(true);
            zonePanel.Render(BattleSceneLoader.CurrentMap);
            battleBossList.gameObject.SetActive(BattleSceneLoader.IsBossBattle);
            battleEnemyList.gameObject.SetActive(!BattleSceneLoader.IsBossBattle);
            if (BattleSceneLoader.IsBossBattle)
            {
                battleBossList.Setup(manager.enemies[0]);
            }
            else
            {
                battleEnemyList.Setup(manager.enemies);
            }
        }
        else if (BattleSceneLoader.CurrentMap.Mode == Map.MapMode.Explore)
        {
            battleEnemyList.gameObject.SetActive(true);
            battleBossList.gameObject.SetActive(false);
            battleEnemyList.Setup(manager.enemies);
            zonePanel.gameObject.SetActive(false);
        }

        battlePlayerList.Setup(manager.players);

        // setup player battle portraits
        for (int i = 0; i < playerImages.Length; i++)
        {
            if (i < manager.players.Count)
            {
                var player = manager.players[i];
                if (player != null)
                {
                    playerImages[i].gameObject.SetActive(true);
                    playerImages[i].sprite = player.BattlePortrait;
                }
                else
                {
                    playerImages[i].gameObject.SetActive(false);
                }
            }
            else
            {
                playerImages[i].gameObject.SetActive(false);
            }
        }

        actionOrder.Render(manager.turnOrder);
        HidePlayerOptions();
        battleTopBar.Hide();
        itemPanel.Close();
        skillPanel.Close();
        rewardPanel.gameObject.SetActive(false);
        StartBattleLoop();
    }

    public void SkipBattle()
    {
        manager.IsVictory = true;
        manager.EndBattle();
    }

    public void StartBattleLoop()
    {
        StartCoroutine(BattleLoop());
    }

    public void StopBattleLoop()
    {
        StopCoroutine(BattleLoop());
    }

    public IEnumerator BattleLoop()
    {
        while (true)
        {
            BattleEntity entity = manager.PeekNextEntity();

            if (!entity.IsAlive)
            {
                Debug.Log($"{entity.Name} is dead, skipping turn...");
                manager.StartNextTurn(); // Skips dead units automatically
                continue;
            }

            if (entity is BattleEnemyEntity)
            {
                Debug.Log($"{entity.Name}'s turn - enemy action...");
                manager.StartNextTurn(); // Enemy acts
                yield return new WaitForSeconds(1f); // Wait for enemy action to complete (e.g. animation)
            }
            else if (entity is BattlePlayerEntity)
            {
                isWaitingForPlayerInput = true;
                Debug.Log($"{entity.Name}'s turn - waiting for player input...");
                ShowPlayerOptions();
                yield return new WaitUntil(() => isWaitingForPlayerInput == false);
            }
        }
    }

    public void ShowPlayerOptions()
    {

        actionButtonGroup.gameObject.SetActive(true);
        battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Choose an action");
    }

    public void HidePlayerOptions()
    {
        actionButtonGroup.gameObject.SetActive(false);
    }

    public void StopWaitingForPlayerInput()
    {
        isWaitingForPlayerInput = false;
    }

    public void UpdateUI()
    {
        battlePlayerList.Render();
        if (BattleSceneLoader.IsBossBattle)
        {
            battleBossList.Render();
        }
        else
        {
            battleEnemyList.Render();
        }
        actionOrder.Render(manager.GetUpcomingTurnOrder(6));
    }

    public void OnClickAttack()
    {
        battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to attack");
        selectionMode = SelectionMode.NormalAttack;
    }

    public void OnClickItem()
    {
        battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select An Item to use");
        itemPanel.Open();
    }
    public void OnClickSkill()
    {
        battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select a skill to use");
        if (manager.PeekNextEntity() is BattlePlayerEntity){
            skillPanel.Open(((BattlePlayerEntity)manager.PeekNextEntity()).SkillList, manager.PeekNextEntity());
        }

    }
    public void OnClickEscape()
    {

    }
    public void OnClickEnemy(BattleEnemyEntity enemy)
    {
        if (selectionMode == SelectionMode.NormalAttack)
        {
            manager.OnPlayerAction(selectionMode, enemy);
            OnPlayerInputEnded();
        }else if (selectionMode == SelectionMode.UseOnOpponent){
            manager.OnPlayerAction(selectionMode, enemy);
            OnPlayerInputEnded();
        }
    }
    public void OnClickPlayer(int index)
    {
        BattlePlayerEntity player = manager.players[index];
        if (selectionMode == SelectionMode.UseOnPartner)
        {
            manager.OnPlayerAction(selectionMode, player);
            OnPlayerInputEnded();
        }
    }

    public void OnSelectItem(Item item)
    {
        BattleFunctionalItem battleItem = item as BattleFunctionalItem;
        if (!battleItem.IsUseOnOpponent())
        {
            if (battleItem.IsAOE())
            {
                selectionMode = SelectionMode.UseOnPartnerAOE;
                manager.ItemToUse = battleItem;
                manager.OnPlayerAction(selectionMode, null);
                itemPanel.Close();
                OnPlayerInputEnded();
            }
            else
            {
                battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to use item on");
                selectionMode = SelectionMode.UseOnPartner;
                manager.ItemToUse = battleItem;
                itemPanel.Close();
            }
        }
    }

    public void OnSelectSkill(Skill skill){
        if (!skill.IsUseOnOpponent){
            if(skill.IsAOE){
                selectionMode = SelectionMode.UseOnPartnerAOE;
                manager.SkillToUse = skill;
                manager.OnPlayerAction(selectionMode, null);
                skillPanel.Close();
                OnPlayerInputEnded();
            }
            else{
                battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to use skill on");
                selectionMode = SelectionMode.UseOnPartner;
                manager.SkillToUse = skill;
                skillPanel.Close();
            }
        }else {
            if(skill.IsAOE){
                selectionMode = SelectionMode.UseOnOpponentAOE;
                manager.SkillToUse = skill;
                manager.OnPlayerAction(selectionMode, null);
                skillPanel.Close();
                OnPlayerInputEnded();
            }else {
                battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to use skill on");
                selectionMode = SelectionMode.UseOnOpponent;
                manager.SkillToUse = skill;
                skillPanel.Close();
            }
        }
    }

    private void OnPlayerInputEnded()
    {
        selectionMode = SelectionMode.None;
        battleTopBar.Hide();
        StopWaitingForPlayerInput();
        HidePlayerOptions();
    }

    public void ShowRewardPanel(BattleReward reward)
    {
        rewardPanel.gameObject.SetActive(true);
        rewardPanel.ShowRewardPanel(reward);
    }

}
