using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vase : MonoBehaviour
{
    [SerializeField, Range(50, 100)]
    private float completeThreshold = 80;

    private int amtToWin;
    private int pointsCorrect;   // +1 for correct, -1 for incorrect 
    [SerializeField, Range(0, 270)]
    private int correctSpaces;

    [SerializeField]
    private Collider2D incorrectArea;
    private bool inLoseZone = false;

    private List<CorrectPoint> correctPointsHit;

    private Color currentColor = Color.white;
    private List<GameObject> shapesPlaced;
    private int sortOrder = 1;

    public static Vase Instance { get; private set; }
    public int SortOrder {  get => sortOrder; set => sortOrder = value; }

    public Color CurrentColor { get => currentColor; set => currentColor=value; }

    private void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        amtToWin = (int)(correctSpaces * completeThreshold * .01);

        shapesPlaced = new List<GameObject>();
        correctPointsHit = new List<CorrectPoint>();
    }

    public void CheckCollidersHit(Collider2D shapeCollider)
    {
        shapesPlaced.Add(shapeCollider.gameObject);

        // 1. check if player already lost 
        if(!inLoseZone)
        {
            // 2. find all colliders hit 
            List<Collider2D> collidersHit = new List<Collider2D>();
            shapeCollider.Overlap(collidersHit);

            foreach (Collider2D collider in collidersHit)
            {
                if (collider.CompareTag("IncorrectZone"))
                {
                    // 3. placed in lose zone, end early
                    inLoseZone = true;
                    return;
                }
                else if (collider.CompareTag("CorrectZone"))
                {
                    // 4. correct point collider hit, update correct count

                    CorrectPoint point = collider.GetComponent<CorrectPoint>();
                    int paintValue = point.PaintPoint(currentColor);

                    if(paintValue == 1)
                    {
                        correctPointsHit.Add(point);
                    }

                    pointsCorrect += paintValue;
                }
            }

            if(amtToWin <= pointsCorrect)
            {
                Debug.Log("Complete Puzzle");
                ///     - 2. vase needs threshold for completion (seralize field) 
                ///         - once passed, play complete animation
                ///             - food enter vase 
                ///             - sealed shut
                ///             - close scene 
                ///         - else nothhing happens
                SceneManager.LoadScene("Vineyard");
            }
        }
    }

    public void ResetImage()
    {
        // 1. remove placed shapes
        int shapeCount = shapesPlaced.Count;

        for(int i = 0; i < shapeCount; i++)
        {
            Destroy(shapesPlaced[i]);
        }
        shapesPlaced.RemoveRange(0, shapeCount);

        // 2. reset points
        for(int i = 0; i < correctPointsHit.Count; i++)
        {
            correctPointsHit[i].ResetPoint();
        }

        // 3. reset count  
        pointsCorrect = 0;
        inLoseZone = false;

        // 4. reset sort layer count
        SortOrder = 1;
    }

    /// <summary>
    /// Switches the shape the player must make
    /// </summary>
    /// <param name="newShape">Name of the shape to switch to</param>
    public void SwitchShape(string newShape)
    {
        //TriangleVase.Visible = false;
        //newShape.Visible = true;
    }
}
