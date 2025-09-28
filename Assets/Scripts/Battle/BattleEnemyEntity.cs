using UnityEngine;

public class BattleEnemyEntity : BattleEntity
{
    public int dropMoney { get; private set; }
    public int dropEXP { get; private set; }
    public int enemyId { get; private set; }
    public BattleEnemyEntity(EnemyTemplate enemyTemplate, BattleManager manager)
    : base(enemyTemplate.EnemyName, enemyTemplate.EnemyImage, enemyTemplate.ToBaseStat(), manager, enemyTemplate.ToElementResistance()
    )
    {
        dropMoney = enemyTemplate.DropMoney;
        dropEXP = enemyTemplate.DropExp;
        enemyId = int.Parse(enemyTemplate.ID);
    }

    public override void TakeTurn()
    {
        // Start the enemy turn as a coroutine to handle delays
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.StartCoroutine(EnemyTurnCoroutine());
        }
        else
        {
            // Fallback if BattleScene is not available
            PerformEnemyAction();
        }
    }

    private System.Collections.IEnumerator EnemyTurnCoroutine()
    {
        // Taunt AI: prioritize players with taunt buff, otherwise attack random player
        BattleEntity[] tauntedPlayers = manager.GetTauntedPlayers();
        BattleEntity target;

        if (tauntedPlayers.Length > 0)
        {
            // Attack a random taunted player
            target = tauntedPlayers[Random.Range(0, tauntedPlayers.Length)];
            Debug.Log($"{Name} is taunted and attacks {target.Name}!");
        }
        else
        {
            // No taunted players, attack random player
            BattleEntity[] possibleTargets = manager.GetAlivePlayers();
            if (possibleTargets.Length == 0)
            {
                manager.EndTurn(this);
                yield break;
            }

            target = possibleTargets[Random.Range(0, possibleTargets.Length)];
        }

        // Wait for 1 second to let the animation play
        yield return new WaitForSeconds(1f);

        // Play enemy attack animation before performing attack
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.PlayEnemyAttackAnimation(this, target);
        }

        PerformNormalAttack(target);
        manager.EndTurn(this);
    }

    private void PerformEnemyAction()
    {
        // Fallback method without coroutine - also implements taunt logic
        BattleEntity[] tauntedPlayers = manager.GetTauntedPlayers();
        BattleEntity target;

        if (tauntedPlayers.Length > 0)
        {
            // Attack a random taunted player
            target = tauntedPlayers[Random.Range(0, tauntedPlayers.Length)];
            Debug.Log($"{Name} is taunted and attacks {target.Name}!");
        }
        else
        {
            // No taunted players, attack random player
            BattleEntity[] possibleTargets = manager.GetAlivePlayers();
            if (possibleTargets.Length == 0)
            {
                manager.EndTurn(this);
                return;
            }

            target = possibleTargets[Random.Range(0, possibleTargets.Length)];
        }

        // Play enemy attack animation before performing attack
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.PlayEnemyAttackAnimation(this, target);
        }

        PerformNormalAttack(target);
        manager.EndTurn(this);
    }
}