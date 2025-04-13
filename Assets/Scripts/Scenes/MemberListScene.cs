using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MemberListScene : MonoBehaviour
{
    public abstract void OnMemberSelected(BattleCharacter character);
}
