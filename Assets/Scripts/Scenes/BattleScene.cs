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
        //add items for testing
        var hpPotion1 = DBManager.Instance.GetItem(1);
        var hpPotion2 = DBManager.Instance.GetItem(2);
        // Create test MP Potions
        var mpPotion1 = DBManager.Instance.GetItem(6);
        // Add items to inventory
        GameController.Instance.Inventory.InsertItem(hpPotion1, 1);  // Add 5 Healing Potion I
        GameController.Instance.Inventory.InsertItem(hpPotion2, 1);  // Add 3 Healing Potion II
        GameController.Instance.Inventory.InsertItem(mpPotion1, 1);  // Add 8 Magic Potion I

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
            if (BattleSceneLoader.IsBossBattle){
                battleBossList.gameObject.SetActive(true);
                battleEnemyList.gameObject.SetActive(false);
                battleBossList.Setup(manager.enemies[0]);
            }
            else{
                battleBossList.gameObject.SetActive(false);
                battleEnemyList.gameObject.SetActive(true);
                battleEnemyList.Setup(manager.enemies);
            }
        }
        else if (BattleSceneLoader.CurrentMap.Mode == Map.MapMode.Explore)
        {
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
        itemPanel.gameObject.SetActive(false);
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
        if (BattleSceneLoader.IsBossBattle){
            battleBossList.Render();
        }else{
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
    }
    public void OnClickEscape()
    {

    }
    public void OnClickEnemy(BattleEnemyEntity enemy)
    {
        if (selectionMode == SelectionMode.NormalAttack)
        {
            manager.OnPlayerAction(selectionMode, enemy);
            selectionMode = SelectionMode.None;
            battleTopBar.Hide();
            StopWaitingForPlayerInput();
            HidePlayerOptions();
        }
    }
    public void OnClickPlayer(int index)
    {
        BattlePlayerEntity player = manager.players[index];
        if (selectionMode == SelectionMode.UseOnPartner)
        {
            manager.OnPlayerAction(selectionMode, player);
            selectionMode = SelectionMode.None;
            battleTopBar.Hide();
            StopWaitingForPlayerInput();
            HidePlayerOptions();
        }
    }

    public void OnSelectItem(Item item)
    {
        BattleFunctionalItem battleItem = item as BattleFunctionalItem;
        //todo: use item on opponent
        if (!battleItem.IsUseOnOpponent()) {
            if (battleItem.IsAOE()) {
                selectionMode = SelectionMode.UseOnPartnerAOE;
                manager.ItemToUse = battleItem;
                manager.OnPlayerAction(selectionMode, null);
                selectionMode = SelectionMode.None;
                battleTopBar.Hide();
                StopWaitingForPlayerInput();
                HidePlayerOptions();
            } else {
                battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to use item on");
                selectionMode = SelectionMode.UseOnPartner;
                manager.ItemToUse = battleItem;
                itemPanel.Close();
            }
        }
    }

    public void ShowRewardPanel(BattleReward reward)
    {
        rewardPanel.gameObject.SetActive(true);
        rewardPanel.ShowRewardPanel(reward);
    }

}
