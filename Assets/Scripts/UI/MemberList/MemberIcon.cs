using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberIcon : MonoBehaviour
{
    [SerializeField] Image memberFace;
    public BattleCharacter Character { get; set; }
    MemberList memberList;
    int index;
    public void Initialize(MemberList memberList, int index){
        this.memberList = memberList;
        this.index = index;
    }

    public void Render(){
        if (Character != null)
        {
            memberFace.sprite = Character.Face;
            memberFace.gameObject.SetActive(true);
        }
        else
        {

            memberFace.sprite = null;
            memberFace.gameObject.SetActive(false);
        }
    }

    public void OnClick(){
        memberList.OnMemberSelected(index);
    }
}
