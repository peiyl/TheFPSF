using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/EnemyManager")]
public class EnemyManager : MonoBehaviour {
    public GameObject prefabEnemy;
    private GameManager mGameManager;
    private Transform mTransform;
    private int eNum = 0;

	void Start () {
        mTransform = GetComponent<Transform>();
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
        //StartCreateEnemy();
    }
    /// <summary>
    /// 生成敌人
    /// </summary>
	public void StartCreateEnemy()
    {
        InvokeRepeating("Enemy", 1.0f, 3.0f);
    }
    /// <summary>
    /// 停止生成
    /// </summary>
    public void StopCreateEnemy()
    {
        CancelInvoke();
    }
    /// <summary>
    /// 清除敌人
    /// </summary>
    public void RemoveEnemy()
    {
        Transform[] m = mTransform.GetComponentsInChildren<Transform>();
        for(int i= 1;i<m.Length;i++)
        {
            GameObject.Destroy(m[i].gameObject);
        }
        eNum = 0;
    }
    void Enemy()
    {
        //生成一个随机数来决定生成地点
        int num = Random.Range(1, 4);
        //生成位置的四元数
        Vector3 pos = new Vector3();
        switch(num)
        {
            case 1:
                pos = new Vector3(Random.Range(-9.0f,11.0f), 0, Random.Range(-9.0f,-31.0f));
                break;
            case 2:
                pos = new Vector3(Random.Range(-20.0f, -9.0f), 0, Random.Range(-30.0f,11.0f));
                break;
            case 3:
                pos = new Vector3(Random.Range(11.0f, 22.0f), 0, Random.Range(-31.0f, 11.0f));
                break;
        }
       // Debug.Log(pos);
        //实例化物体
        GameObject enemy = Instantiate(prefabEnemy, pos, Quaternion.identity);
        //生成的物体保存到父物体
        enemy.GetComponent<Transform>().SetParent(mTransform);
        eNum++;
        //Debug.Log("e" + eNum);
        //每次生成都进行系统通知
        mGameManager.SystemHints("新的敌人出现！现在敌人有 " +eNum+" 只！");
    }
    public void EnemyDid()
    {
        eNum--;
    }
}
