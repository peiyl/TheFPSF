using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/LifePSManager")]
public class LifePSManager : MonoBehaviour {
    public GameObject mprefab;
    private Transform mTransform;

    void Start()
    {
        mTransform = GetComponent<Transform>();
        //LifeP();
    }
    /// <summary>
    /// 生成
    /// </summary>
	public void StartCreatePrefab()
    {
        Invoke("LifeP", 1.0f);
    }
    /// <summary>
    /// 清除
    /// </summary>
    public void RemoveLP()
    {
        Transform[] m = mTransform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < m.Length; i++)
        {
            GameObject.Destroy(m[i].gameObject);
        }
    }
    void LifeP()
    {
        for(int i= 1;i<7; i++)
        {
            //生成一个随机坐标
            Vector3 pos = new Vector3(Random.Range(-20, 18), 10, Random.Range(-28, 9));
            //实例化物体
            GameObject mGameObject = Instantiate(mprefab, pos, Quaternion.identity);
            //生成的物体保存到父物体
            mGameObject.GetComponent<Transform>().SetParent(mTransform);
            //Debug.Log("shengchenglyigejijiubao");
        }
    }
}

