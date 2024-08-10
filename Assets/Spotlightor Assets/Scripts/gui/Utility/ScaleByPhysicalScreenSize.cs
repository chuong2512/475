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

public class ScaleByPhysicalScreenSize : MonoBehaviour
{
	[System.Serializable]
	public class ScaleSetting
	{
		public float scale = 1;
		public float physicalScreenHeight = 10;
	}
	public ScaleSetting maxScreenSetting;
	public ScaleSetting minScreenSetting;
	public float invalidDeviceScreenDpi = 93;// ipad = 132, iphone retina 326, dell U24 = 93
	
	void Start ()
	{
		UpdateScale ();
	}

	[ContextMenu ("Update Scale")]
	public void UpdateScale ()
	{
		float screenDpi = ControlFreak2.CFScreen.dpi;
		if (screenDpi == 0)
			screenDpi = invalidDeviceScreenDpi;
		
		float physicalScreenHeightInInches = Screen.height / screenDpi;
		float t = Mathf.InverseLerp (minScreenSetting.physicalScreenHeight, maxScreenSetting.physicalScreenHeight, physicalScreenHeightInInches);
		float scale = Mathf.Lerp (minScreenSetting.scale, maxScreenSetting.scale, t);
		transform.localScale = Vector3.one * scale;
	}
}
