using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Abstract base class representing a combat entity in the turn-based battle system.
/// </summary>
/// <remarks>
/// <para>
/// Performance Considerations:
/// - Stats property recalculates buff matrix on every access; consider caching for performance-critical scenarios
/// - Buff removal in OnEndTurn() modifies collection during iteration; consider using RemoveAll() or separate collection
/// - AGI is cached at construction but doesn't update with buffs; may need refresh mechanism
/// </para>
/// </remarks>
public abstract class BattleEntity
{
    /// <summary>
    /// The display name of the entity in battle UI and logs.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The agility stat used for turn order determination.
    /// Note: This value is cached at construction and doesn't update with buffs.
    /// </summary>
    public int AGI { get; private set; }

    /// <summary>
    /// Indicates whether the entity is alive and can participate in combat.
    /// Returns true if CurrentHP is greater than 0.
    /// </summary>
    public bool IsAlive => CurrentHP > 0;

    /// <summary>
    /// The portrait sprite displayed in battle UI elements.
    /// </summary>
    public Sprite Portrait { get; private set; }

    /// <summary>
    /// Current health points. Automatically clamped between 0 and maximum HP.
    /// </summary>
    public int CurrentHP { get; set; }

    /// <summary>
    /// Current mana points. Automatically clamped between 0 and maximum MP.
    /// </summary>
    public int CurrentMP { get; set; }

    /// <summary>
    /// The base statistics of the entity before any buffs or debuffs are applied.
    /// </summary>
    public BaseStat BaseStats { get; private set; }

    /// <summary>
    /// The effective statistics of the entity including all active buffs and debuffs.
    /// Calculated by multiplying base stats with the buff matrix.
    /// </summary>
    public BaseStat Stats
    {
        get
        {
            return BaseStats.Multiply(new BuffMatrix(Buffs.ToArray()));
        }
    }

    /// <summary>
    /// Collection of active buffs and debuffs affecting the entity.
    /// </summary>
    public List<Buff> Buffs { get; private set; }

    /// <summary>
    /// Reference to the battle manager controlling the current battle.
    /// </summary>
    protected BattleManager manager;

    /// <summary>
    /// Initializes a new instance of the BattleEntity class.
    /// </summary>
    /// <param name="name">The display name of the entity</param>
    /// <param name="portrait">The portrait sprite for UI display</param>
    /// <param name="baseStat">The base statistics of the entity</param>
    /// <param name="manager">The battle manager controlling the battle</param>
    /// <exception cref="System.ArgumentNullException">Thrown when name, portrait, baseStat, or manager is null</exception>
    public BattleEntity(string name, Sprite portrait, BaseStat baseStat, BattleManager manager)
    {
        // Validate parameters
        if (string.IsNullOrEmpty(name))
            throw new System.ArgumentNullException(nameof(name), "Entity name cannot be null or empty");
        if (portrait == null)
            throw new System.ArgumentNullException(nameof(portrait), "Portrait cannot be null");
        if (baseStat == null)
            throw new System.ArgumentNullException(nameof(baseStat), "Base stats cannot be null");
        if (manager == null)
            throw new System.ArgumentNullException(nameof(manager), "Battle manager cannot be null");

        Name = name;
        Portrait = portrait;
        BaseStats = baseStat;
        Buffs = new List<Buff>();

        // Initialize current stats from base stats
        CurrentHP = Stats.HP;
        CurrentMP = Stats.MP;

        // Cache AGI for turn order (note: doesn't update with buffs)
        AGI = Stats.AGI;

        this.manager = manager;
    }

    /// <summary>
    /// Abstract method that defines the entity's behavior during its turn.
    /// Must be implemented by derived classes to provide specific turn logic.
    /// </summary>
    public abstract void TakeTurn();

    /// <summary>
    /// Performs a normal attack against the specified target.
    /// </summary>
    /// <param name="target">The entity to attack</param>
    /// <exception cref="System.ArgumentNullException">Thrown when target is null</exception>
    public void PerformNormalAttack(BattleEntity target)
    {
        PerformAttack(target, 1);
    }

    public void PerformSkillAttack(BattleEntity target, Skill skill)
    {
        PerformAttack(target, skill.Modifier);
    }

    private void PerformAttack(BattleEntity target, float modifier)
    {
        if (target == null)
            throw new System.ArgumentNullException(nameof(target), "Attack target cannot be null");
        int damage = CalculateDamage(target, modifier);
        target.OnReceiveDamage(damage);
        Debug.Log($"{Name} attacks {target.Name} for {damage} damage!");
    }

    private int CalculateDamage(BattleEntity target, float modifier)
    {
        int rawDamage = Mathf.Max(1, Stats.ATK - target.Stats.DEF);
        int damage = (int)(rawDamage * modifier);
        return damage;
    }

    /// <summary>
    /// Handles damage received by the entity.
    /// </summary>
    /// <param name="amount">The amount of damage to receive</param>
    public virtual void OnReceiveDamage(int amount)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);
        
        // Show floating damage number
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.ShowFloatingDamage(amount, this);
        }
    }

    /// <summary>
    /// Processes end-of-turn effects, including buff duration management.
    /// </summary>
    public virtual void OnEndTurn()
    {
        // TODO: Fix potential collection modification during iteration
        // Consider using RemoveAll() or a separate collection for items to remove
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buff buff = Buffs[i];
            buff.duration--;
            if (buff.duration <= 0)
            {
                Buffs.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Attempts to escape from the current battle.
    /// </summary>
    public void PerformEscapeAttempt()
    {
        bool success = Random.value < 0.5f; // 50% success for now
        if (success)
        {
            Debug.Log($"{Name} escaped!");
            manager.EndBattle();
        }
        else
        {
            Debug.Log($"{Name} failed to escape.");
            manager.EndTurn(this);
        }
    }

    /// <summary>
    /// Restores health points to the entity.
    /// </summary>
    /// <param name="amount">The amount of health to restore</param>
    public void Heal(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, Stats.HP);
    }

    /// <summary>
    /// Restores mana points to the entity.
    /// </summary>
    /// <param name="amount">The amount of mana to restore</param>
    public void RestoreMP(int amount)
    {
        CurrentMP = Mathf.Min(CurrentMP + amount, Stats.MP);
    }

    /// <summary>
    /// Adds a buff to the entity and handles buff replacement logic.
    /// </summary>
    /// <param name="buffTemplate">The template defining the buff to add</param>
    /// <param name="duration">The duration of the buff in turns</param>
    /// <exception cref="System.ArgumentNullException">Thrown when buffTemplate is null</exception>
    public void AddBuff(BuffTemplate buffTemplate, int duration)
    {
        if (buffTemplate == null)
            throw new System.ArgumentNullException(nameof(buffTemplate), "Buff template cannot be null");

        Buff buff = new Buff(buffTemplate, duration);
        Buffs.Add(buff);
        Debug.Log($"{Name} has been buffed with {buff.buffType} for {buff.duration} turns!");

        // Handle buff replacement logic
        foreach (int replaceBuffId in buffTemplate.replaceBuffIds)
        {
            Buff replaceBuff = Buffs.Find(b => b.id == replaceBuffId);
            if (replaceBuff != null)
            {
                Buffs.Remove(replaceBuff);
            }
        }
    }
}
