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

    public BattleManager(BattleScene scene)
    {
        this.scene = scene;
        InitializeBattle();
    }

    void InitializeBattle()
    {
        turnOrder = new List<BattleEntity>();
        actionQueue = new Queue<BattleEntity>();

        players = new List<BattlePlayerEntity>();
        enemies = new List<BattleEnemyEntity>();
        // Instantiate all entities into battle
        List<BattleEntity> entities = new List<BattleEntity>();

        foreach (var player in BattleSceneLoader.PlayerParty)
        {
            BattlePlayerEntity entity = new BattlePlayerEntity(player, this);
            entities.Add(entity);
            players.Add(entity);
        }

        if (BattleSceneLoader.IsBossBattle)
        {
            Debug.Log("Boss battle");
            BattleBossEntity entity = new BattleBossEntity(BattleSceneLoader.EnemiesToSpawn[0], this);
            entities.Add(entity);
            enemies.Add(entity);
        }
        else
        {
            foreach (var enemyTemplate in BattleSceneLoader.EnemiesToSpawn)
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

        if (CurrentTurnEntity is BattleEnemyEntity enemy)
        {
            enemy.TakeTurn();
            return;
        }
        else
        {
            // Player's turn, show options
            Debug.Log("Player's turn: " + CurrentTurnEntity.Name);
            scene.ShowPlayerOptions();
            return;
        }
    }

    public void OnPlayerAction(SelectionMode selectionMode, BattleEntity target)
    {
        if (selectionMode == SelectionMode.NormalAttack)
        {
            CurrentTurnEntity = actionQueue.Dequeue();
            CurrentTurnEntity.PerformNormalAttack(target);
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnPartner) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if(ItemToUse != null) {
                ItemToUse.Use(new List<BattleEntity> { target });
                GameController.Instance.Inventory.RemoveItem(ItemToUse.id, 1);
            }else if (SkillToUse != null){
                SkillToUse.Use(CurrentTurnEntity, new List<BattleEntity> { target });
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnPartnerAOE) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if(ItemToUse != null) {
                ItemToUse.Use(players.Cast<BattleEntity>().ToList());
                GameController.Instance.Inventory.RemoveItem(ItemToUse.id, 1);
            }else if (SkillToUse != null){
                SkillToUse.Use(CurrentTurnEntity, players.Cast<BattleEntity>().ToList());
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnOpponent) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if (SkillToUse != null){
                SkillToUse.Use(CurrentTurnEntity, new List<BattleEntity> { target });
            }
            EndTurn(CurrentTurnEntity);
        }else if (selectionMode == SelectionMode.UseOnOpponentAOE) {
            CurrentTurnEntity = actionQueue.Dequeue();
            if (SkillToUse != null){
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
        bool allEnemiesDefeated = turnOrder.FindAll(e => e is BattleEnemyEntity && e.IsAlive).Count == 0;
        bool allPlayersDefeated = turnOrder.FindAll(e => e is BattlePlayerEntity && e.IsAlive).Count == 0;
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
        return reward;
    }

    public void ApplyRewards(BattleReward reward)
    {
        // Apply rewards to players
        GameController.Instance.AddMoney(reward.Money);
        int i = 0;
        foreach (var player in BattleSceneLoader.PlayerParty)
        {
            if (reward.IsVictory)
            {
                reward.isLevelUp[i] = player.GainEXP(reward.EXP);
            }
            i++;
        }
    }
}
