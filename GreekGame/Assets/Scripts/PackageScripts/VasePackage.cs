using System.Collections.Generic;
using UnityEngine;

public class VasePackage : MonoBehaviour
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

    private Color currentColor = Color.white;
    private List<GameObject> shapesPlaced;

    public static VasePackage Instance { get; private set; }
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
                    Debug.Log("lose zone");
                    inLoseZone = true;
                    return;
                }
                else if (collider.CompareTag("CorrectZone"))
                {
                    // 4. correct point collider hit, update correct count
                    pointsCorrect += collider.GetComponent<CorrectPoint>().
                        PaintPoint(currentColor);

                    Debug.Log("Amt Correct: " + pointsCorrect);
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
            }
        }
    }

    public void ResetImage()
    {
        // 1. remove placed shapes
        int shapeCount = shapesPlaced.Count;

        for(int i = 8; i < shapeCount; i++)
        {
            Destroy(shapesPlaced[i]);
        }
        shapesPlaced.RemoveRange(8, shapeCount - 8);

        // 2. reset count  
        pointsCorrect = 0;
    }
}
