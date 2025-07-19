using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleEnemyView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private Text enemyName;
    [SerializeField] private Bar hpBar;
    [SerializeField] private EntityBuffDisplay buffDisplay;

    public BattleEnemyEntity Entity { get; private set; }

    public virtual void Setup(BattleEnemyEntity enemyEntity)
    {
        Entity = enemyEntity;
        enemyImage.sprite = enemyEntity.Portrait;
        enemyName.text = enemyEntity.Name;
    }

    public virtual void Render()
    {
        if (Entity == null) return;
        hpBar.Render(Entity.CurrentHP, Entity.Stats.HP);
        buffDisplay.Render(Entity.Buffs.ToArray());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Entity != null && Entity.IsAlive)
        {
            BattleScene.Instance.OnClickEnemy(Entity);
        }
    }
}
