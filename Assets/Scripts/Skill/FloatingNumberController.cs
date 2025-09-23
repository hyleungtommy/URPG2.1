using System.Collections;
using UnityEngine;

public class FloatingNumberController : MonoBehaviour
{
    [Header("Digit Sprites")]
    public Sprite[] damageNumbers; // assign digits 0–9 in Inspector

    private bool isAnimating = false;


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
    
    public void SetupDamageNumber(int damage, bool isCritical = false)
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
