using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MenuManager : MonoBehaviour
{
    private GameManager gm;
    public GameObject loginMenu;
    public InputField email;
    public InputField password;

    public GameObject emailError;
    public GameObject passwordError;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        loginMenu.SetActive(false);
        emailError.gameObject.SetActive(false);
        passwordError.gameObject.SetActive(false);
    }


    /// <summary>
    /// TODO: Should check if the user is logged in. If he/she is, it loads the game. If not it shows the login scene.
    /// </summary>
    public void PlayGame()
    {

    }

    /// <summary>
    /// Verifies the email and password fields and calls login endpoint
    /// </summary>
    public void Login()
    {
        emailError.SetActive(false);
        passwordError.SetActive(false);
        if (email.text == "")
        {
            emailError.SetActive(true);
        }
        if (password.text == "")
        {
            passwordError.GetComponent<Text>().text = "Debe introducir una contraseña";
            passwordError.SetActive(true);
        }


        if (email.text != "" && password.text != "")
        {
            // Login
            IDictionary<string, object> body = new Dictionary<string, object>();
            body.Add("email", email.text);
            body.Add("password", password.text);
            Server.Instance.DoRequest("/login", body, OnLoginSuccess, OnLoginError);

        }
    }

    /// <summary>
    /// Gets executed when login is successfull. Sets the player name in game manager.
    /// TODO: Go to Game
    /// </summary>
    /// <param name="data"></param>
    private void OnLoginSuccess(string data)
    {
        Debug.Log(data);
        Response<User> res = JsonConvert.DeserializeObject<Response<User>>(data);
        if (res.error != null)
        {
            passwordError.GetComponent<Text>().text = res.error;
            passwordError.SetActive(true);
        }
        else
        {
            gm.SetUserName(res.user.name);
            gm.SetUserLoged(true);
        }
    }

    /// <summary>
    /// Gets executed when login fails. 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    private void OnLoginError(int code, string data)
    {
        passwordError.GetComponent<Text>().text = data;
        passwordError.SetActive(true);
    }
}
