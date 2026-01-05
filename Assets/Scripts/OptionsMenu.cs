using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private static readonly Vector2 size1 = new Vector2(1920f, 1080f);
    private static readonly Vector2 size2 = new Vector2(1366f, 768f);

    [SerializeField] List<Vector2> sizes = new() {size1, size2};
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mainMenuButtons;
    public void SetScreenSize(TMP_Dropdown change)
    {
        if (Screen.fullScreen) return;
        Debug.Log((int)sizes[change.value].x + " " +  (int)sizes[change.value].y);
        Screen.SetResolution((int)sizes[change.value].x, (int)sizes[change.value].y, FullScreenMode.Windowed);
    }
    public void onScreenSizeCheck()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void onPauseBack()
    {
        mainMenuButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
