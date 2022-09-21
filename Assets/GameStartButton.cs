using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void OnGameStart()
    {
        //　MyGameManagerDataに保存されている次のシーンに移動する
        sceneTransition.GameStart();
    }
}
