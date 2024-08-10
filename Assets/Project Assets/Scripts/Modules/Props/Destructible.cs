/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Life))]
public class Destructible: MonoBehaviour
{
	public Transform destructFx;

	private Vector3 lastImpulseForce = Vector3.up;

	public Life Life{ get { return GetComponent<Life> (); } }

	public bool destroyOnDestruct = true;

	void OnEnable ()
	{
		if (Life != null)
			Life.Current.MinValueReached += HandleLifeEmpty;
	}

	void OnDisable ()
	{
		if (Life != null)
			Life.Current.MinValueReached -= HandleLifeEmpty;
	}

	void HandleLifeEmpty (RangeInt source)
	{
		Destruct ();
	}

	public void DamageByImpulseForce (int damage, Vector3 impulseForce)
	{
		lastImpulseForce = impulseForce;
		if (Life != null)
			Life.Current.Value -= damage;
	}

	public void Destruct ()
	{
		if (destroyOnDestruct)
			Destroy (gameObject);
			
		destructFx.rotation = Quaternion.LookRotation (-lastImpulseForce);
	}
}
