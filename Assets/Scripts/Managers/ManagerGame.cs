using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviourPunCallbacks
{
    public GameObject PrefabPlayer;
    public GameObject PrefabCasesAmmuniation;
    public bool       CheckVictory;
    public bool       spawnCase;
    
    void Start()
    {
        CheckVictory = false;
        spawnCase = false;
       
        if(PrefabPlayer != null)
        {
            Vector3 posicao = new Vector3(0, 5, 0);
            posicao.x = Random.Range(0, 14);
            posicao.z = Random.Range(6, -9);
            PhotonNetwork.Instantiate(PrefabPlayer.name, posicao, Quaternion.identity);
        }

        Invoke("CheckPlayers", 5);


    }


    void Update()
    {
        CheckCases();
        if(CheckVictory)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }

    void CheckPlayers()
    {
        PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();
        if(players.Length >=  2){CheckVictory = true;}
    }

    void CheckCases()
    {
        CasesAmmunation[] casesAmmunations = GameObject.FindObjectsOfType<CasesAmmunation>();

        if(!spawnCase && casesAmmunations.Length < 2)
        {
            StartCoroutine(SpawnCases());
            spawnCase = true;
        }else 
        {
            StopCoroutine(SpawnCases());
        }
    }

    IEnumerator SpawnCases()
    {
        yield return new WaitForSeconds(10);
        
        Vector3 newPosition = new Vector3(Random.Range(0, 14), 0.5f, Random.Range(6, -9));
        PhotonNetwork.Instantiate(PrefabCasesAmmuniation.name, newPosition,
        Quaternion.identity);
        spawnCase = false;
    
    }
    

}
