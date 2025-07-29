using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using System.Linq;
public class ShopScene : CommonListScene<ShopBox>
{
    [SerializeField] Text TextMoney;
    public int BuyAmount {get; set;} = 1;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
        base.displayList = new List<UnityEngine.Object>(DBManager.Instance.GetAllItems().Where(item => item.Price > 0).Cast<UnityEngine.Object>());
        Render();
    }

    public void UpdateMoney()
    {
        TextMoney.text = Game.Money.ToString();
    }
    
}
