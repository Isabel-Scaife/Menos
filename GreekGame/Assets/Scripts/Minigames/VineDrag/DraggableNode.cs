using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// node that connects vines (edges), may or may not be draggable
public class DraggableNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // fields
    [SerializeField]
    private bool immovable;
    private RectTransform rectTransform;
    public List<VineEdge> connectedEdges;

    // gets components
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        connectedEdges = new List<VineEdge>();
    }

    // moves when dragged if allowed to
    public void OnDrag(PointerEventData eventData)
    {
        if (immovable) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // check collisions
        foreach (VineEdge edge in connectedEdges) 
        {
            Debug.Log("edge");
            edge.CheckOverlap();
        }
    }
}