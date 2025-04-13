using UnityEngine;

public abstract class BattleEntity
{
    public string Name { get; private set; }
    public int AGI { get; private set; }
    public bool IsAlive => CurrentHP > 0;
    public Sprite Portrait { get; private set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public BaseStat Stats { get; private set; }
    protected BattleManager manager;

    public BattleEntity(string name, Sprite portrait, BaseStat baseStat, BattleManager manager)
    {
        Name = name;
        Portrait = portrait;
        Stats = baseStat;
        AGI = Stats.AGI;

        CurrentHP = Stats.HP;
        CurrentMP = Stats.MP;

        this.manager = manager;
    }

    public abstract void TakeTurn();

    public void PerformNormalAttack(BattleEntity target)
    {
        int damage = Mathf.Max(1, Stats.ATK - target.Stats.DEF);
        target.OnReceiveDamage(damage);
        Debug.Log($"{Name} attacks {target.Name} for {damage} damage!");
    }

    public virtual void OnReceiveDamage(int amount)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);
    }

    public virtual void OnEndTurn()
    {

    }

    public void PerformEscapeAttempt()
    {
        bool success = Random.value < 0.5f; // 50% success for now
        if (success)
        {
            Debug.Log($"{Name} escaped!");
            //manager.HandleEscapeSuccess();
        }
        else
        {
            Debug.Log($"{Name} failed to escape.");
            manager.EndTurn(this);
        }
    }

    public void Heal(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, Stats.HP);
    }

    public void RestoreMP(int amount)
    {
        CurrentMP = Mathf.Min(CurrentMP + amount, Stats.MP);
    }
    
}
