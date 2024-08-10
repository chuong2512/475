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

public class MoveTransitionManager : ValueTransitionManager
{
	public Vector3 posIn = Vector3.one;
	public Vector3 posOut = Vector3.zero;
	public bool isLocal = true;
	private RigidbodyMover mover;

	private RigidbodyMover Mover {
		get {
			if (mover == null) {
				mover = GetComponent<RigidbodyMover> ();
				if (mover == null)
					mover = gameObject.AddComponent<RigidbodyMover> ();
			}
			return mover;
		}
	}

	public virtual Vector3 PositionOut { get { return posOut; } }

	protected override void OnProgressValueUpdated (float progress)
	{
		Vector3 pos = MathAddons.LerpUncapped (PositionOut, posIn, progress);
		if (isLocal && transform.parent != null)
			pos = transform.parent.TransformPoint (pos);

		if (GetComponent<Rigidbody> () != null)
			Mover.MovePosition (pos);
		else
			transform.position = pos;
	}

	void Reset ()
	{
		posIn = posOut = transform.localPosition;
	}

	void OnDrawGizmosSelected ()
	{
		Bounds bounds = new Bounds (transform.position, Vector3.one);

		Collider collider = GetComponent<Collider> ();
		if (collider != null)
			bounds = collider.bounds;
		else {
			Renderer rd = GetComponentInChildren<Renderer> ();
			if (rd != null)
				bounds = rd.bounds;
		}

		Vector3 currentLocalPos = transform.localPosition;
		Gizmos.color = new Color (0, 0, 1, 0.3f);
		Vector3 posInBoundsOffset = posIn - currentLocalPos;
		if (transform.parent != null)
			posInBoundsOffset = transform.parent.TransformVector (posInBoundsOffset);
		Gizmos.DrawWireCube (bounds.center + posInBoundsOffset, bounds.size);

		Gizmos.color = new Color (1, 0, 0, 0.3f);
		Vector3 posOutBoundsOffset = posOut - currentLocalPos;
		if (transform.parent != null)
			posOutBoundsOffset = transform.parent.TransformVector (posOutBoundsOffset);
		Gizmos.DrawWireCube (bounds.center + posOutBoundsOffset, bounds.size);
	}
}
