using UnityEngine;

// minigame vine that connects two draggable nodes
public class VineEdge : MonoBehaviour
{
    // fields
    public RectTransform nodeA;
    public RectTransform nodeB;
    private RectTransform rectTransform;
    private bool onObject = true;

    // functions
    void Awake()
    {
        // gets components
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // add vine to nodes list
        nodeA.GetComponent<DraggableNode>().connectedEdges.Add(this);
        nodeB.GetComponent<DraggableNode>().connectedEdges.Add(this);
    }

    void LateUpdate()
    {
        // exits if missing one or both endpoints
        if (nodeA == null || nodeB == null) return;

        // updates position base on endpoint positions
        Vector2 posA = nodeA.anchoredPosition;
        Vector2 posB = nodeB.anchoredPosition;
        rectTransform.anchoredPosition = (posA + posB) / 2f;

        // stretches and rotates
        float dist = Vector2.Distance(posA, posB);
        rectTransform.sizeDelta = new Vector2(dist, rectTransform.sizeDelta.y);
        Vector2 direction = posB - posA;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);

    }

    public void CheckOverlap()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // create rect to compare
        Rect vineRect = new Rect(
            corners[0].x,
            corners[0].y,
            corners[2].x -  corners[0].x,
            corners[2].y - corners[0].y);

        corners = VineDragMinigame.Instance.coveredCorners;
        Rect coveredRect = new Rect(
            corners[0].x,
            corners[0].y,
            corners[2].x -  corners[0].x,
            corners[2].y - corners[0].y);

        // vine moved off object 
        if (!vineRect.Overlaps(coveredRect))
        {
            // increase count if vine moved off for first time
            if(onObject)
            {
                Debug.Log("No overlap: " + this.name);
                VineDragMinigame.Instance.VineOff();
                onObject = false;
            }
        }
        else
        {   // vine placed back on object 
            if (!onObject)
            {
                Debug.Log("RE-overlap:" + this.name);
                VineDragMinigame.Instance.VineOn();
                onObject = true;
            }
        }
    }
}