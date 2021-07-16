using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class ManagerLobby : MonoBehaviourPunCallbacks
{

    private bool spawn;
    

    void Start()
    {
        spawn = false;

    }

    
    
    void Update()
    {
        if(!spawn && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            StartCoroutine(StartGame());
            spawn  = true;
        }
    }

   

    


    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("Game");
        
    }

   
    
    
  

    
}
