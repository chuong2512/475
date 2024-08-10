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

public class LifeChanger : MonoBehaviour
{
	[Header ("生命值改变量，负数为伤害，正数为治疗")]
	public int deltaLife = -1;
	[OverrideLabel ("只触发一次")]
	public bool triggerOnce = true;
	public bool ignoreSameLayer = true;
	public UnityEvent onTrigger;

	private int triggerTimes = 0;

	void OnTriggerEnter (Collider other)
	{
		TryToChangeLifeFor (other);
	}

	void OnCollisionEnter (Collision collision)
	{
		TryToChangeLifeFor (collision.collider);
	}

	public void TryToChangeLifeFor (Collider targetCollider)
	{
		if (triggerOnce && triggerTimes > 0) {
			
		} else {
			Life life = null;
			if (targetCollider != null) {
				if (targetCollider.attachedRigidbody != null)
					life = targetCollider.attachedRigidbody.GetComponent<Life> ();
				else
					life = targetCollider.GetComponent<Life> ();
			}
			if (life != null) {
				bool ignoredBySameLayer = false;
				if (ignoreSameLayer)
					ignoredBySameLayer = life.gameObject.layer == this.gameObject.layer;

				if (!ignoredBySameLayer) {
					life.Current.Value += deltaLife;
					triggerTimes++;

					onTrigger.Invoke ();
				}
			}
		}
	}
}
