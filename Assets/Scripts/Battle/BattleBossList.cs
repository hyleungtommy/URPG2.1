using UnityEngine;

public class BattleBossList : MonoBehaviour
{
    [SerializeField] BattleBossView bossView;
    public void Setup(BattleEnemyEntity boss){
        bossView.Setup(boss);
    }
    public void Render(){
        bossView.Render();
    }
}
