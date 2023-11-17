using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TMP_Text userName;
    public TMP_Text killsText;
    public TMP_Text deathText;
    Player player;
    public void Initialize(Player player)
    {
        this.player = player;

        userName.text = player.NickName;
        UpdateStats();
    }
    void UpdateStats()
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();
        }
        if (player.CustomProperties.TryGetValue("death", out object death))
        {
            deathText.text = death.ToString();
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == player)
        {
            if (changedProps.ContainsKey("kills") || changedProps.ContainsKey("deaths"))
            {
                UpdateStats();
            }
        }
    }

}
