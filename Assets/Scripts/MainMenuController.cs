using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public  GameObject menuCanvas;
    [SerializeField] public  GameObject mainMenuButtons;
    [SerializeField] public  GameObject game;
    [SerializeField] public  GameObject optionsMenu;

    private void Awake()
    {
        mainMenuButtons.SetActive(true);
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }
    public void onStartGamePressed()
    {
        game.SetActive(true);
        menuCanvas.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void onOptionsPressed()
    {
        optionsMenu.SetActive(true);
        game.SetActive(false);
        mainMenuButtons.SetActive(false);
    }

    public void onExitPressed()
    {
        Application.Quit();
    }


}
