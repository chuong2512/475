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

#if !UNITY_5_4_OR_NEWER
[RequireComponent(typeof(ParticleEmitter))]
#endif
public class AutoDestructParticleVFX : MonoBehaviour
{
	public float delay = 1;

	#if UNITY_5_4_OR_NEWER
	void Start ()
	{
		this.Log ("ParticleEmitter is decrepit");
	}
	#else
	IEnumerator Start ()
	{
		yield return new WaitForSeconds (delay);
		GetComponent<ParticleEmitter> ().emit = false;
		yield return new WaitForSeconds (GetComponent<ParticleEmitter> ().maxEnergy);
		Destroy (gameObject);
	}
	#endif
}
