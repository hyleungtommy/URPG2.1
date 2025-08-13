using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using System.Linq;
public class ShopScene : CommonListScene<ShopListBox>
{
    [SerializeField] Text TextMoney;
    public int BuyAmount {get; set;} = 1;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
        AddDisplayList(DBManager.Instance.GetAllItems().Where(item => item.Price > 0).Cast<System.Object>().ToList());
        Render();
    }

    public void UpdateMoney()
    {
        TextMoney.text = Game.Money.ToString();
    }
    
}
