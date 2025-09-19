using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftRequirementRow : MonoBehaviour{
    [SerializeField] Image itemIcon;
    [SerializeField] Text itemName;
    [SerializeField] Text itemAmount;

    public bool Render(CraftRequirement requirement, int craftAmount){
        bool isIngredientEnough;
        itemIcon.sprite = requirement.item.Icon;
        itemName.text = requirement.item.Name;
        int availableAmount = Game.Inventory.GetTotalItemQuantity(requirement.item);
        itemAmount.text = $"{availableAmount}/{requirement.amount * craftAmount}";
        if(availableAmount >= requirement.amount * craftAmount){
            itemAmount.color = Color.green;
            isIngredientEnough = true;
        }else{
            itemAmount.color = Color.red;
            isIngredientEnough = false;
        }
        return isIngredientEnough;
    }
}