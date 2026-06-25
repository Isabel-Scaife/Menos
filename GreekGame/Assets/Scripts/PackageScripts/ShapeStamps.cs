using UnityEngine;
using UnityEngine.InputSystem;

public class ShapeStamps : Tool
{
    [SerializeField]
    private GameObject shapePrefab;

    [SerializeField]
    private Color currentColor; // change color of prefeb when placed 

    public Color CurrentColor { get => currentColor; set => currentColor = value; }

    void Start()
    {
        
        Collider2D myCollider = GetComponent<Collider2D>();

        // update instances color to match parent
        Tool stamp = ToolManager.Instance.CurrentTool;

        if (stamp != null)
        {
            currentColor = stamp.GetComponent<ShapeStamps>().currentColor;

            // call to find if placed in right zone on vase 
            VaseMinigame.Instance.CheckCollidersHit(myCollider);
        }

    }

    /// <summary>
    /// Performs tools designate action when the user clicks
    /// </summary>
    public override void Use() 
    {
        // determine what was hit 
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D[] hit; 
        hit = Physics2D.RaycastAll(worldPos, Vector2.zero, 10.0f, clickable);

        // hit nothing 
        if (hit.Length <= 0) return;
        
        // color hit, change color of stamp 
        if (hit[0].collider.CompareTag("Color"))
        {
            ChangeColor(hit[0].collider);
        }

        // vase hit, place stamp at mouse location
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Vase"))
            {
                PlaceStamp(worldPos, hit[i].collider.transform);
            }
        }
    }


    /// <summary>
    /// Place stamp at location 
    /// </summary>
    /// <param name="position">mouse position</param>
    private void PlaceStamp(Vector3 position, Transform parent)
    {
        // add shape where clicked 
        GameObject newObj = Instantiate(shapePrefab, position, Quaternion.identity, parent);

        // match color and apply layer order
        SpriteRenderer sprite = newObj.GetComponent<SpriteRenderer>();
        sprite.color = currentColor;

        sprite.sortingOrder = VaseMinigame.Instance.SortOrder;
        VaseMinigame.Instance.SortOrder++;
    }

    /// <summary>
    /// Change color of stamp to color of collider
    /// </summary>
    private void ChangeColor(Collider2D collider)
    {
        currentColor = collider.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        VaseMinigame.Instance.CurrentColor = currentColor;
    }

    public override void SelectTool()
    {
        // apply color and pick up tool 
        VaseMinigame.Instance.CurrentColor = currentColor;
        base.SelectTool();
    }

    public override void DropTool()
    {
        base.DropTool();

        transform.position = startPosition;

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 10;

    }

}
