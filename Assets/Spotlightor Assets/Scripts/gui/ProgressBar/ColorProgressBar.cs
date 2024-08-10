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

public class ColorProgressBar : ProgressBar
{
	public Gradient progressColor;
	public bool inactiveOnEmptyProgress = false;
	private ColorDisplayer colorDisplayer;

	private ColorDisplayer MyColorDisplayer {
		get {
			if (colorDisplayer == null) {
				colorDisplayer = GetComponent<ColorDisplayer> ();
				if (colorDisplayer == null)
					colorDisplayer = gameObject.AddComponent<ColorDisplayer> ();
			}
			return colorDisplayer;
		}
	}

	protected override void UpdateProgressDisplay (float progress)
	{
		MyColorDisplayer.Display (progressColor.Evaluate (progress));

		if (inactiveOnEmptyProgress)
			gameObject.SetActive (progress > 0);
	}
}
