using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnchantmentScene : CommonListScene<BasicListBox>
{
    [SerializeField] CraftSkillRow craftSkillRow;
    [SerializeField] CraftCompleteDialog craftCompleteDialog;

    void Start(){
        Render();
    }

    public override void Render()
    {
        ClearDisplayList();
        AddDisplayList(Game.Inventory.GetEnchantableEquipmentList().Select(e => e.Item).Cast<object>().ToList());
        infoPanel.Hide();
        base.Render();
        UpdateCraftSkillRow();
    }

    public void ShowCraftCompleteDialog(Equipment equipment)
    {
        craftCompleteDialog.gameObject.SetActive(true);
        craftCompleteDialog.Render(equipment);
    }

    public void UpdateCraftSkillRow()
    {
        craftSkillRow.Render(Game.CraftSkillManager.Enchanting);
    }
}
