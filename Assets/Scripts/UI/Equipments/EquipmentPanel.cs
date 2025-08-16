using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : CommonListScene<EquipmentListBox>
{
    [SerializeField] CurrentEquipmentInfoPanel currentEquipmentInfoPanel;
    [SerializeField] CurrentEquipmentGroup currentEquipmentGroup;

    public BattleCharacter character {get; private set;}
    public void Setup(BattleCharacter character)
    {
        this.character = character;
    }

    public override void Render()
    {
        ClearDisplayList();
        AddDisplayList(Game.Inventory.GetEquipmentList().Cast<System.Object>().ToList());
        currentEquipmentInfoPanel.Hide();
        currentEquipmentGroup.Render(character.CharacterClass.EquipmentManager);
        base.Render();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Render();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClickCurrentEquipment(int index)
    {
        if (index == 0)
        {
            if (character.CharacterClass.EquipmentManager.MainHand == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.MainHand);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 1)
        {
            if (character.CharacterClass.EquipmentManager.OffHand == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.OffHand);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 2)
        {
            if (character.CharacterClass.EquipmentManager.Head == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.Head);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 3)
        {
            if (character.CharacterClass.EquipmentManager.Body == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.Body);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 4)
        {
            if (character.CharacterClass.EquipmentManager.Hands == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.Hands);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 5)
        {
            if (character.CharacterClass.EquipmentManager.Legs == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.Legs);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 6)
        {
            if (character.CharacterClass.EquipmentManager.Feet == null)
            {
                return;
            }
            currentEquipmentInfoPanel.SetUp(character.CharacterClass.EquipmentManager.Feet);
            currentEquipmentInfoPanel.Render();
            currentEquipmentInfoPanel.Show();
        }
        else if (index == 7)
        {
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory1);
        }
        else if (index == 8)
        {
            //currentEquipmentInfoPanel.Render(character.Class.EquipmentManager.Accessory2);
        }
    }

    private void AddTestEquipment(){
        Equipment testWeapon1 = new Weapon(DBManager.Instance.GetWeapon(0));
        Game.Inventory.InsertItem(testWeapon1, 1);
        Equipment testWeapon2 = new Weapon(DBManager.Instance.GetWeapon(1));
        Game.Inventory.InsertItem(testWeapon2, 1);
        Equipment testArmor1 = new Armor(DBManager.Instance.GetArmor(0));
        Game.Inventory.InsertItem(testArmor1, 1);
        Equipment testArmor2 = new Armor(DBManager.Instance.GetArmor(1));
        Game.Inventory.InsertItem(testArmor2, 1);
    }
}
