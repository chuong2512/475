/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour
{
	public GameObject prefab;
	public int initialPooledCount = 5;
	[Header ("0 = No pooled count limitation")]
	public int maxPooledCount = 0;
	public bool initializeOnAwake = false;
	public bool autoActivate = true;
	[Space ()]
	public bool debug = false;

	private bool hasInitialized = false;
	private Stack<GameObject> pooledInstances;
	private List<GameObject> activeInstances;
	private int numInstantiateCalls = 0;
	private int numPoolPopCalls = 0;
	private int maxPooledCountInHistory = 0;

	private Stack<GameObject> PooledInstances {
		get {
			if (pooledInstances == null)
				pooledInstances = new Stack<GameObject> ();
			return pooledInstances;
		}
	}

	public List<GameObject> ActiveInstances {
		get {
			if (activeInstances == null)
				activeInstances = new List<GameObject> ();
			return activeInstances;
		}
	}

	public bool IsFull {
		get {
			return maxPooledCount > 0 && PooledCount >= maxPooledCount;
		}
	}

	public int PooledCount {
		get {
			return PooledInstances.Count;
		}
	}

	void Start ()
	{
		if (initializeOnAwake && !hasInitialized)
			Initialize ();
	}

	public void Initialize ()
	{
		ValidateSettings ();
		InitializePool ();

		hasInitialized = true;
	}

	private void ValidateSettings ()
	{
		initialPooledCount = Mathf.Max (0, initialPooledCount);
		maxPooledCount = maxPooledCount > 0 ? Mathf.Max (maxPooledCount, initialPooledCount) : Mathf.Max (maxPooledCount, 0);
	}

	private void InitializePool ()
	{
		if (initialPooledCount > 0) {
			for (int i = 0; i < initialPooledCount; i++) {
				GameObject instance = GameObject.Instantiate (prefab) as GameObject;
				ReturnInstance (instance);
			}
		}
	}

	public GameObject BorrowInstance ()
	{
		if (!hasInitialized)
			Initialize ();

		GameObject instance = null;
		if (PooledCount > 0) {
			instance = PooledInstances.Pop ();
			if (autoActivate)
				instance.SetActive (true);
			
			numPoolPopCalls++;
		} else {
			instance = GameObject.Instantiate (prefab) as GameObject;
			numInstantiateCalls++;
		}
		ActiveInstances.Add (instance);
		return instance;
	}

	public void ReturnInstance (GameObject instance)
	{
		if (!IsFull) {
			if (autoActivate || !hasInitialized)
				instance.SetActive (false);
			
			instance.transform.SetParent (transform, false);
			PooledInstances.Push (instance);
			ActiveInstances.Remove (instance);
			maxPooledCountInHistory = Mathf.Max (maxPooledCountInHistory, PooledCount);
		} else {
			Debug.LogWarning (string.Format ("GameObjectPool[{0}] is full({1}), no more instance can be pooled. You can increase the maxPooledCount or set it to 0.", gameObject.name, maxPooledCount));
		}
	}

	void OnGUI ()
	{
		if (debug) {
			string stats = string.Format (@"Instantiate calls:{0}
Pop calls:{1}
Pooled count:{2}/{3}", numInstantiateCalls, numPoolPopCalls, PooledCount, activeInstances.Count + PooledCount);
			GUI.Box (new Rect (300, 100, 300, 150), stats);
		}
	}
}
