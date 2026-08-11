using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "Move", menuName = "Events/Move")]
public class MoveObject : QuestEvent
{
    [SerializeField] private Vector2 newLocation;

    public override void PlayEvent()
    {
        if(gameObject != null)
        {
            gameObject.transform.position = newLocation;
        }
    }
}
