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

public class MaterialPropertyAnimator : MonoBehaviour
{
	public int materialIndex = 0;
	public bool tweenSharedMaterial = false;
	public string propertyName = "_Shininess";
	public AnimationCurve valueByTime = new AnimationCurve (new Keyframe (0, 0), new Keyframe (3, 1));
	public bool ignoreTimeScale = false;
	
	public Material TargetMaterial {
		get {
			if (tweenSharedMaterial) {
				return GetComponent<Renderer>().sharedMaterials [materialIndex];
			} else {
				return GetComponent<Renderer>().materials [materialIndex];
			} 
		}
	}
	// Update is called once per frame
	void Update ()
	{
		float currentTime = ignoreTimeScale ? Time.timeSinceLevelLoad : Time.time;
		TargetMaterial.SetFloat (propertyName, valueByTime.Evaluate (currentTime));
	}
}
