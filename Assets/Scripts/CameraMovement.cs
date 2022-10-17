using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;
    private float speed;
    
    void Start()
    {
        speed = 1;
        camera = Camera.main;
    }

    void Update()
    {
        /* For easyer use a little speed if shift is pressed */
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2.5f;
        }
        else
        {
            speed = 1;
        }

        /* Camera zoom */
        if (Input.mouseScrollDelta.y > 0)
        {
            camera.orthographicSize += speed;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            /* return if less than 5 so the size cant be in the - because if its in the - controls are flipped */
            if (camera.orthographicSize < 5)
            {
                return;
            }
            camera.orthographicSize -= speed;
        }


        /* Camera movement */
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical")* speed);
    }
}
