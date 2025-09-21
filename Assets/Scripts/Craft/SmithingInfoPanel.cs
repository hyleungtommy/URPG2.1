using UnityEngine;
using UnityEngine.UI;

public class SmithingInfoPanel : InfoPanel{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemTypeText;
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Button craftButton;
    [SerializeField] Text errorText;
    [SerializeField] CraftRequirementRow[] CraftRequirementRows;
    [SerializeField] SmithingScene smithingScene;

    public override void Render()
    {
        CraftRecipe recipe = obj as CraftRecipe;
        if (recipe != null)
        {
            itemNameText.text = recipe.resultItem.Name;
            itemTypeText.text = recipe.resultItem.ItemType + "\nReq. Lv." + recipe.requireSkillLevel;
            itemBox.Render(recipe.resultItem);

            bool isIngredientEnough = true;
            for (int i = 0; i < CraftRequirementRows.Length; i++)
            {
                if (i < recipe.requirements.Length)
                {
                    CraftRequirementRows[i].gameObject.SetActive(true);
                    if(!CraftRequirementRows[i].Render(recipe.requirements[i], 1)){
                        isIngredientEnough = false;
                    }
                }
                else
                {
                    CraftRequirementRows[i].gameObject.SetActive(false);
                }
            }
            if (isIngredientEnough)
            {
                craftButton.gameObject.SetActive(true);
                errorText.gameObject.SetActive(false);
            }
            else
            {
                craftButton.gameObject.SetActive(false);
                errorText.gameObject.SetActive(true);
                errorText.text = "Not enough ingredients";
            }
        }

    }

    public void OnClickCraft()
    {
        CraftRecipe recipe = obj as CraftRecipe;
        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            Debug.Log("RemoveItem: " + recipe.requirements[i].item.Name + " " + recipe.requirements[i].amount);
            Game.Inventory.RemoveItem(recipe.requirements[i].item, recipe.requirements[i].amount);
        }

        Equipment resultEquipment = (recipe.resultItem as Equipment).CreateCopy();
        resultEquipment.Rarity = UnityEngine.Random.Range(0, 5);

        Game.Inventory.InsertItem(resultEquipment, recipe.resultAmount);

        if (recipe.craftSkillType == CraftSkillType.Smithing){
            Game.CraftSkillManager.Smithing.GainExp(1);
        }else if (recipe.craftSkillType == CraftSkillType.ArcaneCrafting){
            Game.CraftSkillManager.ArcaneCrafting.GainExp(1);
        }else if (recipe.craftSkillType == CraftSkillType.ArcherCrafting){
            Game.CraftSkillManager.ArcherCrafting.GainExp(1);
        }

        Render();
        smithingScene.UpdateCraftSkillRow();
        smithingScene.DisplayCraftCompleteDialog(resultEquipment);
    }
}