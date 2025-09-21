using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;

public class SmithingScene : CommonListScene<SmithingListBox>{

    [SerializeField] CraftSkillRow craftSkillRow;
    [SerializeField] CraftCompleteDialog craftCompleteDialog;
    void Start()
    {
        AddDisplayList(DBManager.Instance.GetAllCraftRecipes().Where(c => c.craftSkillType == CraftSkillType.Smithing && c.requireSkillLevel <= Game.CraftSkillManager.Smithing.Level).Cast<System.Object>().ToList());
        AddDisplayList(DBManager.Instance.GetAllCraftRecipes().Where(c => c.craftSkillType == CraftSkillType.ArcaneCrafting && c.requireSkillLevel <= Game.CraftSkillManager.ArcaneCrafting.Level).Cast<System.Object>().ToList());
        AddDisplayList(DBManager.Instance.GetAllCraftRecipes().Where(c => c.craftSkillType == CraftSkillType.ArcherCrafting && c.requireSkillLevel <= Game.CraftSkillManager.ArcherCrafting.Level).Cast<System.Object>().ToList());
        craftCompleteDialog.gameObject.SetActive(false);
        Render();
    }

    public override void Render()
    {
        base.Render();
        UpdateCraftSkillRow();
    }

    public void UpdateCraftSkillRow(){
        if(selectedTabId == 0){
            craftSkillRow.Render(Game.CraftSkillManager.Smithing);
        }else if(selectedTabId == 1){
            craftSkillRow.Render(Game.CraftSkillManager.ArcaneCrafting);
        }else if(selectedTabId == 2){
            craftSkillRow.Render(Game.CraftSkillManager.ArcherCrafting);
        }
    }

    public void DisplayCraftCompleteDialog(Equipment equipment){
        craftCompleteDialog.gameObject.SetActive(true);
        craftCompleteDialog.Render(equipment);
    }
}