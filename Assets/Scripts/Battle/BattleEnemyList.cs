using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyList : MonoBehaviour
{
    [SerializeField] BattleEnemyView[] enemyViews = new BattleEnemyView[5];

    public void Setup(List<BattleEnemyEntity> enemies)
    {
        for (int i = 0; i < enemyViews.Length; i++)
        {
            if (i < enemies.Count)
            {
                var enemy = enemies[i];
                if (enemy != null)
                {
                    enemyViews[i].gameObject.SetActive(true);
                    enemyViews[i].Setup(enemy);
                }
                else
                {
                    enemyViews[i].gameObject.SetActive(false);
                }
            }
            else
            {
                enemyViews[i].gameObject.SetActive(false);
            }
        }

    }

    public void Render()
    {
        foreach (var enemyView in enemyViews)
        {
            if (enemyView.gameObject.activeSelf){
                if(enemyView.Entity.IsAlive){
                    enemyView.Render();
                }else {
                    enemyView.gameObject.SetActive(false);
                }
            }
        }
    }
}
