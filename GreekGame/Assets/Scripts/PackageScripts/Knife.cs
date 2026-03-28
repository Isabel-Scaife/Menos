using UnityEngine;
using UnityEngine.InputSystem;

public class Knife : Tool
{
    [SerializeField]
    private float sealDragDist = -2.5f;
    [SerializeField]
    private float currentDragDist = 0;

    private bool sealCut = false;
    private bool startCutting = false;

    // look into trail render for slice 

    protected override void Update()
    {
        // cut if holding mouse 
        if (mouseDown)
        { 
            // seal is still of letter
            if(!sealCut)
            {
                Cutting();
            }
        }

        base.Update();
    }

    public override void ResetUse()
    {
        // resest drag info
        currentDragDist = 0;
        mouseDown = false;
        startCutting = false;
    }

    private void CutSeal()
    {
        if (currentDragDist <= sealDragDist)
        {
            // remove seal 
            Debug.Log("Removed Seal");

            // update mail state
            GameObject mail = PackageManager.Instance.MailObj;

            if (mail != null)
            {
                mail.GetComponent<Mail>().UpdateMailState();
            }

            ResetUse();

            sealCut = true;
        }
    }

    /// <summary>
    /// Once seal zone hit start tracking change in mouse y 
    /// one reach threshold cut seal 
    /// </summary>
    private void Cutting()
    {
        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 3f, 3f);

        // seal zone hit, update y until cut 
        if (hit.collider != null && !startCutting)
        {
            startCutting = true;
        }

        if (startCutting)
        {
            currentDragDist += Mouse.current.delta.ReadValue().y;
            CutSeal();
        }
    }

    public override void RayCast()
    {
        mouseDown = true;
    }
}
