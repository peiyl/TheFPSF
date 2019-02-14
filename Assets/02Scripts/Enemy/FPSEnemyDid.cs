using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/FPSEnemyDid")]
public class FPSEnemyDid : MonoBehaviour
{
    private GameManager mGameManager;
    private EnemyManager mEnemyManager;
    void Start()
    {
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        mEnemyManager = GameObject.Find("Enemys").GetComponent<EnemyManager>();
    }
    /// <summary>
    /// 死亡并更新UI数据及敌人计数
    /// </summary>
    void Die()
    {
        //击杀的敌人
        mGameManager.EDUpdate();
        //将累计的敌人数减少
        mEnemyManager.EnemyDid();
        //销毁
        Destroy(gameObject);
    }
}
