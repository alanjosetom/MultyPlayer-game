using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lobbyCharacter : MonoBehaviour
{
    [HideInInspector] public string sceneM;
    // Start is called before the first frame update
    void Start()
    {
        string sceneM = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneM == "lobby")
        {

        }

    }
}
