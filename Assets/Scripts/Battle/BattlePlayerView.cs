using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattlePlayerView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Bar hpBar;
    [SerializeField] private Bar mpBar;
    [SerializeField] private EntityBuffDisplay buffDisplay;

    public BattlePlayerEntity Entity { get; private set; }

    public void Setup(BattlePlayerEntity playerEntity)
    {
        Entity = playerEntity;
        icon.sprite = playerEntity.Portrait;
        Render();
    }

    public void Render()
    {
        if(Entity == null) return;
        hpBar.Render(Entity.CurrentHP, Entity.Stats.HP);
        mpBar.Render(Entity.CurrentMP, Entity.Stats.MP);
        buffDisplay.Render(Entity.Buffs.ToArray());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Entity != null && Entity.IsAlive)
        {
            
        }
    }
}
