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

public abstract class LanguageChangeAwareBehavior : FunctionalMonoBehaviour
{
	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		ResponseToLanguage (Localization.CurrentLanguage);
		Messenger.AddListener (Localization.MessageLanguageChanged, OnLanguageChanged);
	}

	protected override void OnBecameUnFunctional ()
	{
		Messenger.RemoveListener (Localization.MessageLanguageChanged, OnLanguageChanged);
	}

	private void OnLanguageChanged (object data)
	{
		ResponseToLanguage (Localization.CurrentLanguage);
	}

	[ContextMenu("Response English")]
	public void ResponseToEnglish ()
	{
		ResponseToLanguage (LocalizationLanguageTypes.English);
	}

	[ContextMenu("Response Chinese")]
	public void ResponseToChinese ()
	{
		ResponseToLanguage (LocalizationLanguageTypes.Chinese);
	}

	protected abstract void ResponseToLanguage (LocalizationLanguageTypes language);
}
