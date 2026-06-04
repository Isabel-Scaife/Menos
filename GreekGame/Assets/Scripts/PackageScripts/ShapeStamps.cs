using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShapeStamps : Tool
{
    [SerializeField]
    private GameObject shapePrefab;

    [SerializeField]
    private LayerMask clickable;

    [SerializeField]
    private Color currentColor; // change color of prefeb when placed 

    public Color CurrentColor { get => currentColor; set => currentColor = value; }

    void Start()
    {
        
        Collider2D myCollider = GetComponent<Collider2D>();

        // update instances color to match parent
        Tool stamp = PackageManager.Instance.CurrentTool;

        if (stamp != null)
        {
            currentColor = stamp.GetComponent<ShapeStamps>().currentColor;

            // call to find if placed in right zone on vase 
            VasePackage.Instance.CheckCollidersHit(myCollider);
        }

    }

    /// <summary>
    /// Performs tools designate action when the user clicks
    /// </summary>
    public override void Use() 
    {
        // determine what was hit 
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit; 
        hit = Physics2D.Raycast(worldPos, Vector2.zero, 10.0f, clickable);
        Collider2D collider = hit.collider;

        Debug.Log(collider.tag);
        Debug.Log(collider.name);
        // vase hit, place stamp at mouse location
        if (collider.CompareTag("Vase"))
        {
            Debug.Log("on vase");
            PlaceStamp(worldPos);
        }

        // color hit, change color of stamp 
        if (collider.CompareTag("Color"))
        {
            ChangeColor(collider);
        }
    }


    /// <summary>
    /// Place stamp at location 
    /// </summary>
    /// <param name="position">mouse position</param>
    private void PlaceStamp(Vector3 position)
    {
        // add shape where clicked 
        GameObject newObj = Instantiate(shapePrefab, position, Quaternion.identity);

        // match color and apply layer order
        SpriteRenderer sprite = newObj.GetComponent<SpriteRenderer>();
        sprite.color = currentColor;

        sprite.sortingOrder = VasePackage.Instance.SortOrder;
        VasePackage.Instance.SortOrder++;
    }

    /// <summary>
    /// Change color of stamp to color of collider
    /// </summary>
    private void ChangeColor(Collider2D collider)
    {
        currentColor = collider.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        VasePackage.Instance.CurrentColor = currentColor;
    }

    public override void SelectTool()
    {
        // apply color and pick up tool 
        VasePackage.Instance.CurrentColor = currentColor;
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
