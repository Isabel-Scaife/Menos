using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Move", menuName = "Events/Move")]
public class MoveObject : QuestEvent
{
    [SerializeField] private Vector2 newLocation;

    public override void PlayEvent()
    {
        if(targetObject != null)
        {
            targetObject.transform.position = newLocation;
        }
    }
}
