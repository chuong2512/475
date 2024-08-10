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

[RequireComponent (typeof(StickyWalker))]
[RequireComponent (typeof(PlayerMotionRecorder))]
[RequireComponent (typeof(Life))]
public class Player : MonoBehaviour
{
	private static ObjectInstanceFinder<Player> instanceFinder = new ObjectInstanceFinder<Player> ("Player");

	public static Player Instance{ get { return instanceFinder.Instance; } }

	public GameObject input;

	public StickyWalker Walker{ get { return GetComponent<StickyWalker> (); } }

	public PlayerMotionRecorder MotionRecorder{ get { return GetComponent<PlayerMotionRecorder> (); } }

	public Life Life{ get { return GetComponent<Life> (); } }

	public bool Controllable {
		get { return input.activeSelf; }
		set {
			input.SetActive (value);
			Walker.ForwardStrength = 0;
		}
	}

	public void TeleportTo (Vector3 position)
	{
		GetComponent<Rigidbody> ().position = position;
		GetComponent<Rigidbody> ().rotation = Quaternion.identity;
		transform.position = position;
		transform.rotation = Quaternion.identity;

		Walker.ForwardStrength = 0;
	}

	public void Kill ()
	{
		Life.Current.Value = 0;
	}

	public void Respawn ()
	{
		Life.Current.Value = Life.Current.MaxValue;

		Treasure treasure = GetComponentInChildren<Treasure> ();
		if (treasure != null)
			Destroy (treasure.gameObject);
	}
}
