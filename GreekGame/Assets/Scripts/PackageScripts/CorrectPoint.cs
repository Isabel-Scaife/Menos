using UnityEngine;

public class CorrectPoint : MonoBehaviour
{
    // [SerializeField]
    // private Color currentColor;
    [SerializeField]
    private Color correctColor = Color.white;

    private bool isPainted = false;

    /// <summary>
    /// Determines is point is covered with correct color
    /// </summary>
    /// <param name="paintColor">color painting with</param>
    /// <returns>
    /// 1 - point correctly painted
    /// 0 - point already painted, or painted incorrectly
    /// -1 - point painted changing to incorrect color 
    /// </returns>
    public int PaintPoint(Color paintColor)
    {
        Debug.Log("Needed color: " + correctColor + "\tPainted Color: " + paintColor);
        // 1. covered point with correct color, and is not painted 
        if (paintColor == correctColor && !isPainted)
        {
            isPainted = true;
            return 1;
        }
        
        // 2. already painted, replacing to incorrect color  
        if(paintColor != correctColor && isPainted)
        {
            isPainted = false;
            return -1;
        }

        // 3. already painted, or painting
        //    for first time with incorrect color 
        return 0;
    }

    public void ResetPoint()
    {
        isPainted = false;
    }
}
