using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScene : MonoBehaviour, MemberListScene
{
    [SerializeField] SkillPanel skillPanel;
    [SerializeField] StatusPanel statusPanel;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] MemberList memberList;

    private int selectedIndex = 0;

    void Start(){
        memberList.memberListScene = this;
        OnMemberSelected(Game.Party.GetAllMembers()[0]);
        //AddTestEquipment();
    }

    public void OnMemberSelected(BattleCharacter character)
    {
        if (character == null)
        {
            return;
        }

        statusPanel.Setup(character);
        skillPanel.Setup(character.CharacterClass.LearntSkillsList, character);
        equipmentPanel.Setup(character);

        CloseOtherPanels();
        if(selectedIndex == 0){
            statusPanel.Open();
        }else if(selectedIndex == 2){
            skillPanel.Show();
        }else if(selectedIndex == 3){
            equipmentPanel.Show();
        }
    }

    public void OnClickSkill(){
        selectedIndex = 2;
        CloseOtherPanels();
        memberList.memberListScene = skillPanel;
        skillPanel.Show();
    }

    public void OnClickStatus(){
        selectedIndex = 0;
        CloseOtherPanels();
        memberList.memberListScene = this;
        statusPanel.Open();
    }

    public void OnClickEquipment(){
        selectedIndex = 3;
        CloseOtherPanels();
        //memberList.memberListScene = equipmentPanel;
        equipmentPanel.Show();
    }

    private void CloseOtherPanels(){
        statusPanel.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
    }

    public void OnClickBack(){
        selectedIndex = 0;
        CloseOtherPanels();
        memberList.memberListScene = this;
        statusPanel.Open();
    }

    public void AddTestEquipment(){
        /*
        Weapon testSword = new Weapon(DBManager.Instance.GetWeapon(0));
        GameController.Instance.Inventory.InsertItem(testSword, 1);
        Weapon testShield = new Weapon(DBManager.Instance.GetWeapon(1));
        GameController.Instance.Inventory.InsertItem(testShield, 1);
        Weapon testAxe = new Weapon(DBManager.Instance.GetWeapon(2));
        GameController.Instance.Inventory.InsertItem(testAxe, 1);
        Weapon testWand = new Weapon(DBManager.Instance.GetWeapon(3));
        GameController.Instance.Inventory.InsertItem(testWand, 1);
        Weapon testArtifact = new Weapon(DBManager.Instance.GetWeapon(4));
        GameController.Instance.Inventory.InsertItem(testArtifact, 1);
        Weapon testStaff = new Weapon(DBManager.Instance.GetWeapon(5));
        GameController.Instance.Inventory.InsertItem(testStaff, 1);
        Weapon testBow = new Weapon(DBManager.Instance.GetWeapon(6));
        GameController.Instance.Inventory.InsertItem(testBow, 1);
        Weapon testDagger = new Weapon(DBManager.Instance.GetWeapon(7));
        GameController.Instance.Inventory.InsertItem(testDagger, 1);
        Armor testHeavyHead = new Armor(DBManager.Instance.GetArmor(0));
        GameController.Instance.Inventory.InsertItem(testHeavyHead, 1);
        Armor testLightHead = new Armor(DBManager.Instance.GetArmor(1));
        GameController.Instance.Inventory.InsertItem(testLightHead, 1);
        Armor testMediumHead = new Armor(DBManager.Instance.GetArmor(2));
        GameController.Instance.Inventory.InsertItem(testMediumHead, 1);
        Armor testHeavyBody = new Armor(DBManager.Instance.GetArmor(3));
        GameController.Instance.Inventory.InsertItem(testHeavyBody, 1);
        Armor testLightBody = new Armor(DBManager.Instance.GetArmor(4));
        GameController.Instance.Inventory.InsertItem(testLightBody, 1);
        Armor testMediumBody = new Armor(DBManager.Instance.GetArmor(5));
        GameController.Instance.Inventory.InsertItem(testMediumBody, 1);
        Armor testHeavyLeggings = new Armor(DBManager.Instance.GetArmor(6));
        GameController.Instance.Inventory.InsertItem(testHeavyLeggings, 1);
        Armor testLightLeggings = new Armor(DBManager.Instance.GetArmor(7));
        GameController.Instance.Inventory.InsertItem(testLightLeggings, 1);
        Armor testMediumLeggings = new Armor(DBManager.Instance.GetArmor(8));
        GameController.Instance.Inventory.InsertItem(testMediumLeggings, 1);
        Armor testHeavyGloves = new Armor(DBManager.Instance.GetArmor(9));
        GameController.Instance.Inventory.InsertItem(testHeavyGloves, 1);
        Armor testLightGloves = new Armor(DBManager.Instance.GetArmor(10));
        GameController.Instance.Inventory.InsertItem(testLightGloves, 1);
        Armor testMediumGloves = new Armor(DBManager.Instance.GetArmor(11));
        GameController.Instance.Inventory.InsertItem(testMediumGloves, 1);
        Armor testHeavyBoots = new Armor(DBManager.Instance.GetArmor(12));
        GameController.Instance.Inventory.InsertItem(testHeavyBoots, 1);
        Armor testLightBoots = new Armor(DBManager.Instance.GetArmor(13));
        GameController.Instance.Inventory.InsertItem(testLightBoots, 1);
        Armor testMediumBoots = new Armor(DBManager.Instance.GetArmor(14));
        GameController.Instance.Inventory.InsertItem(testMediumBoots, 1);
        */
    }
}
