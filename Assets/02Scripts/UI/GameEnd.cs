using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/GameEnd")]
public class GameEnd : MonoBehaviour {
    private GameManager mGameManager;
    void Start () {
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }
	void Update () {
		if(Input.GetKey(KeyCode.Return))
        {
            mGameManager.ChangeGameState(GameManager.GameState.GAME);
        }
	}
}
