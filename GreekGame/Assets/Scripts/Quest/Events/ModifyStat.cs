using UnityEngine;

[System.Serializable]
public class ModifyStat : QuestEvent
{
    [SerializeField] private int[] statChanges; 

    public override void PlayEvent()
    {
        if(GameStateManager.Instance == null)
        {
            Debug.Log("Missing GameStateManager");
        }
        else
        {
            GameStateManager.Instance.ChangeStats(statChanges);
        }
    }
}
