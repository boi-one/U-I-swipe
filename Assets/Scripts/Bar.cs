using UnityEngine;

public class Bar : MonoBehaviour
{
    UnityEngine.UI.Image foreground;
    float maxAmount = 10;
    float amount = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreground = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        foreground.fillAmount = amount/maxAmount;
    }

    public void SetValue(float value)
    {
        amount = value;
    }

    public void AddValue(float value)
    {
        amount += value;
    }
}
