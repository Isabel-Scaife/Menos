using UnityEngine;
using System.Collections.Generic;

public class BoulderTutorial : TutorialTask
{
    // fields
    [SerializeField]
    private Transform[] boulders;

    [SerializeField]
    private float distanceToMove;

    private Vector2[] startPositions;

    private void Awake()
    {
        // get each boulders starting position
        startPositions = new Vector2[boulders.Length];
        for (int i = 0; i < boulders.Length; i++)
        {
            startPositions[i] = boulders[i].position;
        }
    }

    public override bool Ready()
    {
        return true;
    }

    public override bool Completed()
    {
        // returns true if any boulder has moved at least target distance
        for (int i = 0; i < boulders.Length; i++)
        {
            if (Vector2.Distance(startPositions[i], (Vector2)boulders[i].position) >= distanceToMove)
            {
                return true;
            }
        }
        return false;
    }
}
