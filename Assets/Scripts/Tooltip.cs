using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public static Tooltip reference;
    public TMP_Text tooltipText;
    public bool over = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reference = this;
        SetInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        tooltipText.transform.position = Input.mousePosition;
    }

    public void SetVisible()
    {
        tooltipText.color = new Color(1, 1, 1, 1);
    }

    public void SetInvisible()
    {
        tooltipText.color = new Color(0, 0, 0, 0);
    }

    public void SetName(string name)
    {
        tooltipText.text = name;
    }
}
