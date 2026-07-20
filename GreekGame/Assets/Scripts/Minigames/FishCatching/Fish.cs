using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Fish : MonoBehaviour
{
    //parameters
    public float trueSize;
    private float currentSize;
    public float colorNumR;
    public float colorNumG;
    public float colorNumB;

    public SpriteRenderer spriteRenderer;
    private Collider2D objCollider;
    private Rigidbody2D rb;
    private FishSpawnManager fishSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets fish parameters
        trueSize = Random.Range(0, 5);
        colorNumR = Random.Range(0, 255)/255f;
        colorNumG = Random.Range(0, 255)/255f;
        colorNumB = Random.Range(0, 255)/255f;

        //sets current size to a third of true size so 
        //it can appear to float to the surface
        currentSize = trueSize / 3f;

        //sets fish physical appearance
        transform.localScale = new Vector3(currentSize, currentSize, 1);

        //sets fish color 
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new UnityEngine.Color(colorNumR, colorNumG, colorNumB, 1);

        //sets fish spawn location
        transform.position = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 1);

        ///connects to fish spawn
        fishSpawn = GameObject.FindFirstObjectByType<FishSpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //makes fish slowly get bigger if they are not at max size
        //appears to "float up"

        if (currentSize <= trueSize)
        {
            currentSize = (currentSize + .02f);
            transform.localScale = new Vector3(currentSize, currentSize, 1);
        }

    }

    //detetcs when moused over and clicked on
    private void OnMouseDown()
    {
        Debug.Log("Fish grabbed");

        ///adds it in fish manager to list of fish caught
        fishSpawn.fishList.Add(this);

        //disables from view
        //cannot destory object bc then reference is removed frrom fish list
        GetComponent<Renderer>().enabled = false;
    }
    
}
