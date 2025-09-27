using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
        target.OnReceiveDamage(damage, this);
        Debug.Log($"{Name} attacks {target.Name} for {damage} damage!");
    }

    private int CalculateDamage(BattleEntity target, float modifier)
    {
        int rawDamage = Mathf.Max(1, Stats.ATK - target.Stats.DEF);
        int damage = (int)(rawDamage * modifier);

        // Apply counter attack defense buff reduction (35% damage reduction)
        if (target.HasBuff("Counter_Attack_Defense"))
        {
            damage = Mathf.Max(1, (int)(damage * 0.65f)); // Reduce damage by 35%, minimum 1 damage
            Debug.Log($"{target.Name}'s Counter Attack Defense buff reduces damage from {(int)(rawDamage * modifier)} to {damage}!");
        }
        // Apply healing defense buff reduction (35% damage reduction)
        else if (target.HasBuff("Healing_Defense"))
        {
            damage = Mathf.Max(1, (int)(damage * 0.65f)); // Reduce damage by 35%, minimum 1 damage
            Debug.Log($"{target.Name}'s Healing Defense buff reduces damage from {(int)(rawDamage * modifier)} to {damage}!");
        }
        // Apply regular defense buff reduction (50% damage reduction) - lowest priority
        else if (target.HasBuff("Defense"))
        {
            damage = Mathf.Max(1, damage / 2); // Reduce damage by 50%, minimum 1 damage
            Debug.Log($"{target.Name}'s Defense buff reduces damage from {rawDamage} to {damage}!");
        }

        return damage;
    }

    /// <summary>
    /// Handles damage received by the entity.
    /// </summary>
    /// <param name="amount">The amount of damage to receive</param>
    /// <param name="attacker">The entity that dealt the damage (for counter attack reflection)</param>
    public virtual void OnReceiveDamage(int amount, BattleEntity attacker = null)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);

        // Show floating damage number
        if (BattleScene.Instance != null)
        {
            BattleScene.Instance.ShowFloatingDamage(amount, this);
        }

        // Process counter attack defense reflection (25% of damage reflected back)
        if (attacker != null && HasBuff("Counter_Attack_Defense"))
        {
            int reflectedDamage = Mathf.Max(1, (int)(amount * 0.25f)); // Reflect 25% of damage, minimum 1
            Debug.Log($"{Name}'s Counter Attack Defense reflects {reflectedDamage} damage back to {attacker.Name}!");

            // Apply reflected damage to attacker
            attacker.CurrentHP = Mathf.Max(0, attacker.CurrentHP - reflectedDamage);

            // Show floating damage number for reflected damage
            if (BattleScene.Instance != null)
            {
                BattleScene.Instance.ShowFloatingDamage(reflectedDamage, attacker);
            }
        }
    }

    /// <summary>
    /// Processes end-of-turn effects, including buff duration management and HP debuffs.
    /// </summary>
    public virtual void OnEndTurn()
    {
        // Process HP debuffs (poison, bleed) before duration management
        ProcessHPBuffs();

        // Process healing defense HP regeneration (10% of max HP)
        ProcessHealingDefenseRegeneration();

        // TODO: Fix potential collection modification during iteration
        // Consider using RemoveAll() or a separate collection for items to remove
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            Buff buff = Buffs[i];
            buff.duration--;
            if (buff.duration < 0)
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

    public bool HasBuff(string buffName)
    {
        return Buffs.Any(b => b.buffType.ToString() == buffName);
    }

    /// <summary>
    /// Checks if the entity is stunned and cannot perform actions this turn.
    /// </summary>
    /// <returns>True if the entity is stunned, false otherwise</returns>
    public bool IsStunned()
    {
        return Buffs.Any(b => b.buffType == BuffType.Stun);
    }

    /// <summary>
    /// Processes HP debuff effects like poison and bleed.
    /// Reduces HP by percentage based on buff value, with minimum HP of 1.
    /// </summary>
    public void ProcessHPBuffs()
    {
        foreach (var buff in Buffs)
        {
            if (buff.buffType == BuffType.HP && buff.isDebuff)
            {
                // Calculate damage as percentage of max HP (value / 100)
                float damagePercentage = buff.value / 100f;
                int maxHP = Stats.HP;
                int damage = Mathf.RoundToInt(maxHP * damagePercentage);

                // Apply damage but ensure minimum HP of 1
                int newHP = Mathf.Max(1, CurrentHP - damage);
                int actualDamage = CurrentHP - newHP;

                if (actualDamage > 0)
                {
                    CurrentHP = newHP;
                    Debug.Log($"{Name} takes {actualDamage} damage from {buff.buffType}!");

                    // Show floating damage number
                    if (BattleScene.Instance != null)
                    {
                        BattleScene.Instance.ShowFloatingDamage(actualDamage, this);
                    }
                }
            }
            else if (buff.buffType == BuffType.HP && !buff.isDebuff)
            {
                // Calculate 10% of max HP
                int maxHP = Stats.HP;
                int regenerationAmount = Mathf.RoundToInt(maxHP * buff.value / 100f);

                // Apply regeneration
                int originalHP = CurrentHP;
                CurrentHP = Mathf.Min(CurrentHP + regenerationAmount, maxHP);
                int actualHeal = CurrentHP - originalHP;
                if (actualHeal > 0)
                {
                    Debug.Log($"{Name} regenerates {actualHeal} HP!");

                    // Show floating heal number
                    if (BattleScene.Instance != null)
                    {
                        BattleScene.Instance.ShowFloatingHeal(actualHeal, this);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Processes healing defense HP regeneration.
    /// Regenerates 10% of max HP at the end of each turn.
    /// </summary>
    public void ProcessHealingDefenseRegeneration()
    {
        if (HasBuff("Healing_Defense"))
        {
            // Calculate 10% of max HP
            int maxHP = Stats.HP;
            int regenerationAmount = Mathf.RoundToInt(maxHP * 0.1f);

            // Apply regeneration
            int originalHP = CurrentHP;
            CurrentHP = Mathf.Min(CurrentHP + regenerationAmount, maxHP);
            int actualHeal = CurrentHP - originalHP;

            if (actualHeal > 0)
            {
                Debug.Log($"{Name} regenerates {actualHeal} HP from Healing Defense!");

                // Show floating heal number
                if (BattleScene.Instance != null)
                {
                    BattleScene.Instance.ShowFloatingHeal(actualHeal, this);
                }
            }
        }
    }
}
