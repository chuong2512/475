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
public class ObjectPool<T> where T:Object
{
	private T _prefab;
	private List<T> _pool;
	protected List<T> Pool {
		get {
			if (_pool == null)
				_pool = new List<T> ();
			return _pool;
		}
	}
	
	public ObjectPool(T prefab)
	{
		_prefab = prefab;
	}
	
	public void DisposeObject (T objToDispose)
	{
		Pool.Add(objToDispose);
	}
	
	public void DisposeObjectAndDeactivate(T objToDispose)
	{
		DisposeObject(objToDispose);
		(objToDispose as Component).gameObject.SetActive(false);
	}

	public T GetInstance ()
	{
		T instance;
		if(Pool.Count > 0){
			instance = Pool[0];
			Pool.RemoveAt(0);
		}else {
			instance = GameObject.Instantiate(_prefab) as T;
		}
		return instance;
	}
	
	public T GetInstanceAndActivate()
	{
		T instance = GetInstance();
		(instance as Component).gameObject.SetActive(true);
		return instance;
	}
}

