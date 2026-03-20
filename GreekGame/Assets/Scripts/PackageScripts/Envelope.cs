using UnityEngine;
using UnityEngine.InputSystem;


public class Envelope : Mail
{
    [SerializeField]
    private float letterDragDist = 80f;

    [SerializeField]
    private GameObject closedFlap;
    [SerializeField]
    private GameObject openFlap;

    protected void Update()
    {
        if (dragging)
        {
            Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit;
            hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 4f, .1f);

            // mail lid hit, start dragging  
            if (hit.collider != null)
            {
                // update change in mouse y  
                currentDragDist += Mouse.current.delta.ReadValue().y;

                // check if letter can be opened
                if (currentState == MailState.Unsealed)
                {
                    Open();
                }
                // check if letter can be closed
                else if (currentState == MailState.Opened)
                {
                    Close();
                }
            }
        }
    }

    /// <summary>
    /// If drag distance met open letter, 
    /// recieve letter inside 
    /// </summary>
    protected override void Open()
    {
        Debug.Log(currentDragDist);
        if (currentDragDist >= letterDragDist)
        {
            Debug.Log("Opened letter");

            // visuals
            openFlap.SetActive(true);
            closedFlap.SetActive(false);

            // update state 
            UpdateMailState();
            currentDragDist = 0;

            // get mail from letter

        }
    }

    /// <summary>
    /// If drag distance met close letter
    /// </summary>
    protected override void Close()
    {
        if (currentDragDist <=(-1*letterDragDist))
        {
            // close letter
            Debug.Log("Closed letter");

            // visuals
            openFlap.SetActive(false);
            closedFlap.SetActive(true);

            // update state
            UpdateMailState();
            currentDragDist = 0;
        }
    }

    public override void Raycast()
    {
        dragging = true;
    }


    // method to add seal, may be moved to seal use  
    //      check if holding wax sealer
    //      ckeck if clicked if right spot
    //      

    // look into trail render for slice 
}
