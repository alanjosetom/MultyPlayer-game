using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerManager : MonoBehaviour
{
    GameObject controller;
    PhotonView PV;
    int kills;
    int death;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoints();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Arissa"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
    }

    // Update is called once per frame
    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();

        death++;

        Hashtable hash = new Hashtable();
        hash.Add("death", death);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
    public void GetKill()
    {
        PV.RPC(nameof(RPC_GetKill), PV.Owner);
    }
    [PunRPC]
    void RPC_GetKill()
    {
        kills++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
