using UnityEngine;

[System.Serializable]
public class MoveObject : QuestEvent
{
    [SerializeField] private Vector2 newLocation;
    
    // add scriptable to object that needs to be moved 
    public GameObject target;

    public override void PlayEvent()
    {
        if(target != null)
        {
            target.transform.position = newLocation;
        }
    }
}
