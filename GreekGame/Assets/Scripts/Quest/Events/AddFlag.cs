using UnityEngine;

[System.Serializable]
public class AddFlag : QuestEvent
{
    [SerializeField] private string flagToAdd;
    public override void PlayEvent()
    {
        if (flagToAdd != null)
        {
            if(GameStateManager.Instance == null) { Debug.Log("No game state manager"); return; }
            GameStateManager.Instance.SetFlag(flagToAdd);
        }
    }
}
