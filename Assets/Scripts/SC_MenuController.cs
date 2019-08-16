using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MenuController : MonoBehaviour
{
    public SC_MenuLogic sc_MenuLogic;

    public void ChangeScreen(string _NewScreen)
    {
        GlobalEnums.Screens _newScreen = (GlobalEnums.Screens)GlobalEnums.Screens.Parse(typeof(GlobalEnums.Screens), _NewScreen);
        sc_MenuLogic.ChangeScreen(_newScreen);
    }

    public void MultiplayerSlider()
    {
        if (sc_MenuLogic != null)
            sc_MenuLogic.MultiplayerSlider();
    }

    public void OptionsMusicSlider()
    {
        if (sc_MenuLogic != null)
            sc_MenuLogic.OptionsMusicSlider();
    }

    public void OptionsSFXSlider()
    {
        if (sc_MenuLogic != null)
            sc_MenuLogic.OptionsSFXSlider();
    }

    public void OpenLinkToBrowser()
    {
        if (sc_MenuLogic != null)
            sc_MenuLogic.OpenURL();
    }

    public void MoveCursorToButton(GameObject sender)
    {
        if (sc_MenuLogic != null)
            sc_MenuLogic.moveCursor(sender);
    }
}
