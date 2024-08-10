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

public class FinishHomeSeeThrough : MonoBehaviour
{
	public LayerMask sightHitMask = -1;
	public bool logInfo = false;
	private bool visible = false;

	public bool Visible {
		get { return visible; }
		set {
			visible = value;
			if (value) {
				if (!GetComponent<ParticleSystem> ().isPlaying)
					GetComponent<ParticleSystem> ().Play ();
			} else {
				if (GetComponent<ParticleSystem> ().isEmitting)
					GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}

	void LateUpdate ()
	{
		Transform seeTarget = null;
		if (Finish.Instance.gameObject.activeSelf)
			seeTarget = Finish.Instance.transform;
		if (Home.Instance.gameObject.activeSelf)
			seeTarget = Home.Instance.transform;

		if (seeTarget != null) {
			transform.position = seeTarget.position;

			Vector3 seeTargetVector = seeTarget.position - Camera.main.transform.position;
			Ray ray = new Ray (Camera.main.transform.position, seeTargetVector.normalized);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, seeTargetVector.magnitude - 0.5f, sightHitMask)) {
				Visible = true;
				if (logInfo)
					Debug.LogFormat ("hit {0}", hit.collider);
			} else
				Visible = false;
		} else
			Visible = false;
	}
}
