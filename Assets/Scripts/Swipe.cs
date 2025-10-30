using UnityEngine;

public class Swipe : MonoBehaviour
{
    Vector3 mouseWorldPosition;
    Vector3 mouseScreenPosition;

    Vector3 basePosition;
    Vector3 offset;
    bool dragging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));


        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPosition))
            {
                dragging = true;
                offset = transform.position - mouseWorldPosition;

                transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z) + offset;
                Debug.Log("DUI");
            }
            else
            {
                Debug.Log("dont press");

            }
        }

        if (dragging && Input.GetMouseButton(0))
        {
            transform.position = mouseWorldPosition + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            transform.position = basePosition;
        }
    }


}
