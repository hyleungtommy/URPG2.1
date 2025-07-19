using UnityEngine;
using System.Collections.Generic;
public abstract class BattleEntity
{
    public string Name { get; private set; }
    public int AGI { get; private set; }
    public bool IsAlive => CurrentHP > 0;
    public Sprite Portrait { get; private set; }
    public int CurrentHP { get; set; }
    public int CurrentMP { get; set; }
    public BaseStat BaseStats { get; private set; }
    public BaseStat Stats { get{
        return BaseStats.Multiply(new BuffMatrix(Buffs.ToArray()));
    }}
    public List<Buff> Buffs { get; private set; }
    protected BattleManager manager;

    public BattleEntity(string name, Sprite portrait, BaseStat baseStat, BattleManager manager)
    {
        Name = name;
        Portrait = portrait;
        BaseStats = baseStat;
        Buffs = new List<Buff>();
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
        foreach (Buff buff in Buffs){
            buff.duration--;
            if (buff.duration <= 0){
                Buffs.Remove(buff);
            }
        }
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

    public void AddBuff(BuffTemplate buffTemplate, int duration){
        Buff buff = new Buff(buffTemplate, duration);
        Buffs.Add(buff);
        Debug.Log($"{Name} has been buffed with {buff.buffType} for {buff.duration} turns!");
        foreach (int replaceBuffId in buffTemplate.replaceBuffIds){
            Buff replaceBuff = Buffs.Find(b => b.id == replaceBuffId);
            if (replaceBuff != null){
                Buffs.Remove(replaceBuff);
            }
        }
    }
    
}
