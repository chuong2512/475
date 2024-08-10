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
using System.Collections.Generic;

public class ScreenshotsLocalizedCommand : ScreenshotCommand
{
	public List<LocalizationLanguageTypes> languages = new List<LocalizationLanguageTypes> () {
		LocalizationLanguageTypes.Chinese,
		LocalizationLanguageTypes.English
	};

	protected override IEnumerator CoroutineCommand ()
	{
		yield return new WaitForSeconds (delay);

		float timeScaleBefore = Time.timeScale;
		string shotNameBefore = this.screenshotName;

		Time.timeScale = 0;
		foreach (LocalizationLanguageTypes language in languages) {
			Localization.CurrentLanguage = language;
			yield return null;

			this.screenshotName = string.Format ("{1}/{0} {1}", shotNameBefore, language.ToString ());
			this.Screenshot ();
			yield return null;
		}
		Time.timeScale = timeScaleBefore;
		this.screenshotName = shotNameBefore;
	}
}
