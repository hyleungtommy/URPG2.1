using System.Collections;
using UnityEngine;

public class FloatingNumberController : MonoBehaviour
{
    [Header("Digit Sprites")]
    public Sprite[] damageNumbers; // assign digits 0–9 in Inspector
    public Sprite[] healNumbers; // assign digits 0–9 in Inspector
    public Sprite[] manaRegenNumbers; // assign digits 0–9 in Inspector
    public Sprite fireIcon;
    public Sprite iceIcon;
    public Sprite lightningIcon;
    public Sprite earthIcon;
    public Sprite windIcon;
    public Sprite lightIcon;
    public Sprite darkIcon;

    private bool isAnimating = false;
    
    [Header("Element Icon Settings")]
    public float elementIconSpacing = 0.5f; // Space between element icon and numbers

    /// <summary>
    /// Gets the sprite for the specified element type
    /// </summary>
    /// <param name="element">The element type</param>
    /// <returns>The corresponding sprite, or null if element is None or not found</returns>
    public Sprite GetElementIcon(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                return fireIcon;
            case ElementType.Ice:
                return iceIcon;
            case ElementType.Lightning:
                return lightningIcon;
            case ElementType.Earth:
                return earthIcon;
            case ElementType.Wind:
                return windIcon;
            case ElementType.Light:
                return lightIcon;
            case ElementType.Dark:
                return darkIcon;
            default:
                return null;
        }
    }

    public void SetDigit(int digit)
    {
        if (digit >= 0 && digit < damageNumbers.Length)
        {
            // Use SpriteRenderer for world space display
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = damageNumbers[digit];
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on FloatingNumberController! Make sure the prefab has a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError($"Digit {digit} is out of range! Array length: {damageNumbers.Length}");
        }
    }
    
    public void SetupDamageNumber(int damage, bool isCritical = false, ElementType element = ElementType.None)
    {
        // Check if damageNumbers array is properly set
        if (damageNumbers == null || damageNumbers.Length == 0)
        {
            Debug.LogError("damageNumbers array is null or empty! Please assign digit sprites in the Inspector.");
            return;
        }
        
        // Convert damage to string to get individual digits
        string damageString = damage.ToString();

        // For single digit numbers, just set the sprite directly
        if (damageString.Length == 1)
        {
            int digit = int.Parse(damageString);
            SetDigit(digit);
        }
        else
        {
            Debug.LogWarning($"Multi-digit damage ({damage}) detected. This prefab only supports single digits. Consider creating multiple prefab instances.");
            // For now, just show the first digit
            int firstDigit = int.Parse(damageString[0].ToString());
            SetDigit(firstDigit);
        }
    }
    
    public void SetupHealNumber(int heal)
    {
        // Check if healNumbers array is properly set
        if (healNumbers == null || healNumbers.Length == 0)
        {
            Debug.LogError("healNumbers array is null or empty! Please assign heal digit sprites in the Inspector.");
            return;
        }
        
        // Convert heal to string to get individual digits
        string healString = heal.ToString();

        // For single digit numbers, just set the sprite directly
        if (healString.Length == 1)
        {
            int digit = int.Parse(healString);
            SetHealDigit(digit);
        }
        else
        {
            Debug.LogWarning($"Multi-digit heal ({heal}) detected. This prefab only supports single digits. Consider creating multiple prefab instances.");
            // For now, just show the first digit
            int firstDigit = int.Parse(healString[0].ToString());
            SetHealDigit(firstDigit);
        }
    }
    
    public void SetHealDigit(int digit)
    {
        if (digit >= 0 && digit < healNumbers.Length)
        {
            // Use SpriteRenderer for world space display
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = healNumbers[digit];
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on FloatingNumberController! Make sure the prefab has a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError($"Heal digit {digit} is out of range! Array length: {healNumbers.Length}");
        }
    }
    
    public void SetupManaRegenNumber(int manaRegen)
    {
        // Check if manaRegenNumbers array is properly set
        if (manaRegenNumbers == null || manaRegenNumbers.Length == 0)
        {
            Debug.LogError("manaRegenNumbers array is null or empty! Please assign mana regen digit sprites in the Inspector.");
            return;
        }
        
        // Convert mana regen to string to get individual digits
        string manaRegenString = manaRegen.ToString();

        // For single digit numbers, just set the sprite directly
        if (manaRegenString.Length == 1)
        {
            int digit = int.Parse(manaRegenString);
            SetManaRegenDigit(digit);
        }
        else
        {
            Debug.LogWarning($"Multi-digit mana regen ({manaRegen}) detected. This prefab only supports single digits. Consider creating multiple prefab instances.");
            // For now, just show the first digit
            int firstDigit = int.Parse(manaRegenString[0].ToString());
            SetManaRegenDigit(firstDigit);
        }
    }
    
    public void SetManaRegenDigit(int digit)
    {
        if (digit >= 0 && digit < manaRegenNumbers.Length)
        {
            // Use SpriteRenderer for world space display
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = manaRegenNumbers[digit];
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on FloatingNumberController! Make sure the prefab has a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError($"Mana regen digit {digit} is out of range! Array length: {manaRegenNumbers.Length}");
        }
    }
    
    
    public void StartFloatingAnimation(float duration, float height)
    {
        if (isAnimating) return;
        
        StartCoroutine(FloatingAnimation(duration, height));
    }
    
    private IEnumerator FloatingAnimation(float duration, float height)
    {
        isAnimating = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * height;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            
            // Use ease-out curve for smooth animation
            float easedProgress = 1f - Mathf.Pow(1f - progress, 3f);
            
            // Move upward using world position
            transform.position = Vector3.Lerp(startPos, endPos, easedProgress);
            
            // Fade out over time
            float alpha = Mathf.Lerp(1f, 0f, progress);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
            
            yield return null;
        }
        
        // Destroy the object after animation
        Destroy(gameObject);
    }
    
}
