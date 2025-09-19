using UnityEngine;
using UnityEngine.UI;
public class AlchemyInfoPanel : InfoPanel
{
    [SerializeField] Text itemNameText;
    [SerializeField] Text itemTypeText;
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text craftAmountText;
    [SerializeField] Button craftButton;
    [SerializeField] Text errorText;
    [SerializeField] CraftRequirementRow[] CraftRequirementRows;
    [SerializeField] AlchemyScene alchemyScene;

    private int craftAmount = 1;

    public override void Render()
    {
        CraftRecipe recipe = obj as CraftRecipe;
        if (recipe != null)
        {
            itemNameText.text = recipe.resultItem.Name;
            itemTypeText.text = recipe.resultItem.ItemType + "\nReq. Alchemy Lv." + recipe.requireSkillLevel;
            itemBox.Render(recipe.resultItem);
            craftAmountText.text = craftAmount.ToString();

            bool isIngredientEnough = true;
            for (int i = 0; i < CraftRequirementRows.Length; i++)
            {
                if (i < recipe.requirements.Length)
                {
                    CraftRequirementRows[i].gameObject.SetActive(true);
                    isIngredientEnough = CraftRequirementRows[i].Render(recipe.requirements[i], craftAmount);
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

    public void OnClickAdd1()
    {
        if (craftAmount < 99)
        {
            craftAmount++;
            Render();
        }
    }

    public void OnClickMinus1()
    {
        if (craftAmount > 1)
        {
            craftAmount--;
            Render();
        }
    }

    public void OnClickAdd10()
    {

        if (craftAmount < 90)
        {
            craftAmount += 10;
        }
        else
        {
            craftAmount = 99;
        }
        Render();
    }

    public void OnClickMinus10()
    {
        if (craftAmount > 10)
        {
            craftAmount -= 10;
        }
        else
        {
            craftAmount = 1;
        }
        Render();
    }

    public void OnClickCraft()
    {
        CraftRecipe recipe = obj as CraftRecipe;
        for (int i = 0; i < recipe.requirements.Length; i++)
        {
            Game.Inventory.RemoveItem(recipe.requirements[i].item, recipe.requirements[i].amount * craftAmount);
        }
        Game.Inventory.InsertItem(recipe.resultItem, craftAmount * recipe.resultAmount);
        Game.CraftSkillManager.Alchemy.GainExp(craftAmount);
        Render();
        alchemyScene.UpdateCraftSkillRow();
        alchemyScene.DisplayCraftCompleteDialog(recipe, craftAmount);
    }
}