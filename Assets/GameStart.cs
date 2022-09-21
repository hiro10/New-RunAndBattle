using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private GameManagerData myGameManagerData;

    // Start is called before the first frame update
    void Start()
    {
        myGameManagerData = FindObjectOfType<GameManager>().GetGameManagerData();
        Instantiate(myGameManagerData.GetCharacter(), Vector3.zero, Quaternion.identity);
    }
}

