using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumberManager : MonoBehaviour
{
    public static FloatingNumberManager Instance;
    public GameObject floatingNumberPrefab;
    public Canvas canvas;
    
    [Header("Animation Settings")]
    public float floatDuration = 1.5f;
    public float floatHeight = 0.1f; // Reduced height to keep numbers visible
    public float digitSpacing = 0.3f; // Space between digits
    public float randomOffsetRange = 0.2f; // Small random offset
    public float elementIconSpacing = 0.5f; // Space between element icon and damage numbers
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowDamageNumber(int damage, Vector3 worldPosition, bool isCritical = false)
    {
        if (floatingNumberPrefab == null)
        {
            Debug.LogWarning("FloatingNumberManager: Missing prefab reference");
            return;
        }

        // Convert damage to string to get individual digits
        string damageString = damage.ToString();
        
        // Calculate total width for multi-digit numbers
        float totalWidth = (damageString.Length - 1) * digitSpacing;
        float startX = -totalWidth / 2f;
        
        // Add random offset only once for the entire number
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0
        );
        
        // Create a prefab instance for each digit
        for (int i = 0; i < damageString.Length; i++)
        {
            int digit = int.Parse(damageString[i].ToString());
            
            // Calculate position for this digit (relative to the first digit with random offset)
            Vector3 digitPosition = worldPosition + new Vector3(startX + (i * digitSpacing), 0, 0) + randomOffset;
            
            // Create the damage number display
            GameObject damageNumberObj = Instantiate(floatingNumberPrefab);
            damageNumberObj.transform.position = digitPosition;
            
            // Get the controller and set up the single digit
            FloatingNumberController controller = damageNumberObj.GetComponent<FloatingNumberController>();
            if (controller != null)
            {
                controller.SetDigit(digit);
                controller.StartFloatingAnimation(floatDuration, floatHeight);
            }
        }
    }
    
    public void ShowDamageNumberOnEntity(int damage, BattleEntity entity, bool isCritical = false)
    {
        if (entity == null) return;
        
        // Get the entity's UI position
        Vector3 entityPosition = BattleScene.Instance.GetEntityPosition(entity);
        ShowDamageNumber(damage, entityPosition, isCritical);
    }
    
    /// <summary>
    /// Shows magical damage with element icon for magic skills
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    /// <param name="worldPosition">World position to show the number</param>
    /// <param name="element">The element type of the magic</param>
    /// <param name="isCritical">Whether this is a critical hit</param>
    public void ShowMagicDamageNumber(int damage, Vector3 worldPosition, ElementType element, bool isCritical = false)
    {
        if (floatingNumberPrefab == null)
        {
            Debug.LogWarning("FloatingNumberManager: Missing prefab reference");
            return;
        }

        // Convert damage to string to get individual digits
        string damageString = damage.ToString();
        
        // Calculate total width for multi-digit numbers
        float totalWidth = (damageString.Length - 1) * digitSpacing;
        
        // If we have an element icon, add space for it
        float elementIconWidth = 0f;
        if (element != ElementType.None)
        {
            elementIconWidth = elementIconSpacing;
        }
        
        float startX = -(totalWidth + elementIconWidth) / 2f;
        
        // Add random offset only once for the entire number
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0
        );
        
        // Create element icon first if element is not None
        if (element != ElementType.None)
        {
            Vector3 iconPosition = worldPosition + new Vector3(startX, 0, 0) + randomOffset;
            
            GameObject iconObj = Instantiate(floatingNumberPrefab);
            iconObj.transform.position = iconPosition;
            
            FloatingNumberController iconController = iconObj.GetComponent<FloatingNumberController>();
            if (iconController != null)
            {
                Sprite elementSprite = iconController.GetElementIcon(element);
                if (elementSprite != null)
                {
                    SpriteRenderer spriteRenderer = iconObj.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.sprite = elementSprite;
                    }
                }
                iconController.StartFloatingAnimation(floatDuration, floatHeight);
            }
        }
        
        // Create a prefab instance for each digit
        for (int i = 0; i < damageString.Length; i++)
        {
            int digit = int.Parse(damageString[i].ToString());
            
            // Calculate position for this digit (accounting for element icon)
            Vector3 digitPosition = worldPosition + new Vector3(startX + elementIconWidth + (i * digitSpacing), 0, 0) + randomOffset;
            
            // Create the damage number display
            GameObject damageNumberObj = Instantiate(floatingNumberPrefab);
            damageNumberObj.transform.position = digitPosition;
            
            // Get the controller and set up the single digit
            FloatingNumberController controller = damageNumberObj.GetComponent<FloatingNumberController>();
            if (controller != null)
            {
                controller.SetDigit(digit);
                controller.StartFloatingAnimation(floatDuration, floatHeight);
            }
        }
    }
    
    /// <summary>
    /// Shows magical damage with element icon for magic skills on an entity
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    /// <param name="entity">The entity that took damage</param>
    /// <param name="element">The element type of the magic</param>
    /// <param name="isCritical">Whether this is a critical hit</param>
    public void ShowMagicDamageNumberOnEntity(int damage, BattleEntity entity, ElementType element, bool isCritical = false)
    {
        if (entity == null) return;
        
        // Get the entity's UI position
        Vector3 entityPosition = BattleScene.Instance.GetEntityPosition(entity);
        ShowMagicDamageNumber(damage, entityPosition, element, isCritical);
    }
    
    public void ShowHealNumber(int heal, Vector3 worldPosition)
    {
        if (floatingNumberPrefab == null)
        {
            Debug.LogWarning("FloatingNumberManager: Missing prefab reference");
            return;
        }

        // Convert heal to string to get individual digits
        string healString = heal.ToString();
        
        // Calculate total width for multi-digit numbers
        float totalWidth = (healString.Length - 1) * digitSpacing;
        float startX = -totalWidth / 2f;
        
        // Add random offset only once for the entire number
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0
        );
        
        // Create a prefab instance for each digit
        for (int i = 0; i < healString.Length; i++)
        {
            int digit = int.Parse(healString[i].ToString());
            
            // Calculate position for this digit (relative to the first digit with random offset)
            Vector3 digitPosition = worldPosition + new Vector3(startX + (i * digitSpacing), 0, 0) + randomOffset;
            
            // Create the heal number display
            GameObject healNumberObj = Instantiate(floatingNumberPrefab);
            healNumberObj.transform.position = digitPosition;
            
            // Get the controller and set up the single digit
            FloatingNumberController controller = healNumberObj.GetComponent<FloatingNumberController>();
            if (controller != null)
            {
                controller.SetupHealNumber(digit);
                controller.StartFloatingAnimation(floatDuration, floatHeight);
            }
        }
    }
    
    public void ShowHealNumberOnEntity(int heal, BattleEntity entity)
    {
        if (entity == null) return;
        
        // Get the entity's UI position
        Vector3 entityPosition = BattleScene.Instance.GetEntityPosition(entity);
        ShowHealNumber(heal, entityPosition);
    }
    
    public void ShowManaRegenNumber(int manaRegen, Vector3 worldPosition)
    {
        if (floatingNumberPrefab == null)
        {
            Debug.LogWarning("FloatingNumberManager: Missing prefab reference");
            return;
        }

        // Convert mana regen to string to get individual digits
        string manaRegenString = manaRegen.ToString();
        
        // Calculate total width for multi-digit numbers
        float totalWidth = (manaRegenString.Length - 1) * digitSpacing;
        float startX = -totalWidth / 2f;
        
        // Add random offset only once for the entire number
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            0
        );
        
        // Create a prefab instance for each digit
        for (int i = 0; i < manaRegenString.Length; i++)
        {
            int digit = int.Parse(manaRegenString[i].ToString());
            
            // Calculate position for this digit (relative to the first digit with random offset)
            Vector3 digitPosition = worldPosition + new Vector3(startX + (i * digitSpacing), 0, 0) + randomOffset;
            
            // Create the mana regen number display
            GameObject manaRegenNumberObj = Instantiate(floatingNumberPrefab);
            manaRegenNumberObj.transform.position = digitPosition;
            
            // Get the controller and set up the single digit
            FloatingNumberController controller = manaRegenNumberObj.GetComponent<FloatingNumberController>();
            if (controller != null)
            {
                controller.SetupManaRegenNumber(digit);
                controller.StartFloatingAnimation(floatDuration, floatHeight);
            }
        }
    }
    
    public void ShowManaRegenNumberOnEntity(int manaRegen, BattleEntity entity)
    {
        if (entity == null) return;
        
        // Get the entity's UI position
        Vector3 entityPosition = BattleScene.Instance.GetEntityPosition(entity);
        ShowManaRegenNumber(manaRegen, entityPosition);
    }
    
}
