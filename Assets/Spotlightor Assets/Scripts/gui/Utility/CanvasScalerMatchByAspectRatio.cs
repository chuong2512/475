/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(CanvasScaler))]
public class CanvasScalerMatchByAspectRatio : MonoBehaviour
{
	public AnimationCurve matchWidthOrHeightByAspectRatio = new AnimationCurve (
		                                                        new Keyframe (9f / 16f, 0f), 
		                                                        new Keyframe (2f / 3f, 0f), 
		                                                        new Keyframe (3f / 4f, 1f)
	                                                        );

	void Awake ()
	{
		UpdateCanvasScalerSetting ();
	}

	void Update ()
	{
		UpdateCanvasScalerSetting ();
	}

	public void UpdateCanvasScalerSetting ()
	{
		float aspectRatio = (float)Screen.width / (float)Screen.height;
		float matchWidthOrHeight = matchWidthOrHeightByAspectRatio.Evaluate (aspectRatio);

		CanvasScaler scaler = GetComponent<CanvasScaler> ();
		if (scaler.matchWidthOrHeight != matchWidthOrHeight)
			scaler.matchWidthOrHeight = matchWidthOrHeight;
	}

	void Reset ()
	{
		GetComponent<CanvasScaler> ().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
	}
}
