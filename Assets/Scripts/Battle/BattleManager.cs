using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager
{
    public List<BattleEntity> turnOrder { get; private set; }
    public Queue<BattleEntity> actionQueue { get; private set; }
    public List<BattlePlayerEntity> players { get; private set; }
    public List<BattleEnemyEntity> enemies { get; private set; }
    public BattleEntity CurrentTurnEntity { get; private set; }
    public BattleScene scene { get; private set; }
    public bool IsVictory { get; set; }
    public BattleFunctionalItem ItemToUse { get; set; }
    public Skill SkillToUse { get; set; }
    public List<BattleCharacter> PlayerParty {get; private set;}

    public bool IsBossBattle 
    { 
        get 
        { 
            return Game.CurrentMap?.Mode == Map.MapMode.Zone && 
                   Game.CurrentMap?.CurrentZone == Game.CurrentMap?.Template?.NumberOfZones; 
        } 
    }

    public BattleManager(BattleScene scene)
    {
        this.scene = scene;
        PlayerParty = Game.Party.GetUnlockedMembers();
        InitializeBattle();
    }

    public BattleManager(BattleScene scene, MapTemplate testMap, PartyTemplate testPartyTemplate, bool testBossBattle){
        this.scene = scene;
        Game.CurrentMap = new Map(testMap, Map.MapMode.Zone);
        if (testBossBattle)
        {
            Game.CurrentMap.TestBossBattle();
        }
        PlayerParty = new Party(testPartyTemplate).GetUnlockedMembers();
        InitializeBattle();
    }

    void InitializeBattle()
    {
        if (Game.CurrentMap == null){
            Debug.LogError("Game.CurrentMap is null, cannot start battle");
            return;
        }
        List<EnemyTemplate> EnemiesToSpawn = Game.CurrentMap.GenerateEnemies();
        
        if (PlayerParty.Count > 4)
            PlayerParty = PlayerParty.GetRange(0, 4);

        turnOrder = new List<BattleEntity>();
        actionQueue = new Queue<BattleEntity>();

        players = new List<BattlePlayerEntity>();
        enemies = new List<BattleEnemyEntity>();
        // Instantiate all entities into battle
        List<BattleEntity> entities = new List<BattleEntity>();

        foreach (var player in PlayerParty)
        {
            BattlePlayerEntity entity = new BattlePlayerEntity(player, this);
            entities.Add(entity);
            players.Add(entity);
        }

        if (IsBossBattle)
        {
            Debug.Log("Boss battle");
            BattleBossEntity entity = new BattleBossEntity(EnemiesToSpawn[0], this);
            entities.Add(entity);
            enemies.Add(entity);
        }
        else
        {
            foreach (var enemyTemplate in EnemiesToSpawn)
            {
                BattleEnemyEntity entity = new BattleEnemyEntity(enemyTemplate, this);
                entities.Add(entity);
                enemies.Add(entity);
            }
        }


        // Sort by agility to determine initial turn order
        entities.Sort((a, b) => b.AGI.CompareTo(a.AGI));
        foreach (var entity in entities)
        {
            actionQueue.Enqueue(entity);
        }

        turnOrder = entities;
    }

    public void StartNextTurn()
    {
        if (actionQueue.Count == 0)
        {
            // Refill the queue
            foreach (var entity in turnOrder)
            {
                actionQueue.Enqueue(entity);
            }
        }

        CurrentTurnEntity = actionQueue.Dequeue();
        if (!CurrentTurnEntity.IsAlive)
        {
            return;
        }

        // Check if entity is stunned
        if (CurrentTurnEntity.IsStunned())
        {
            Debug.Log($"{CurrentTurnEntity.Name} is stunned and cannot act!");
            // Process HP debuffs even when stunned, then skip the turn
            CurrentTurnEntity.OnEndTurn();
            actionQueue.Enqueue(CurrentTurnEntity);
            scene.UpdateUI();
            bool isBattleEnded = CheckBattleOutcome();
            if (isBattleEnded)
            {
                EndBattle();
            }
            return;
        }

        if (CurrentTurnEntity is BattleEnemyEntity enemy)
        {
            enemy.TakeTurn();
        }
        else
        {
            // Player's turn, show options
            Debug.Log("Player's turn: " + CurrentTurnEntity.Name);
            scene.ShowPlayerOptions();
        }
    }

    public void OnPlayerAction(SelectionMode selectionMode, BattleEntity target)
    {
        if (selectionMode == SelectionMode.NormalAttack)
        {
            CurrentTurnEntity = actionQueue.Dequeue();
            // Play normal attack animation before performing attack
            scene.PlayNormalAttackAnimation(CurrentTurnEntity, target);
            CurrentTurnEntity.PerformNormalAttack(target);
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnSelf) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if (SkillToUse != null){
                // Play skill animation before using skill on self
                scene.PlaySkillAnimation(SkillToUse, CurrentTurnEntity, new List<BattleEntity> { CurrentTurnEntity });
                SkillToUse.Use(CurrentTurnEntity, new List<BattleEntity> { CurrentTurnEntity });
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnPartner) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if(ItemToUse != null) {
                ItemToUse.Use(new List<BattleEntity> { target });
                Game.Inventory.RemoveItem(ItemToUse, 1);
            }else if (SkillToUse != null){
                // Play skill animation before using skill
                scene.PlaySkillAnimation(SkillToUse, CurrentTurnEntity, new List<BattleEntity> { target });
                SkillToUse.Use(CurrentTurnEntity, new List<BattleEntity> { target });
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnPartnerAOE) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if(ItemToUse != null) {
                ItemToUse.Use(players.Cast<BattleEntity>().ToList());
                Game.Inventory.RemoveItem(ItemToUse, 1);
            }else if (SkillToUse != null){
                // Play skill animation before using AOE skill on partners
                scene.PlaySkillAnimation(SkillToUse, CurrentTurnEntity, players.Cast<BattleEntity>().ToList());
                SkillToUse.Use(CurrentTurnEntity, players.Cast<BattleEntity>().ToList());
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnOpponent) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if (SkillToUse != null){
                // Play skill animation before using skill on opponent
                scene.PlaySkillAnimation(SkillToUse, CurrentTurnEntity, new List<BattleEntity> { target });
                SkillToUse.Use(CurrentTurnEntity, new List<BattleEntity> { target });
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnOpponentAOE) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if (SkillToUse != null){
                // Play skill animation before using AOE skill on enemies
                scene.PlaySkillAnimation(SkillToUse, CurrentTurnEntity, enemies.Cast<BattleEntity>().ToList());
                SkillToUse.Use(CurrentTurnEntity, enemies.Cast<BattleEntity>().ToList());
            }
            EndTurn(CurrentTurnEntity);
        }
    }

    public void OnPlayerActionAOE(SelectionMode selectionMode)
    {
        //todo: implement AOE
    }

    public BattleEntity PeekNextEntity()
    {
        return actionQueue.Peek();
    }

    public void EndTurn(BattleEntity entity)
    {
        Debug.Log($"{entity.Name} ended their turn.");
        entity.OnEndTurn();
        ItemToUse = null;
        SkillToUse = null;
        actionQueue.Enqueue(entity);
        scene.UpdateUI();
        bool isBattleEnded = CheckBattleOutcome();
        if (isBattleEnded)
        {
            EndBattle();
        }

    }

    bool CheckBattleOutcome()
    {
        bool allEnemiesDefeated = GetAliveEnemies().Length == 0;
        bool allPlayersDefeated = GetAlivePlayers().Length == 0;
        bool isBattleEnded = allEnemiesDefeated || allPlayersDefeated;
        if (allEnemiesDefeated)
        {
            Debug.Log("Victory!");
            IsVictory = true;
        }
        else if (allPlayersDefeated)
        {
            Debug.Log("Defeat...");
            IsVictory = false;
        }
        return isBattleEnded;
    }

    public void EndBattle()
    {
        scene.StopBattleLoop();
        BattleReward reward = CalculateRewards();
        ApplyRewards(reward);
        scene.ShowRewardPanel(reward);
    }

    public BattleEntity[] GetAlivePlayers()
    {
        return turnOrder
            .FindAll(e => e is BattlePlayerEntity && e.IsAlive)
            .ToArray();
    }

    public BattleEntity[] GetAliveEnemies()
    {
        return turnOrder
            .FindAll(e => e is BattleEnemyEntity && e.IsAlive)
            .ToArray();
    }

    public List<BattleEntity> GetUpcomingTurnOrder(int maxCount)
    {
        List<BattleEntity> result = new List<BattleEntity>();
        Queue<BattleEntity> tempQueue = new Queue<BattleEntity>(actionQueue);

        while (result.Count < maxCount && tempQueue.Count > 0)
        {
            var entity = tempQueue.Dequeue();
            if (entity.IsAlive)
            {
                result.Add(entity);
            }
        }

        return result;
    }

    public BattleReward CalculateRewards()
    {
        BattleReward reward = new BattleReward();
        reward.IsVictory = IsVictory;
        if (reward.IsVictory)
        {
            foreach (var enemy in enemies)
            {
                reward.Money += enemy.dropMoney;
                reward.EXP += enemy.dropEXP;
            }
        }
        reward.PlayerParty = PlayerParty;
        return reward;
    }

    public void ApplyRewards(BattleReward reward)
    {
        // Apply rewards to players
        Game.AddMoney(reward.Money);
        int i = 0;
        foreach (var player in PlayerParty)
        {
            if (reward.IsVictory)
            {
                reward.isLevelUp[i] = player.GainEXP(reward.EXP);
            }
            i++;
        }
        if(reward.IsVictory){
            int[] enemyIds = enemies.Select(e => e.enemyId).ToArray();
            Game.QuestManager.UpdateAllQuests(enemyIds);
        }
    }
}
