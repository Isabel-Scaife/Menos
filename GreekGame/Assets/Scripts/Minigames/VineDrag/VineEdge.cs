using UnityEngine;

// minigame vine that connects two draggable nodes
public class VineEdge : MonoBehaviour
{
    // fields
    public RectTransform nodeA;
    public RectTransform nodeB;
    private RectTransform rectTransform;
    [SerializeField] private bool onObject = true;

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
        // create vine line segment
        Vector3 startLine = nodeA.transform.position;
        Vector3 endLine = nodeB.transform.position;

        // create covered object rectangle
        Vector3[] corners = new Vector3[4];

        corners = VineDragMinigame.Instance.coveredCorners;
        Rect coveredRect = Rect.MinMaxRect(
            corners[0].x,
            corners[0].y,
            corners[2].x,
            corners[2].y);

        bool isOverlapping = IntersectsRect(startLine, endLine, coveredRect);

        // vine moved off object 
        if (!isOverlapping)
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
            else
            {
                Debug.Log("Overlap:" + this.name);
            }
        }
    }

    private bool IntersectsRect(Vector2 p1, Vector2 p2, Rect rect)
    {
        // check if points both are left or right of rect, therefore can not interesect
        if (Mathf.Max(p1.x, p2.x) < rect.xMin || Mathf.Min(p1.x, p2.x) > rect.xMax) return false;
        if (Mathf.Max(p1.y, p2.y) < rect.yMin || Mathf.Min(p1.y, p2.y) > rect.yMax) return false;

        // node in rect
        if (rect.Contains(p1) || rect.Contains(p2)) return true;

        float dx = p1.x - p2.x;
        float dy = p1.y - p2.y;

        float tMin = 0f;
        float tMax = 1f;

        // left and right of rect
        if (!ClipTest(-dx, p1.x - rect.xMin, ref tMin, ref tMax)) return false;
        if (!ClipTest(dx, rect.xMax - p1.x, ref tMin, ref tMax)) return false;

        // bottom and top of rect
        if (!ClipTest(-dy, p1.y - rect.yMin, ref tMin, ref tMax)) return false;
        if (!ClipTest(dy, rect.yMax - p1.y, ref tMin, ref tMax)) return false;

        return tMin <= tMax;
    }


    // Liang-Barkey parametric checks 
    private bool ClipTest(float p, float q, ref float tMin, ref float tMax)
    {
        if (p == 0)
        {
            // line parallel to bound line
            return q >= 0;
        }

        float t = q / p;

        if (p < 0)
        {
            if (t > tMax) return false;
            if (t > tMin) tMin = t; 
        }
        else
        {
            if (t < tMin) return false;
            if (t < tMax) tMax = t;
        }

        return true; 
    }
}