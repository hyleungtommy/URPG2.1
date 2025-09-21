using UnityEngine;
using UnityEngine.UI;
public class ReinforceInfoPanel : InfoPanel
{
    [SerializeField] BasicItemBox itemBox;
    [SerializeField] Text itemName;
    [SerializeField] Text itemType;
    [SerializeField] Text reinforceLevel;
    [SerializeField] EquipmentPowerText beforeReinforce;
    [SerializeField] EquipmentPowerText afterReinforce;
    [SerializeField] CraftRequirementRow[] requirementRows;
    [SerializeField] Text errorText;
    [SerializeField] Button reinforceButton;
    [SerializeField] ReinforceScene reinforceScene;
    public ReinforceData ReinforceData { get; set; }
    public override void Render()
    {
        Equipment equipment = obj as Equipment;
        if (equipment == null) return;
        itemBox.Render(equipment);
        itemName.text = equipment.Name;
        FormatItemType(equipment);
        reinforceLevel.text = "Reinforce Lv." + equipment.ReinforceLv.ToString() + ">" + (equipment.ReinforceLv + 1).ToString();
        beforeReinforce.Render(equipment);
        afterReinforce.RenderReinforceText(equipment);
        
        RenderRequirementRows();
        bool isIngredientEnough = true;
        for(int i = 0; i < requirementRows.Length; i++){
            requirementRows[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < ReinforceData.requirements.Length; i++)
        {
            if (i < ReinforceData.requirements.Length)
            {
                requirementRows[i].gameObject.SetActive(true);
                if (!requirementRows[i].Render(ReinforceData.requirements[i], 1))
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
            reinforceButton.gameObject.SetActive(true);
            errorText.gameObject.SetActive(false);
        }
        else
        {
            reinforceButton.gameObject.SetActive(false);
            errorText.gameObject.SetActive(true);
            errorText.text = "Not enough ingredients";
        }
    }

    public void RenderRequirementRows()
    {
        for (int i = 0; i < requirementRows.Length; i++)
        {
            if (i < ReinforceData.requirements.Length)
            {
                requirementRows[i].Render(ReinforceData.requirements[i], 1);
            }
        }
    }

    public void FormatItemType(Equipment equipment){
        itemType.text = equipment.ItemType.ToString() + "\nReq. Lv." + equipment.RequireLv.ToString();
    }

    public void OnClickReinforce(){
        Equipment equipment = obj as Equipment;
        if (equipment == null) return;
        equipment.Reinforce();
        for(int i = 0; i < ReinforceData.requirements.Length; i++){
            Game.Inventory.RemoveItem(ReinforceData.requirements[i].item, ReinforceData.requirements[i].amount);
        }
        reinforceScene.ShowCraftCompleteDialog(equipment);
        reinforceScene.Render();
    }
    
}