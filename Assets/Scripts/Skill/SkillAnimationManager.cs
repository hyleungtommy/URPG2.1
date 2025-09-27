using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillAnimationManager : MonoBehaviour
{
    public static SkillAnimationManager Instance;
    public GameObject normalAttackAnimation;
    public GameObject healAnimation;
    public GameObject buffAnimation;
    public GameObject debuffAnimation;
    public GameObject enemyAttackAnimation;
    public Canvas canvas;

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
    
    public void PlayNormalAttackAnimation(Vector3 position)
    {
        if (normalAttackAnimation != null)
        {
            GameObject animation = Instantiate(normalAttackAnimation);
            SetAnimationPosition(animation, position);
            // Animation will auto-destroy via SkillAnimationController
        }
    }
    
    public void PlayEnemyAttackAnimation(Vector3 position)
    {
        if (enemyAttackAnimation != null)
        {
            GameObject animation = Instantiate(enemyAttackAnimation);
            SetAnimationPosition(animation, position);
            // Animation will auto-destroy via SkillAnimationController
        }
    }
    
    public void PlaySkillAnimation(Skill skill, Vector3 position)
    {
        if (skill.Animation != null)
        {
            GameObject animation = Instantiate(skill.Animation);
            SetAnimationPosition(animation, position);
            // Animation will auto-destroy via SkillAnimationController
        }
        else
        {
            // Fallback to skill type animations
            PlaySkillAnimationByType(skill.Type, position);
        }
    }
    
    private void PlaySkillAnimationByType(SkillType skillType, Vector3 position)
    {
        GameObject animationPrefab = null;
        
        switch (skillType)
        {
            case SkillType.Heal:
            case SkillType.HealAOE:
                animationPrefab = healAnimation;
                break;
            case SkillType.Buff:
            case SkillType.BuffSelf:
            case SkillType.BuffAOE:
                animationPrefab = buffAnimation;
                break;
            case SkillType.Debuff:
            case SkillType.DebuffAOE:
                animationPrefab = debuffAnimation;
                break;
            case SkillType.Attack:
            case SkillType.AttackAOE:
                animationPrefab = normalAttackAnimation; // Fallback to normal attack for attack skills
                break;
        }
        
        if (animationPrefab != null)
        {
            Debug.Log("Playing skill animation: " + animationPrefab.name);
            GameObject animation = Instantiate(animationPrefab);
            SetAnimationPosition(animation, position);
            // Animation will auto-destroy via SkillAnimationController
        }
    }
    
    private void SetAnimationPosition(GameObject animation, Vector3 position)
    {
        if (animation == null) return;
        
        
        // Set world position directly
        animation.transform.position = position;
        
        // If the animation has a RectTransform, remove it since we're using world space
        RectTransform rectTransform = animation.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Convert to regular transform for world space positioning
            Transform transform = animation.transform;
            Vector3 pos = transform.position;
            Vector3 scale = transform.localScale;
            Vector3 rot = transform.eulerAngles;
            
            DestroyImmediate(rectTransform);
            
            // Restore position and scale
            transform.position = pos;
            transform.localScale = scale;
            transform.eulerAngles = rot;
        }
    }
}
