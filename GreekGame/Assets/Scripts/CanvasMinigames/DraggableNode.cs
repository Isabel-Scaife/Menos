using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// node that connects vines (edges), may or may not be draggable
public class DraggableNode : MonoBehaviour, IDragHandler
{
    // fields
    [SerializeField]
    private bool immovable;
    private RectTransform rectTransform;
    private List<VineEdge> connectedEdges;

    // gets components
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // moves when dragged if allowed to
    public void OnDrag(PointerEventData eventData)
    {
        if (immovable) return;
        rectTransform.anchoredPosition += eventData.delta;
    }
}