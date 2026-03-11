using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShapeStamps : Tool
{
    [SerializeField]
    private GameObject shapePrefab;

    [SerializeField]
    private LayerMask vaseLayer;

    [SerializeField]
    private Color currentColor; // change color of prefeb when placed 

    public Color CurrentColor { get => currentColor; set => currentColor = value; }

    void Start()
    {
        
        Collider2D myCollider = GetComponent<Collider2D>();

        // call to find if placed in right zone on vase 
        VasePackage.Instance.CheckCollidersHit(myCollider);

    }

    /// <summary>
    /// Performs tools designate action when the user clicks
    /// </summary>
    public override void Use() 
    {
        // 1. check if on pot
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit; 
        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, vaseLayer, 0f);
        Collider2D collider = hit.collider;

        if (collider != null)
        {
            // 2. add current shape to scene where clicked 
            GameObject newObj = Instantiate(shapePrefab, worldPos, Quaternion.identity);
            newObj.GetComponent<SpriteRenderer>().color = currentColor;
        }

        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, -1, 1.1f);
        collider = hit.collider;

        if (collider != null && collider.CompareTag("NewColor"))
        {
            // 3. change color of sprite shape and in manager
            currentColor = collider.GetComponent<SpriteRenderer>().color;

            gameObject.GetComponent<SpriteRenderer>().color = currentColor;
            VasePackage.Instance.CurrentColor = currentColor;
        }
    }

    public override void SelectTool()
    {
        // 1. set color to picked up shapes color 
        VasePackage.Instance.CurrentColor = currentColor;

        // 2. call base select tool 
        base.SelectTool();
    }

    /// <summary>
    /// Use the stamp 
    /// </summary>
    public override void RayCast()
    {
        Use();
    }

}
