using UnityEngine;

public class ToolManager : MonoBehaviour
{

    [SerializeField]
    private Tool currentTool;

    public static ToolManager Instance { get; private set; }
    public Tool CurrentTool { get => currentTool; set => currentTool = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
