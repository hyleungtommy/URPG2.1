using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftCompleteDialog : MonoBehaviour{
    [SerializeField] Text itemNameText;
    [SerializeField] BasicItemBox itemBox;

    public void Render(CraftRecipe recipe, int craftAmount){
        itemNameText.text = recipe.resultItem.Name + " x " + (craftAmount * recipe.resultAmount);
        itemBox.Render(recipe.resultItem);
    }

    public void Render(Equipment equipment){
        itemNameText.text = equipment.FullName;
        itemBox.Render(equipment);
    }

    public void OnClickClose(){
        gameObject.SetActive(false);
    }
}