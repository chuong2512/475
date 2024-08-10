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

[RequireComponent (typeof(Light))]
public class LightTransitionManager : ValueTransitionManager
{
	public float inIntensity = 1;
	public float outIntensity = 0;
	public bool autoEnableLight = true;

	protected override void OnProgressValueUpdated (float progress)
	{
		Light myLight = GetComponent<Light> ();

		myLight.intensity = Mathf.LerpUnclamped (outIntensity, inIntensity, progress);

		if (autoEnableLight)
			myLight.enabled = myLight.intensity > 0;
	}

	public override Object[] GetTransitionModifiedObjects ()
	{
		return new Object[]{ this, GetComponent<Light> () };
	}

	public override void SetAsInState ()
	{
		inIntensity = GetComponent<Light> ().intensity;
	}

	public override void SetAsOutState ()
	{
		outIntensity = GetComponent<Light> ().intensity;
	}

	void Reset ()
	{
		Light myLight = GetComponent<Light> ();
		if (myLight != null)
			inIntensity = myLight.intensity;
	}
}
