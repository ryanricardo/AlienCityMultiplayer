using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Buttons : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI TextConnect;
    public TMP_InputField InputNickname;

    private bool connecting;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ConnectServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            connecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = InputNickname.text;
            TextConnect.text = "Conectando ao servidor";
            
        }else 
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        TextConnect.text = "Entrando em uma sala";
        SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        TextConnect.text = "A conexão falhou, vamos criar outra sala...";
        PhotonNetwork.CreateRoom("Lobby", new RoomOptions(){MaxPlayers = 2});
    }
    
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }

   

    public override void OnConnectedToMaster()
    {
        TextConnect.text = "Você esta conectado ao servidor! " ;
    }

    

    

  
    


}
