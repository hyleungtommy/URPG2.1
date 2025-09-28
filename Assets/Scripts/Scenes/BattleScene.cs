using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    [SerializeField] BattleTopBar battleTopBar;
    [SerializeField] ZonePanel zonePanel;
    [SerializeField] Image backgroundImage;
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
            manager = new BattleManager(this, testMap, testPartyTemplate, testBossBattle);
        }
        else
        {
            manager = new BattleManager(this);
        }

        if (Game.CurrentMap == null)
        {
            Debug.LogError("Game.CurrentMap is null, cannot start battle");
        }
        if (Game.CurrentMapMode == Map.MapMode.Zone)
        {
            zonePanel.gameObject.SetActive(true);
            zonePanel.Render(Game.CurrentMap);
            battleBossList.gameObject.SetActive(manager.IsBossBattle);
            battleEnemyList.gameObject.SetActive(!manager.IsBossBattle);
            if (manager.IsBossBattle)
            {
                battleBossList.Setup(manager.enemies[0]);
            }
            else
            {
                battleEnemyList.Setup(manager.enemies);
            }
        }
        else if (Game.CurrentMapMode == Map.MapMode.Explore)
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
        backgroundImage.sprite = Game.CurrentMap.Background;
        HidePlayerOptions();
        battleTopBar.Hide();
        itemPanel.Close();
        skillPanel.Close();
        rewardPanel.gameObject.SetActive(false);
        Game.State = GameState.Battle;

        UpdateUI();

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
        if (manager.IsBossBattle)
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
        if (manager.PeekNextEntity() is BattlePlayerEntity)
        {
            skillPanel.Open(((BattlePlayerEntity)manager.PeekNextEntity()).SkillList, manager.PeekNextEntity());
        }

    }
    public void OnClickEscape()
    {
        // Attempt to escape from battle
        BattleEntity currentEntity = manager.PeekNextEntity();
        if (currentEntity is BattlePlayerEntity playerEntity)
        {
            playerEntity.PerformEscapeAttempt();
            OnPlayerInputEnded();
        }
    }
    public void OnClickEnemy(BattleEnemyEntity enemy)
    {
        if (selectionMode == SelectionMode.NormalAttack)
        {
            manager.OnPlayerAction(selectionMode, enemy);
            OnPlayerInputEnded();
        }
        else if (selectionMode == SelectionMode.UseOnOpponent)
        {
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

    public void OnSelectSkill(Skill skill)
    {
        if (!skill.IsUseOnOpponent)
        {
            if (skill.IsAOE)
            {
                selectionMode = SelectionMode.UseOnPartnerAOE;
                manager.SkillToUse = skill;
                manager.OnPlayerAction(selectionMode, null);
                skillPanel.Close();
                OnPlayerInputEnded();
            }
            else if (skill.Type == SkillType.BuffSelf)
            {
                // BuffSelf skills automatically target the caster
                selectionMode = SelectionMode.UseOnSelf;
                manager.SkillToUse = skill;
                manager.OnPlayerAction(selectionMode, null);
                skillPanel.Close();
                OnPlayerInputEnded();
            }
            else
            {
                battleTopBar.SetTextAndShow(manager.PeekNextEntity().Name, "Select target to use skill on");
                selectionMode = SelectionMode.UseOnPartner;
                manager.SkillToUse = skill;
                skillPanel.Close();
            }
        }
        else
        {
            if (skill.IsAOE)
            {
                selectionMode = SelectionMode.UseOnOpponentAOE;
                manager.SkillToUse = skill;
                manager.OnPlayerAction(selectionMode, null);
                skillPanel.Close();
                OnPlayerInputEnded();
            }
            else
            {
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

    public void PlayNormalAttackAnimation(BattleEntity attacker, BattleEntity target)
    {
        if (SkillAnimationManager.Instance != null)
        {
            // Play animation at target's position
            Vector3 animationPosition = GetEntityPosition(target);
            SkillAnimationManager.Instance.PlayNormalAttackAnimation(animationPosition);
        }
    }

    public void PlayEnemyAttackAnimation(BattleEntity enemy, BattleEntity target)
    {
        if (SkillAnimationManager.Instance != null)
        {
            // Play enemy attack animation at target's position (player's position)
            Vector3 animationPosition = GetEntityPosition(target);
            SkillAnimationManager.Instance.PlayEnemyAttackAnimation(animationPosition);
        }
    }

    public void PlaySkillAnimation(Skill skill, BattleEntity user, List<BattleEntity> targets)
    {
        if (SkillAnimationManager.Instance != null)
        {
            if (skill.IsAOE)
            {
                // For AOE skills, play animation at center of all targets
                Vector3 centerPosition = Vector3.zero;
                foreach (var target in targets)
                {
                    centerPosition += GetEntityPosition(target);
                }
                centerPosition /= targets.Count;
                SkillAnimationManager.Instance.PlaySkillAnimation(skill, centerPosition);
            }
            else
            {
                // For single target skills, play animation at first target's position
                if (targets.Count > 0)
                {
                    Vector3 animationPosition = GetEntityPosition(targets[0]);
                    SkillAnimationManager.Instance.PlaySkillAnimation(skill, animationPosition);
                }
            }
        }
    }

    public Vector3 GetEntityPosition(BattleEntity entity)
    {
        // Try to find the entity in enemy views first
        if (entity is BattleEnemyEntity enemyEntity)
        {
            // Check regular enemy list
            if (battleEnemyList != null)
            {
                var enemyViews = battleEnemyList.GetComponentsInChildren<BattleEnemyView>();
                foreach (var enemyView in enemyViews)
                {
                    if (enemyView.Entity == enemyEntity && enemyView.gameObject.activeInHierarchy)
                    {
                        return GetWorldPositionFromUI(enemyView.GetImageTransform());
                    }
                }
            }
            
            // Check boss list
            if (battleBossList != null)
            {
                var bossViews = battleBossList.GetComponentsInChildren<BattleEnemyView>();
                foreach (var bossView in bossViews)
                {
                    if (bossView.Entity == enemyEntity && bossView.gameObject.activeInHierarchy)
                    {
                        return GetWorldPositionFromUI(bossView.GetImageTransform());
                    }
                }
            }
        }
        // Try to find the entity in player views
        else if (entity is BattlePlayerEntity playerEntity)
        {
            if (battlePlayerList != null)
            {
                var playerViews = battlePlayerList.GetComponentsInChildren<BattlePlayerView>();
                foreach (var playerView in playerViews)
                {
                    if (playerView.Entity == playerEntity && playerView.gameObject.activeInHierarchy)
                    {
                        return GetWorldPositionFromUI(playerView.GetImageTransform());
                    }
                }
            }
        }
        
        // Fallback: return zero if entity not found
        Debug.LogWarning($"Could not find UI position for entity: {entity.Name}");
        return Vector3.zero;
    }
    
    private Vector3 GetWorldPositionFromUI(Transform uiTransform)
    {
        if (uiTransform == null) return Vector3.zero;

        return uiTransform.position;
    }

    /// <summary>
    /// Shows floating damage numbers when an entity takes damage
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    /// <param name="target">The entity that took damage</param>
    public void ShowFloatingDamage(int damage, BattleEntity target)
    {
        FloatingNumberManager.Instance?.ShowDamageNumberOnEntity(damage, target, false);
    }
    
    /// <summary>
    /// Shows floating magical damage numbers with element icon when an entity takes magical damage
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    /// <param name="target">The entity that took damage</param>
    /// <param name="element">The element type of the magic</param>
    public void ShowFloatingMagicDamage(int damage, BattleEntity target, ElementType element)
    {
        Debug.Log($"Showing floating magical damage: {damage} on {target.Name} with element {element}");
        FloatingNumberManager.Instance?.ShowMagicDamageNumberOnEntity(damage, target, element, false);
    }
    
    /// <summary>
    /// Shows floating heal numbers when an entity is healed
    /// </summary>
    /// <param name="heal">The amount of healing done</param>
    /// <param name="target">The entity that was healed</param>
    public void ShowFloatingHeal(int heal, BattleEntity target)
    {
        FloatingNumberManager.Instance?.ShowHealNumberOnEntity(heal, target);
    }
    
    /// <summary>
    /// Shows floating mana regeneration numbers when an entity restores mana
    /// </summary>
    /// <param name="manaRegen">The amount of mana restored</param>
    /// <param name="target">The entity that restored mana</param>
    public void ShowFloatingManaRegen(int manaRegen, BattleEntity target)
    {
        FloatingNumberManager.Instance?.ShowManaRegenNumberOnEntity(manaRegen, target);
    }

}
