using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_MenuLogic : MonoBehaviour
{
    public bool isLoading = false;
    private float loadingTimer = 0;
    GameObject loadingCursor;
    RectTransform loadingCursorTransform;

    private Dictionary<string, GameObject> projectScripts;
    private Dictionary<string, GameObject> projectObjects;

    private GlobalEnums.Screens currentScreen;
    private GlobalEnums.Screens prevScreen;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        GameObject[] _screens = GameObject.FindGameObjectsWithTag("Screens");
        GameObject[] _objects = GameObject.FindGameObjectsWithTag("Objects");
        projectScripts = new Dictionary<string, GameObject>();
        projectObjects = new Dictionary<string, GameObject>();

        foreach (GameObject scr in _screens)
        {
            projectScripts.Add(scr.name, scr);
            projectScripts[scr.name].SetActive(false);
        }

        foreach (GameObject obj in _objects)
            projectObjects.Add(obj.name, obj);

        ChangeScreen(GlobalEnums.Screens.MainMenu);
        moveCursor(projectObjects["SCREEN_MainMenu_BTN_Singleplayer"]);
        loadingCursor = projectObjects["SCREEN_Loading_Cursor"];
        loadingCursorTransform = loadingCursor.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if(isLoading)
        {
            if (loadingTimer >= 3)
                isLoading = false;
            loadingTimer += Time.deltaTime;
            LoadingScreen(loadingTimer);    
        }
    }
    public void ChangeScreen(GlobalEnums.Screens _NewScreen)
    {
        if (currentScreen != GlobalEnums.Screens.Default)
            projectScripts["SCREEN_" + currentScreen].SetActive(false);

        if (_NewScreen == GlobalEnums.Screens.Options)
            prevScreen = currentScreen;

        if (currentScreen == GlobalEnums.Screens.Options)
            _NewScreen = prevScreen;

        projectScripts["SCREEN_" + _NewScreen].SetActive(true);
        currentScreen = _NewScreen;

        if (_NewScreen == GlobalEnums.Screens.Loading)
            isLoading = true;
    }

    public void OpenURL()
    {
        Application.OpenURL("https://drive.google.com/open?id=1IAFclh3bCtMnyz3MyN15vBqCwqRW2cuR");
    }

    public void MultiplayerSlider()
    {
        int _value = (int)projectObjects["SCREEN_Multiplayer_Slider"].GetComponent<Slider>().value;
        projectObjects["SCREEN_Multiplayer_Slider_TXT"].GetComponent<Text>().text = _value.ToString();
    }

    public void OptionsMusicSlider()
    {
        int _value = (int)projectObjects["SCREEN_Options_Slider_Music"].GetComponent<Slider>().value;
        projectObjects["SCREEN_Options_Slider_TXT_Music"].GetComponent<Text>().text = _value.ToString();
    }

    public void OptionsSFXSlider()
    {
        int _value = (int)projectObjects["SCREEN_Options_Slider_SFX"].GetComponent<Slider>().value;
        projectObjects["SCREEN_Options_Slider_TXT_SFX"].GetComponent<Text>().text = _value.ToString();
    }

    private void LoadingScreen(float time)
    {
        projectObjects["SCREEN_Loading_Slider"].GetComponent<Slider>().value = time;
        if((int)time % 2 == 0)
        {
            loadingCursorTransform.localScale = new Vector3(1, 1, 1);
            loadingCursor.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            loadingCursorTransform.localScale = new Vector3(-1, 1, 1);
            loadingCursor.transform.GetChild(1).gameObject.SetActive(true);
        }

    }

    public void moveCursor(GameObject sender)
    {
        GameObject myCoolCursor = projectObjects["SCREEN_MainMenu_Cursor"];
        RectTransform myRectTransform = myCoolCursor.GetComponent<RectTransform>();
        RectTransform middle = projectObjects["SCREEN_MainMenu_BTN_Singleplayer"].GetComponent<RectTransform>();
        Vector2 tmp = new Vector2(middle.anchoredPosition.x, middle.anchoredPosition.y - 250);

        switch (sender.gameObject.name)
        {
            case "SCREEN_MainMenu_BTN_Singleplayer":
                myRectTransform.anchoredPosition = sender.GetComponent<RectTransform>().anchoredPosition + new Vector2(300, -30);
                myRectTransform.localScale = new Vector3(1, 1, 1);
                myCoolCursor.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case "SCREEN_MainMenu_BTN_Multiplayer":
                myRectTransform.anchoredPosition = sender.GetComponent<RectTransform>().anchoredPosition + new Vector2(300, -30);
                myRectTransform.localScale = new Vector3(1, 1, 1);
                myCoolCursor.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case "SCREEN_MainMenu_BTN_Student":
                myRectTransform.anchoredPosition = tmp;
                myRectTransform.localScale = new Vector3(1, 1, 1);
                myCoolCursor.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case "SCREEN_MainMenu_BTN_Options":
                myRectTransform.anchoredPosition = tmp;
                myRectTransform.localScale = new Vector3(-1, 1, 1);
                myCoolCursor.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case null:
                break;

        }

    }
}
