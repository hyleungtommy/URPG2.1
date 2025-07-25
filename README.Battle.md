# URPG 2.1 Battle System Documentation

## Overview

The URPG 2.1 battle system is a turn-based RPG combat system that supports both regular enemy encounters and boss battles. The system features a dynamic turn order based on agility, multiple action types (normal attacks, skills, items), and a comprehensive reward system.

## Battle System Architecture

### Core Components

1. **BattleManager** - Central controller managing the entire battle flow
2. **BattleScene** - Unity scene handling UI and battle loop
3. **BattleEntity** - Abstract base class for all combat participants
4. **BattlePlayerEntity** - Player character representation in battle
5. **BattleEnemyEntity** - Regular enemy representation
6. **BattleBossEntity** - Special boss enemy with unique mechanics

## Battle Initialization Flow

### 1. Battle Scene Loading
```csharp
BattleSceneLoader.LoadBattleScene(Map map)
```
- Sets up the current map and generates enemies
- Retrieves up to 4 unlocked party members
- Loads the Battle scene

### 2. Battle Preparation
```csharp
BattleScene.PrepareBattle()
```
- Creates a new BattleManager instance
- Initializes all battle entities (players and enemies)
- Sets up UI components and battle state
- Starts the battle loop

### 3. Entity Initialization
```csharp
BattleManager.InitializeBattle()
```

#### Player Entity Creation
- Creates `BattlePlayerEntity` for each party member
- Inherits stats from `BattleCharacter` (HP, MP, ATK, DEF, etc.)
- Includes skill list and battle portrait

#### Enemy Entity Creation
- **Regular Enemies**: Creates `BattleEnemyEntity` instances
- **Boss Battles**: Creates `BattleBossEntity` with special mechanics
- Enemies are generated based on map configuration and current zone

#### Turn Order Determination
- All entities are sorted by AGI (Agility) in descending order
- Higher AGI = earlier turn in the round
- Turn order is stored in both `turnOrder` list and `actionQueue`

## Battle Loop Mechanics

### Main Battle Loop
```csharp
BattleScene.BattleLoop() // Coroutine
```

The battle loop runs continuously until the battle ends:

1. **Peek Next Entity**: Check who's next in the action queue
2. **Check Entity Status**: Skip dead entities automatically
3. **Process Turn**:
   - **Enemy Turn**: Execute AI action immediately
   - **Player Turn**: Wait for player input

### Turn Processing

#### Enemy Turn Flow
```csharp
BattleEnemyEntity.TakeTurn()
```
- **Basic AI**: Randomly selects a living player target
- **Action**: Performs normal attack
- **Turn End**: Automatically ends turn and returns to queue

#### Player Turn Flow
```csharp
BattleScene.ShowPlayerOptions()
```
- Displays action buttons (Attack, Skill, Item, Escape)
- Waits for player selection
- Handles target selection based on action type

### Action Types

#### 1. Normal Attack
- **Target**: Single enemy
- **Damage**: `Max(1, ATK - target.DEF)`
- **MP Cost**: 0

#### 2. Skills
- **Types**: Attack, Healing, Buff, Debuff
- **Targeting**: Single target or AOE
- **MP Cost**: Varies by skill
- **Effects**: Damage, healing, stat modifications

#### 3. Items
- **Types**: Healing, buff, utility
- **Targeting**: Single target or AOE
- **Consumption**: Removed from inventory after use

#### 4. Escape
- **Success Rate**: 50% (configurable)
- **Effect**: Ends battle (if successful)

### Turn End Processing
```csharp
BattleManager.EndTurn(BattleEntity entity)
```

1. **Entity Cleanup**: Process buffs/debuffs (duration reduction)
2. **Queue Management**: Re-add entity to action queue
3. **UI Update**: Refresh all battle displays
4. **Battle Check**: Determine if battle should end

## Combat Mechanics

### Damage Calculation
```csharp
int damage = Mathf.Max(1, attacker.Stats.ATK - target.Stats.DEF);
```

### Stat System
- **HP**: Health Points (0 = defeated)
- **MP**: Mana Points (required for skills)
- **ATK**: Physical Attack power
- **DEF**: Physical Defense
- **MATK**: Magical Attack power
- **MDEF**: Magical Defense
- **AGI**: Agility (determines turn order)
- **DEX**: Dexterity (accuracy/critical)

### Buff/Debuff System
- **Duration**: Buffs expire after a certain number of turns
- **Stacking**: Multiple buffs of same type can stack
- **Replacement**: New buffs can replace existing ones
- **Calculation**: Stats are modified by buff multipliers

### Boss Mechanics
```csharp
BattleBossEntity
```
- **Shield System**: Boss has a shield that absorbs damage
- **Damage Multiplier**: Increases damage taken as shield depletes
- **Shield Regeneration**: 5% per turn when active
- **Shield Recovery**: Full shield restoration after 5-turn cooldown

## Battle Outcome Detection

### Victory Conditions
```csharp
bool allEnemiesDefeated = turnOrder.FindAll(e => e is BattleEnemyEntity && e.IsAlive).Count == 0;
```

### Defeat Conditions
```csharp
bool allPlayersDefeated = turnOrder.FindAll(e => e is BattlePlayerEntity && e.IsAlive).Count == 0;
```

## Reward System

### Reward Calculation
```csharp
BattleManager.CalculateRewards()
```
- **Money**: Sum of all defeated enemies' drop money
- **EXP**: Sum of all defeated enemies' drop EXP
- **Level Up Tracking**: Records which characters leveled up

### Reward Application
```csharp
BattleManager.ApplyRewards(BattleReward reward)
```
1. **Money**: Added to player's total money
2. **Experience**: Distributed to all party members
3. **Level Up**: Characters gain levels and stat points

## UI Components

### Battle Scene UI
- **Battle Top Bar**: Shows current turn and action prompts
- **Player List**: Displays party members with HP/MP bars
- **Enemy List**: Shows enemies with HP bars
- **Action Order**: Visual turn order indicator
- **Action Buttons**: Attack, Skill, Item, Escape options

### Reward Panel
- **Victory/Defeat Banner**: Visual outcome indicator
- **Reward Display**: Money and EXP gained
- **Character Panels**: Show level up notifications
- **Action Buttons**: Re-battle, Next Zone, Leave options

## Map Integration

### Zone Mode
- **Progressive Difficulty**: Each zone increases enemy power
- **Boss Battle**: Final zone features a boss enemy
- **Zone Progression**: Victory advances to next zone
- **Save Zones**: Every 5 zones act as checkpoints

### Explore Mode
- **Random Encounters**: Variable enemy compositions
- **Repeatable**: Can re-battle the same map
- **No Progression**: No zone advancement

## Technical Implementation

### Key Classes and Responsibilities

| Class | Responsibility |
|-------|---------------|
| `BattleManager` | Core battle logic, turn management, outcome detection |
| `BattleScene` | UI management, battle loop, player input handling |
| `BattleEntity` | Base combat entity with stats and actions |
| `BattlePlayerEntity` | Player-specific battle behavior |
| `BattleEnemyEntity` | Enemy AI and behavior |
| `BattleBossEntity` | Boss-specific mechanics and shield system |
| `BattleSceneLoader` | Scene transition and entity setup |
| `BattleReward` | Reward data structure |

### State Management
- **GameController.State.Battle**: Prevents other game interactions
- **SelectionMode**: Tracks current player action selection
- **isWaitingForPlayerInput**: Controls battle loop flow

### Performance Considerations
- **Coroutine-based Loop**: Non-blocking battle progression
- **UI Updates**: Only refresh when necessary
- **Entity Pooling**: Reuse entities for multiple battles
- **Memory Management**: Clean up references on battle end

## Future Enhancements

### Planned Features
- **Advanced AI**: More sophisticated enemy decision making
- **Status Effects**: Poison, paralysis, sleep, etc.
- **Combo System**: Linked attacks between party members
- **Environmental Effects**: Battlefield modifiers
- **Equipment Durability**: Items degrade with use
- **Multiplayer Support**: Cooperative battles

### Technical Improvements
- **Animation System**: Visual feedback for actions
- **Sound Effects**: Audio cues for combat events
- **Particle Effects**: Visual enhancements for skills
- **Save System**: Battle state persistence
- **Performance Optimization**: Reduced memory allocation

## Debugging and Testing

### Test Mode
- **Test Battle Scene**: Load specific map/party combinations
- **Skip Battle**: Instant victory for testing
- **Debug Logging**: Comprehensive battle event logging

### Common Issues
- **Turn Order**: Verify AGI calculations
- **Damage Calculation**: Check stat modifications
- **UI Updates**: Ensure proper refresh timing
- **Memory Leaks**: Monitor entity cleanup

This battle system provides a solid foundation for turn-based RPG combat with extensible architecture for future enhancements. 