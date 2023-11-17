using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class loadCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] charaterCollection;
    public Transform spawnPoint;
    public TMP_Text label;
    public GameObject objParent;
    void Start()
    {
        int character = PlayerPrefs.GetInt("characterID");
        Debug.Log(character);
        GameObject prefab = charaterCollection[character];
        Debug.Log(prefab.name);
        GameObject clone = PhotonNetwork.Instantiate(prefab.name, spawnPoint.position, Quaternion.identity);
        clone.transform.position = objParent.transform.position;
        // GameObject cam = GameObject.FindGameObjectWithTag("tag");
        // label.text = prefab.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
