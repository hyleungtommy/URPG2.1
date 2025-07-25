using System;
using UnityEngine;
using UnityEngine.UI;
public class BattleBossView : BattleEnemyView
{
    [SerializeField] Bar shieldBar;
    [SerializeField] Text damageMultiplierText;

    public override void Setup(BattleEnemyEntity enemyEntity)
    {
        base.Setup(enemyEntity);
        if (enemyEntity is BattleBossEntity)
        {
            shieldBar.gameObject.SetActive(true);
            damageMultiplierText.gameObject.SetActive(true);
            Render();
        }
    }

    public override void Render()
    {
        base.Render();
        if (Entity is BattleBossEntity boss)
        {
            shieldBar.Render(boss.CurrentShield, boss.MaxShield);
            damageMultiplierText.text = $"Damage: {Math.Round(boss.DamageMultiplier * 100)}%";
        }
    }
}
