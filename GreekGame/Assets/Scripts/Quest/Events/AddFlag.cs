using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "AddFlag", menuName = "Events/AddFlag")]
public class AddFlag : QuestEvent
{
    [SerializeField] private string flag;
    public override void PlayEvent()
    {
        if (flag != null)
        {
            if(GameStateManager.Instance == null) { Debug.Log("No game state manager"); return; }
            GameStateManager.Instance.SetFlag(flag);
        }
    }
}
