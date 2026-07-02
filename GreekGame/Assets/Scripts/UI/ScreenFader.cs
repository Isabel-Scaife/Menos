using UnityEngine;
using System.Threading.Tasks;
using Unity.Cinemachine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private CinemachinePositionComposer cinemachinePositionComposer;
    
    private Vector3 orginalDamping;

    public static ScreenFader Instance { get; private set; }
    void Awake()
    {
        if (Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        orginalDamping = cinemachinePositionComposer.Damping;
    }

    private async Task Fade(float targetTransparency)
    {
        float start = canvasGroup.alpha; 
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, targetTransparency, t / fadeDuration);
            await Task.Yield();
        }
        canvasGroup.alpha = targetTransparency;
    }

    /// <summary>
    /// Fade to black
    /// </summary>
    public async Task FadeOut()
    {
        SetDamping(Vector3.zero);
        await Fade(1);
    }

    /// <summary>
    /// Fade to transparent
    /// </summary>
    public async Task FadeIn()
    {
        await Fade(0);
        SetDamping(orginalDamping);
    }

    /// <summary>
    /// Set camera damping 
    /// </summary>
    public void SetDamping(Vector3 damping)
    {
        cinemachinePositionComposer.Damping = damping; 
    }
}
