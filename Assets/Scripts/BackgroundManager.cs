using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Images")]
    public Image backgroundImage;           // Reference to your UI Image component
    public Sprite mainMenuWallpaper;        // Assign in Inspector
    public Sprite optionsMenuWallpaper;     // Assign in Inspector

    // Switch naar de options achtergrond
    public void ShowOptionsWallpaper()
    {
        backgroundImage.sprite = optionsMenuWallpaper;
    }

    // switch terug naar de main menu wallpaper 
    public void ShowMainMenuWallpaper()
    {
        backgroundImage.sprite = mainMenuWallpaper;
    }
}
