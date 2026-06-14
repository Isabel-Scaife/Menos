using UnityEngine;

public class Envelope : MonoBehaviour, IRaycast
{
    [SerializeField]
    private GameObject openEnvelope;
    [SerializeField]
    private GameObject closedEnvelope;
    [SerializeField]
    private Collider2D letterCollider;

    public int State { get; set; }

    public void Interact()
    {
        if (State == 1)
        {
            openEnvelope.SetActive(true);
            closedEnvelope.SetActive(false);

            letterCollider.enabled = true;
        }
    }
}
