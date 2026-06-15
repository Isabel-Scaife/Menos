using Unity.VisualScripting;
using UnityEngine;

public class Seal : MonoBehaviour, IRaycast
{
    [SerializeField] Envelope envelope;
    
    Rigidbody2D rg;

    private void Awake()
    {
        if((rg = GetComponent<Rigidbody2D>()) == null)
        {
            Debug.Log(name + "Missing Rigidbody");
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}

    public void Interact()
    {
        rg.WakeUp();
        envelope.State++;
    }
}
