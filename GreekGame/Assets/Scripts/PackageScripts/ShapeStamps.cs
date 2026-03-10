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

        // call to find if placed in right zone on vase 
        VasePackage.Instance.CheckCollidersHit(myCollider);

    }

    /// <summary>
    /// Performs tools designate action when the user clicks
    /// </summary>
    public override void Use() 
    {
        /// TO DO:
        /// 1. check if on pot 
        /// 2. if on pot duplicate current shape to scene where user clicked
        Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Instantiate(shapePrefab, worldPos, Quaternion.identity);
        ///         - (later) make sure object is correct coloe 
        /// 3. if on color change color 
        /// since object was placed in scene should auto call trigger 


    }

    /// <summary>
    /// Resets any trackers for using if action complete or cancellted 
    /// </summary>
    public override void ResetUse() 
    { 

    }

    /// <summary>
    /// Use the stamp 
    /// </summary>
    public override void RayCast()
    {
        Use();
    }

}
