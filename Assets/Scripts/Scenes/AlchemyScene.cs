using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AlchemyScene : CommonListScene<AlchemyListBox>{

    [SerializeField] CraftSkillRow craftSkillRow;
    [SerializeField] CraftCompleteDialog craftCompleteDialog;
    void Start()
    {
        AddDisplayList(DBManager.Instance.GetAllCraftRecipes().Where(c => c.craftSkillType == CraftSkillType.Alchemy && c.requireSkillLevel <= Game.CraftSkillManager.Alchemy.Level).Cast<System.Object>().ToList());
        craftSkillRow.Render(Game.CraftSkillManager.Alchemy);
        craftCompleteDialog.gameObject.SetActive(false);
        Render();
    }

    public void UpdateCraftSkillRow(){
        craftSkillRow.Render(Game.CraftSkillManager.Alchemy);
    }

    public void DisplayCraftCompleteDialog(CraftRecipe recipe, int craftAmount){
        craftCompleteDialog.gameObject.SetActive(true);
        craftCompleteDialog.Render(recipe, craftAmount);
    }

}