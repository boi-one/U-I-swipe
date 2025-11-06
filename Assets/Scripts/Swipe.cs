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
    float totalDistance = 0f;
    float side;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cardSpriteRenderer = GetComponent<SpriteRenderer>();
        basePosition = transform.position;
        SetParameterBar();
    }

    // Update is called once per frame
    void Update()
    {
        mouseScreenPosition = Input.mousePosition;
        float camToCard = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        mouseWorldPosition = new Vector3(mouseWorldPosition.x, transform.position.y, mouseWorldPosition.z);
        distanceFromCenter = (transform.position - basePosition).magnitude;

        float clamp = Mathf.Clamp01(distanceFromCenter * 0.3f);
        float invertedClamp = 1f - clamp;

        cardSpriteRenderer.color = new Color(cardSpriteRenderer.color.r, cardSpriteRenderer.color.g, cardSpriteRenderer.color.b, invertedClamp);

        if (Input.GetMouseButtonDown(0))
        {
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
            side = (transform.position.x - basePosition.x) >= 0f ? -1f : 1f;
            float rot = side * (distanceFromCenter * 10);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rot));

            for (int i = 0; i < CardManager.instance.card.impact.Length; i++)
            {
                float scaledValue;

                SpriteRenderer[] impact = CardManager.instance.card.impact;
                Card cc = CardManager.instance.currentCard;
                
                if(!Settings.reference.detailedEffect)
                {
                    scaledValue = (side >= 0) ? Mathf.Lerp(0f, cc.parameters[i].rightValueMin, clamp) : Mathf.Lerp(0f, cc.parameters[i].leftValueMin, clamp);
                    impact[i].transform.localScale = Vector2.one * Mathf.Abs(scaledValue) * 0.04f;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            transform.position = basePosition;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            foreach (SpriteRenderer s in CardManager.instance.card.impact)
                s.transform.localScale = new Vector2(0f, 0f);

            if (invertedClamp < 0.1f)
            {
                if (CardManager.instance.cardDeck.cards.Length >= 1)
                    CardManager.instance.SetCard();
                else UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
                for (int i = 0; i < CardManager.instance.card.paramaterValues.Length; i++)
                {
                    int value = (side >= 0) ? CardManager.instance.currentCard.parameters[i].rightValueMin : CardManager.instance.currentCard.parameters[i].leftValueMin;
                    CardManager.instance.card.paramaterValues[i] += value;

                    if (CardManager.instance.card.paramaterValues[i] <= 0)
                    { 
                        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
                    }
                }
                SetParameterBar();
            }
        }
    }

    void SetParameterBar()
    {
        for (int i = 0; i < CardManager.instance.card.parameterValueBar.Length; i++)
        {
            CardManager.instance.card.parameterValueBar[i].SetValue(CardManager.instance.card.paramaterValues[i]);
        }
    }
}
