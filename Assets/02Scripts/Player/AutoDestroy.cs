using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/AutoDestroy")]
public class AutoDestroy : MonoBehaviour {
    public float timer = 1.0f;
	void Start ()
    {
        Destroy(gameObject, timer);
	}
}
