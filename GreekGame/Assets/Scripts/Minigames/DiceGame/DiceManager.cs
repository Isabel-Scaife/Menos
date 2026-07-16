using UnityEngine;

// manages dice game logic
public class DiceManager : MonoBehaviour
{
    // singleton
    public static DiceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
