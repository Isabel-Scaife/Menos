using UnityEngine;

public class ObjectAnchor : MonoBehaviour
{
    [SerializeField] private QuestEvent @event;
    [SerializeField] private GameObject anchor;

    private void Start()
    {
        @event.AddReference(anchor);
    }
}
