using UnityEngine;

public class MoveObject : MonoBehaviour, IEvent
{
    [SerializeField] Vector2 moveTo;
    [SerializeField] GameObject target;

    public void OnQuestComplete()
    {
        if(target != null)
        {
            target.transform.position = moveTo;
        }
    }
}
