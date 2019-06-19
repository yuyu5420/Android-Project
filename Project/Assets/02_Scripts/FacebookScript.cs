using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using Facebook.MiniJSON;
using System.Collections;
 
public class FacebookScript : MonoBehaviour {
 
   // public Text FriendsText;
    public GameObject login;
    public GameObject logout;
    public GameObject head;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }
 

    #region Login / Logout
    public void CheckLogin(){
        if(FB.IsLoggedIn){
            login.SetActive(false);
            logout.SetActive(true);
            head.SetActive(true);
        }
        else{
            login.SetActive(true);
            logout.SetActive(false);
            head.SetActive(false);
        }
    }
    public void FacebookLogin()
    {
         var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
        CheckLogin();
    }
 
    public void FacebookLogout()
    {
        FB.LogOut();
        CheckLogin();
         CheckLogin();
          CheckLogin();
    }

    void AuthCallBack(IResult result){ 
        if(result.Error != null){
            Debug.Log(result.Error);
        }
        else{
            DealWithMenu(FB.IsLoggedIn);
        }
        
    }
    void DealWithMenu(bool IsLogged) {
        if(IsLogged){
            head.SetActive(true);
            FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayHead);
        }
        else{
            head.SetActive(false);
        }
    }

    void DisplayHead(IGraphResult result){
        if(result.Texture != null){
            Debug.Log("WTF");
            Image ProfilePic = head.GetComponent<Image>();
            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0,0,128,128), new Vector2 ());
        }
        CheckLogin();
    }

    #endregion

    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("http://cps.imis.ncku.edu.tw/"), "Check it out!",
            "lol!",
            new System.Uri("https://i.imgur.com/FA4h9AS.png"));
    }


    #region Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey! Come and play this awesome game!", title: "Reso Coder Tutorial");
    }
 

    #endregion
 
   /*/ public void GetFriendsPlayingThisGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            FriendsText.text = string.Empty;
            foreach (var dict in friendsList)
                FriendsText.text += ((Dictionary<string, object>)dict)["name"];
        });
    }*/
}