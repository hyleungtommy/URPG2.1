using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantmentInfoPanel : InfoPanel
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text itemName;
    [SerializeField] Text itemType;
    [SerializeField] CraftRequirementRow[] requirementRows;
    [SerializeField] Text errorText;
    [SerializeField] Button enchantButton;
    [SerializeField] EnchantmentScene enchantmentScene;
    private EnchantmentData enchantmentData;

    public override void Render(){
        Equipment equipment = obj as Equipment;
        enchantmentData = DBManager.Instance.GetEnchantmentData(equipment.RequireLv);
        if(equipment == null) return;
        itemBox.Render(equipment);
        itemName.text = equipment.Name;
        FormatItemType(equipment);
        bool isIngredientEnough = true;
        for(int i = 0; i < requirementRows.Length; i++){
            requirementRows[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < enchantmentData.Requirements.Length; i++)
        {
            if (i < enchantmentData.Requirements.Length)
            {
                requirementRows[i].gameObject.SetActive(true);
                if (!requirementRows[i].Render(enchantmentData.Requirements[i], 1))
                {
                    isIngredientEnough = false;
                }
            }
            else
            {
                requirementRows[i].gameObject.SetActive(false);
            }
        }
        if (isIngredientEnough)
        {
            enchantButton.gameObject.SetActive(true);
            errorText.gameObject.SetActive(false);
        }
        else
        {
            enchantButton.gameObject.SetActive(false);
            errorText.gameObject.SetActive(true);
            errorText.text = "Not enough ingredients";
        }
    }

    public void RenderRequirementRows()
    {
        for (int i = 0; i < requirementRows.Length; i++)
        {
            if (i < enchantmentData.Requirements.Length)
            {
                requirementRows[i].Render(enchantmentData.Requirements[i], 1);
            }
        }
    }

    public void FormatItemType(Equipment equipment){
        itemType.text = equipment.ItemType.ToString() + "\nReq. Lv." + equipment.RequireLv.ToString();
    }

    public void OnClickEnchant(){
        Equipment equipment = obj as Equipment;
        equipment.Enchant();
        for(int i = 0; i < enchantmentData.Requirements.Length; i++){
            Game.Inventory.RemoveItem(enchantmentData.Requirements[i].item, enchantmentData.Requirements[i].amount);
        }
        enchantmentScene.ShowCraftCompleteDialog(equipment);
        enchantmentScene.Render();
    }
}
