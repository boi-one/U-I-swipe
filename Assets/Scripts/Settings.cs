using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings reference;
    public bool detailedEffect = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        reference = this;
    }

    public void ChangeEffect()
    {
        detailedEffect = !detailedEffect;
    }
}
