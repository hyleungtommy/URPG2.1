using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// <summary> Show character exp gain and show levelup image when level up </summary>
public class RewardPanelBattleCharacterPanel : MonoBehaviour
{
    [SerializeField] Bar expBar;
    [SerializeField] Image levelupImage;
    [SerializeField] Image characterIcon;
    [SerializeField] Text level;
    public void Render(BattleCharacter character, bool levelUp)
    {
        // Set character icon
        characterIcon.sprite = character.Face;
        // Set exp bar value
        expBar.Render(character.CurrentEXP, character.RequiredEXP);
        // Show level up image if level up
        levelupImage.gameObject.SetActive(levelUp);
        level.text = "Lv." + character.Lv;

    }
}
