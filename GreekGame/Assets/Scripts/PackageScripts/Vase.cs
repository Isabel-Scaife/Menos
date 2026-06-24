using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vase : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private int spaces;
    private int numCorrect; // +1 for correct, -1 for incorrect 
    private List<CorrectPoint> correctPointsHit;

    [SerializeField] private Collider2D loseArea;
    private bool lost = false;
    private List<GameObject> shapesPlaced;

    public static Vase Instance { get; private set; }
    public int SortOrder {  get; set; }
    public Color CurrentColor { get; set; }

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

        SortOrder = 1;
        CurrentColor = Color.white;

        shapesPlaced = new List<GameObject>();
        correctPointsHit = new List<CorrectPoint>();
    }

    public void CheckCollidersHit(Collider2D shapeCollider)
    {
        shapesPlaced.Add(shapeCollider.gameObject);

        // 1. check if player already lost 
        if (lost) return;

        // 2. find all colliders hit 
        List<Collider2D> collidersHit = new List<Collider2D>();
        shapeCollider.Overlap(collidersHit);

        foreach (Collider2D collider in collidersHit)
        {
            // 3. lose end early 
            if (collider.CompareTag("IncorrectZone")) { lost = true; return; }
            
            // 4.update correct count
            if (collider.CompareTag("CorrectZone"))
            {
                CorrectPoint point = collider.GetComponent<CorrectPoint>();
                int paintValue = point.PaintPoint(CurrentColor);

                if(paintValue == 1)
                {
                    correctPointsHit.Add(point);
                }

                numCorrect += paintValue;
            }
        }

        // 5. complete puzzle if there are enough correct 
        if(numCorrect >= spaces)
        {
            Debug.Log("Complete Puzzle");

            CompletePuzzle();

            //SceneManager.LoadScene("Vineyard");
        }
        
    }

    private void CompletePuzzle()
    {
        // play animation needs await (finish animation before moving on) 

        if (OnComplete != null)
        {
            OnComplete();
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
        numCorrect = 0;
        lost = false;

        // 4. reset sort layer count
        SortOrder = 1;
    }

    private void OnDestroy()
    {
        for (int i = 0; i <  shapesPlaced.Count; i++)
        {
            Destroy(shapesPlaced[i]);
        }
    }
}
