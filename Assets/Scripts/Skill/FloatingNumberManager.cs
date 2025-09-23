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
    
}
