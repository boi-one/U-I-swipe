using UnityEngine;
using UnityEngine.EventSystems;

public class ParameterHover : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 mouseWorldPosition;
    public string displayName;

    bool wasInside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        bool isInside = GetComponent<BoxCollider2D>().OverlapPoint(mouseWorldPosition);

        if (!wasInside && isInside)
        {
            wasInside = true;
            Enter();
        }
        else if (wasInside && !isInside)
        {
            wasInside = false;
            Exit();
        }
    }

    public void Enter()
    {
        Tooltip.reference.SetName(displayName);
        Tooltip.reference.SetVisible();
    }

    public void Exit()
    {
        Tooltip.reference.SetInvisible();
    }

}
