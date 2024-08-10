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

public class Treasure : MonoBehaviour
{
	public Vector3 carryOffset = Vector3.up * 0.5f;
	public Vector3 dropOffset = Vector3.down * 0.5f;
	public UnityEvent onCarry;
	public UnityEvent onDrop;

	[SerializeField]
	private Tweener dropTweener = new Tweener (0, 1, 0.5f, iTween.EaseType.easeOutCubic);

	public void CarryBy (Transform carrier)
	{
		transform.SetParent (carrier, false);
		transform.localPosition = carryOffset;
		onCarry.Invoke ();
	}

	public void DropTo (Transform target)
	{
		transform.SetParent (null);
		StartCoroutine (DoDropTo (target));
	}

	private IEnumerator DoDropTo (Transform target)
	{
		onDrop.Invoke ();
		Vector3 startPos = transform.position;

		while (!dropTweener.IsCompleted) {
			yield return null;
			dropTweener.TimeElapsed += Time.deltaTime;
			transform.position = Vector3.Lerp (startPos, target.position + dropOffset, dropTweener.Value);
		}

		Destroy (gameObject);
	}
}
