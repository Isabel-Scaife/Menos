using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [Header("Follow Images")]
    [SerializeField] Image imgFollow;
    [SerializeField] Sprite follow;
    [SerializeField] Sprite stopFollow;

    [Header("Switch Images")]
    [SerializeField] Image imgSwitch;
    [SerializeField] Sprite birdSwitch;
    [SerializeField] Sprite playerSwitch;

    public void ToggleSwitch()
    {
        if (imgSwitch.sprite == birdSwitch)
        {
            imgSwitch.sprite = playerSwitch;
        }
        else
        {
            imgSwitch.sprite = birdSwitch;
        }
    }

    public void ToggleFollow()
    {
        if (imgFollow.sprite == follow)
        {
            imgFollow.sprite = stopFollow;
        }
        else
        {
            imgFollow.sprite = follow;
        }
    }
}
