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

public class ColorTransitionManager : ValueTransitionManager
{
	[ColorUsage (true, true, 0, 8, 0.125f, 3)]
	public Color colorIn = Color.white;
	[ColorUsage (true, true, 0, 8, 0.125f, 3)]
	public Color colorOut = new Color (1, 1, 1, 0);

	private ColorDisplayer colorDisplayer;

	public ColorDisplayer ColorDisplayer {
		get {
			if (colorDisplayer == null) {
				colorDisplayer = GetComponent<ColorDisplayer> ();
				if (colorDisplayer == null)
					colorDisplayer = gameObject.AddComponent<ColorDisplayer> ();
			}
			return colorDisplayer;
		}
	}

	protected override void OnProgressValueUpdated (float progress)
	{
		ColorDisplayer.Display (Color.Lerp (colorOut, colorIn, progress));
	}

	void Reset ()
	{
		easeIn = iTween.EaseType.linear;
		easeOut = iTween.EaseType.linear;
	}
}
