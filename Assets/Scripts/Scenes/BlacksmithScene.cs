using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithScene : CommonListScene<BlacksmithListBox>
{
    [SerializeField] Text moneyText;

    void Start(){
        AddDisplayList(DBManager.Instance.GetAllWeapons().ToList().Cast<System.Object>().ToList());
        AddDisplayList(DBManager.Instance.GetAllArmors().ToList().Cast<System.Object>().ToList());
        Render();
        UpdateMoneyText();
    }

    public void UpdateMoneyText(){
        moneyText.text = Game.Money.ToString();
    }
    
    

    

}
