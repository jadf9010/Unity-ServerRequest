using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string userName = "";
    private bool isUserLoggedIn = false;

    public GameObject settingsMenu;
    public GameObject settingsCanvas;

    void Start()
    {
        settingsMenu.SetActive(false);
    }

    /// <summary>
    /// Checks if an user is logged in
    /// </summary>
    /// <returns>boolean saying wether the user is logged in</returns>
    public bool checkUserLogged()
    {
        return isUserLoggedIn;
    }

    /// <summary>
    /// Sets loged in boolean
    /// </summary>
    /// <param name="loged">is user loged</param>
    public void SetUserLoged(bool loged)
    {
        isUserLoggedIn = loged;
    }
   

    /// <summary>
    /// Shows or hides the settings menu
    /// </summary>
    /// <param name="show">Wether to show or hide the menu</param>
    public void ToggleSettings(bool show)
    {
        settingsMenu.SetActive(show);
    }

    /// <summary>
    /// User name setter
    /// </summary>
    /// <param name="name">new user name</param>
    public void SetUserName(string name)
    {
        userName = name;
    }

    /// <summary>
    /// User name getter
    /// </summary>
    /// <returns>user name</returns>
    public string GetUserName()
    {
        return userName;
    }


}
