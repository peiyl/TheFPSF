using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/BBSManager")]
public class BBSManager : MonoBehaviour {
    public GameObject mPrefab;
    private Transform mTransform;
    private GameManager mGameManager;
    private int pF = 0;
    void Start () {
        mTransform = GetComponent<Transform>();
        mGameManager = GameObject.Find("Canvas").GetComponent<GameManager>();
    }
    /// <summary>
    /// 生成
    /// </summary>
    public void StartCreatePrefab()
    {
        InvokeRepeating("Prefab", 3.0f, 3.0f);
    }
    /// <summary>
    /// 停止生成
    /// </summary>
    public void StopCreatePrefab()
    {
        CancelInvoke();
    }
    /// <summary>
    /// 清除
    /// </summary>
    public void RemovePrefab()
    {
        Transform[] m = mTransform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < m.Length; i++)
        {
            Destroy(m[i].gameObject);
        }
    }
    void Prefab()
    {
        if(pF == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                //生成一个随机坐标
                Vector3 pos = new Vector3(Random.Range(-20, 18), 10, Random.Range(-28, 9));
                //实例化物体
                GameObject mGameObject = Instantiate(mPrefab, pos, Quaternion.identity);
                //生成的物体保存到父物体
                mGameObject.GetComponent<Transform>().SetParent(mTransform);
                Debug.Log("生成了子弹+1");
                //记录数目
                pF++;
                Debug.Log("子弹数目" + pF);
            }
            //系统提示
            mGameManager.SystemHints("新的补给生成！");
        }
    }
    public void PrefabReduce()
    {
        pF--;
        Debug.Log("被吃掉了一个，还有"+pF);
    }
}
