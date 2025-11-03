using UnityEngine;

public class Swipe : MonoBehaviour
{
    Vector3 mouseWorldPosition;
    Vector3 mouseScreenPosition;

    Vector3 basePosition;
    Vector3 offset;
    bool dragging = false;
    SpriteRenderer cardSpriteRenderer;
    Camera mainCamera;
    float distanceFromCenter = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cardSpriteRenderer = GetComponent<SpriteRenderer>();
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
        float camToCard = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); 
        //mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, camToCard));
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        mouseWorldPosition = new Vector3(mouseWorldPosition.x, transform.position.y, mouseWorldPosition.z);
        distanceFromCenter = (transform.position - basePosition).magnitude;

        float clamp = Mathf.Clamp01(distanceFromCenter * 0.3f);
        float invertedClamp = 1f - clamp;

        cardSpriteRenderer.color = new Color(cardSpriteRenderer.color.r, cardSpriteRenderer.color.g, cardSpriteRenderer.color.b, invertedClamp);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            if (GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPosition))
            {
                dragging = true;
                offset = transform.position - mouseWorldPosition;

                transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z) + offset;
            }
        }

        if (dragging && Input.GetMouseButton(0))
        {
            transform.position = mouseWorldPosition + offset;


            float side = (transform.position.x - basePosition.x) >= 0f ? -1f : 1f;
            Debug.DrawLine(transform.position, basePosition, Color.red);
            float rot = side * (distanceFromCenter * 10);
            Debug.Log("r " + rot);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rot));

            if (mouseWorldPosition.y > basePosition.y)
            {
                //Vector2 dir = (transform.position - basePosition);
                //float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            transform.position = basePosition;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            if (invertedClamp < 0.1f)
            {
                Debug.Log("Bye");
                CardManager.instance.SetCard();
            }
        }
    }


}
