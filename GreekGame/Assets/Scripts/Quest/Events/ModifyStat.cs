using UnityEngine;

public class ModifyStat : MonoBehaviour, IEvent
{
    [SerializeField] private int[] statChanges; 

    public void OnQuestComplete()
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
