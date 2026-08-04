using UnityEngine;

public class AddFlag : MonoBehaviour, IEvent
{
    [SerializeField] private string flagToAdd;
    public void OnQuestComplete()
    {
        if (flagToAdd != null)
        {
            if(GameStateManager.Instance == null) { Debug.Log("No game state manager"); return; }
            GameStateManager.Instance.SetFlag(flagToAdd);
        }
    }
}
