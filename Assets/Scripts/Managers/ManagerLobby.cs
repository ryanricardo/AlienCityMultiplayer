using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class ManagerLobby : MonoBehaviourPunCallbacks, IPunObservable
{

    private bool spawn;
    private bool StartChronometer;
    private float Chronometer;
    public TextMeshProUGUI TextChronometer;
    public TextMeshProUGUI TextCountPlayers;
    

    void Start()
    {
        Chronometer = 6;
        spawn = false;
        
    }

    
    
    void Update()
    {
        TextCountPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/4";
        if(!spawn && PhotonNetwork.CurrentRoom.PlayerCount >= 2 && PhotonNetwork.CurrentRoom.PlayerCount <= 4)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            StartCoroutine(StartGame());
            spawn  = true;
            
        }

        if(StartChronometer && Chronometer <= 6)
        {
            Chronometer -= Time.deltaTime;
            TextChronometer.text = "Aguarde " + Mathf.FloorToInt(Chronometer).ToString() + " Para iniciar a partida";
        }
    }
    IEnumerator StartGame()
    {
        StartChronometer = true;
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("Game"); 
    }


   
    [PunRPC]


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(Chronometer);
            stream.SendNext(TextChronometer.text);
        }else 
        {
            Chronometer = (float) stream.ReceiveNext();
            TextChronometer.text = (string) stream.ReceiveNext();
        }
    }
    
  

    
}
