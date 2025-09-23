using UnityEngine;

public class BattleEnemyEntity: BattleEntity
{
    public int dropMoney { get; private set; }
    public int dropEXP { get; private set; }
    public int enemyId { get; private set; }
    public BattleEnemyEntity(EnemyTemplate enemyTemplate, BattleManager manager)
    :base(enemyTemplate.EnemyName, enemyTemplate.EnemyImage, enemyTemplate.ToBaseStat(), manager
    ){
        dropMoney = enemyTemplate.DropMoney;
        dropEXP = enemyTemplate.DropExp;
        enemyId = int.Parse(enemyTemplate.ID);
    }

    public override void TakeTurn(){
        // Basic AI: normal attack a random player
        // TODO: if enemy has taunt debuff attack the player who taunted them
        BattleEntity[] possibleTargets = manager.GetAlivePlayers();
        if (possibleTargets.Length == 0)
        {
            manager.EndTurn(this);
            return;
        }

        BattleEntity target = possibleTargets[Random.Range(0, possibleTargets.Length)];
        
        // Play enemy attack animation before performing attack
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.PlayEnemyAttackAnimation(this, target);
        }
        
        PerformNormalAttack(target);
        manager.EndTurn(this);
    }
}