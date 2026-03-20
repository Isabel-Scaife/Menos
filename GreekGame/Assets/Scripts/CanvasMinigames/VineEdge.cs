using UnityEngine;

// minigame vine that connects two draggable nodes
public class VineEdge : MonoBehaviour
{
    // fields
    public RectTransform nodeA;
    public RectTransform nodeB;
    private RectTransform rectTransform;

    // functions
    void Awake()
    {
        // gets components
        rectTransform = GetComponent<RectTransform>();
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
}