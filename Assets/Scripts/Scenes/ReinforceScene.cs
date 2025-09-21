using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ReinforceScene : CommonListScene<BasicListBox>{
    [SerializeField] CraftSkillRow craftSkillRow;
    [SerializeField] CraftCompleteDialog craftCompleteDialog;

    void Start(){
        Render();
    }

    public override void Render()
    {
        ClearDisplayList();
        AddDisplayList(Game.Inventory.GetReinforceableEquipmentList().Select(e => e.Item).Cast<object>().ToList());
        infoPanel.Hide();
        base.Render();
        UpdateCraftSkillRow();
    }

    public override void OnBoxClicked(int slotId)
    {
        Equipment equipment = displayList[0][slotId] as Equipment;
        (infoPanel as ReinforceInfoPanel).ReinforceData = DBManager.Instance.GetReinforceData(ReinforceManager.GetReinforceEquipmentType(equipment), equipment.RequireLv);
        base.OnBoxClicked(slotId);
    }

    public void ShowCraftCompleteDialog(Equipment equipment)
    {
        craftCompleteDialog.gameObject.SetActive(true);
        craftCompleteDialog.Render(equipment);
    }



    public void UpdateCraftSkillRow()
    {
        craftSkillRow.Render(Game.CraftSkillManager.Reinforcing);
    }


}