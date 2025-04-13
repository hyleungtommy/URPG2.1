using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberList : MonoBehaviour
{
    [SerializeField] MemberIcon[] memberIcons;
    [SerializeField] MemberListScene memberListScene;

    void Start()
    {
        List<BattleCharacter> allMembers = GameController.Instance.party.GetAllMembers();
        // Initialize the member icons with the characters from the party
        for (int i = 0; i < memberIcons.Length; i++)
        {
            memberIcons[i].Initialize(this, i);
            if (i < allMembers.Count)
            {
                memberIcons[i].Character = allMembers[i];
                memberIcons[i].Render();
            }
            else
            {
                memberIcons[i].Character = null;
                memberIcons[i].Render();
            }
        }
        if (allMembers.Count > 0 && allMembers[0] != null)
        {
            memberListScene.OnMemberSelected(allMembers[0]);
        }
    }

    public void OnMemberSelected(int index)
    {
        // Check if the member list scene is assigned
        if (memberListScene == null)
        {
            Debug.LogError("MemberListScene is not assigned in MemberList.");
            return;
        }
        // Check if the index is valid
        if (index < 0 || index >= memberIcons.Length)
        {
            Debug.LogError("Invalid member index selected: " + index);
            return;
        }

        // Get the selected character
        BattleCharacter character = memberIcons[index].Character;
        if (character == null)
        {
            return;
        }

        memberListScene.OnMemberSelected(character);
    }
}
