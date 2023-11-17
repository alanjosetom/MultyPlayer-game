using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class characterSelection : MonoBehaviourPunCallbacks
{
    public GameObject[] characters;
    public int characterID = 0;
    // Start is called before the first frame update
    public void NextCharacter()
    {
        characters[characterID].SetActive(false);
        characterID = (characterID + 1) % characters.Length;
        characters[characterID].SetActive(true);
    }
    public void PreviousCharacter()
    {
        characters[characterID].SetActive(false);
        characterID--;
        if (characterID < 0)
        {
            characterID += characters.Length;
        }
        characters[characterID].SetActive(true);
    }
    // public void StartGame()
    // {
    //     PlayerPrefs.SetInt("characterID", characterID);
    //     SceneManager.LoadScene(3, LoadSceneMode.Single);
    // }
    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetInt("characterID", characterID);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    // public void JoinRoom()
    // {
    //     PhotonNetwork.JoinRoom("test");
    // }
    // public override void OnJoinedRoom()
    // {
    //     PhotonNetwork.LoadLevel("Select Character");
    // }
}
