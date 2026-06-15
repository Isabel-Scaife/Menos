using UnityEngine;

public class doorMechanic : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject camera;

    // Teleportingggg
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //player.transform.position = new Vector3(500, 500, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("arf");
        if (collision.gameObject.name == "Player")
        {
            //detects olive clicks and set it to dragging
            if (Input.GetMouseButton(0))
            {
                camera.transform.position = new Vector3(100, 100, 0);
                player.transform.position = new Vector3(100, 100, 0);
            }
        }
    }
}
