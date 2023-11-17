using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class PlayerName : MonoBehaviour
{
    [SerializeField] TMP_InputField userName;
    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            userName.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username"); ;
        }
        else
        {
            userName.text = "Player " + Random.Range(0, 1000).ToString("0000");
            UserNameChanged();
        }
    }
    public void UserNameChanged()
    {
        PhotonNetwork.NickName = userName.text;
        PlayerPrefs.SetString("username", userName.text);
    }
}
