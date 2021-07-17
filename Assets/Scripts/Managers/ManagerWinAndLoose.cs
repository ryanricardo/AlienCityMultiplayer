using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerWinAndLoose : MonoBehaviour
{
    
    private bool StartChronometer;
    private float Chronometer;

    public TextMeshProUGUI TextChronometer;
    void Start()
    {
        StartChronometer = false;
        Chronometer = 6;
        StartCoroutine(ChangeScene());
    }

    
    void Update()
    {
        if(StartChronometer)
        {
            Chronometer -= Time.deltaTime;
            TextChronometer.text = "Espere " + Mathf.FloorToInt(Chronometer).ToString() + " para voltar ao menu";
        }
    }

    IEnumerator ChangeScene()
    {
        StartChronometer = true;
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

   
}
