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

public class PhysicalExploder : MonoBehaviour
{
	public Rigidbody[] allDebris;
	public bool enableDebrisGravity = true;
	public float explosionForce = 10;
	public Vector3 randomForce = Vector3.zero;
	public Vector3 randomTorque = Vector3.zero;
	public float explosionRadius = 10;
	public bool explodeOnAwake = false;

	void OnEnable ()
	{
		if (explodeOnAwake)
			Explode ();
	}

	public void Explode ()
	{
		ExplodeAt (transform.position);
	}

	public void ExplodeAt (Transform target)
	{
		ExplodeAt (target.position);
	}

	public void ExplodeAt (Vector3 explosionPosition)
	{
		foreach (Rigidbody partRigidbody in allDebris) {
			if (enableDebrisGravity)
				partRigidbody.useGravity = true;
			partRigidbody.isKinematic = false;

			Collider partCollider = partRigidbody.GetComponent<Collider> ();
			if (partCollider != null)
				partCollider.enabled = true;

			partRigidbody.AddExplosionForce (explosionForce, explosionPosition, explosionRadius);

			if (randomForce != Vector3.zero)
				partRigidbody.AddForce (new Vector3 (Random.Range (-1f, 1f) * randomForce.x, Random.Range (-1f, 1f) * randomForce.y, Random.Range (-1f, 1f) * randomForce.z), ForceMode.Impulse);
			if (randomTorque != Vector3.zero)
				partRigidbody.AddTorque (new Vector3 (Random.Range (-1f, 1f) * randomTorque.x, Random.Range (-1f, 1f) * randomTorque.y, Random.Range (-1f, 1f) * randomTorque.z), ForceMode.Impulse);
		}
	}

	void Reset ()
	{
		if (allDebris == null || allDebris.Length == 0)
			allDebris = GetComponentsInChildren<Rigidbody> (true);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}
