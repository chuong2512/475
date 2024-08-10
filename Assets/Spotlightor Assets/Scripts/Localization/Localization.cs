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

public enum LocalizationLanguageTypes
{
	English = -1,
	Chinese = 0,
	French = 1,
	German = 2,
	Italian = 3,
	Spanish = 4,
	Japanese = 5,
	Russian = 6,
	ChsTraditional = 7,
	Portuguese = 8,
	Turkish = 9,
	Polish = 10,
	Norwegian = 11,
	Finnish = 12,
	Dutch = 13,
	Danish = 14,
}

public static class Localization
{
	public const string MessageLanguageChanged = "msg_language_changed";

	private static LocalizationLanguageTypes currentLanguage = LocalizationLanguageTypes.English;
	private static SavableInt languagePrefsSaver = new SavableInt ("loc_lang", 0);
	private static bool hasLoadedPrefs = false;

	public static List<LocalizationLanguageTypes> AvailableLanguageTypes {
		get { return LocalizationSettings.Instance.availableLanguageTypes; }
	}

	public static LocalizationLanguageTypes CurrentLanguage {
		get {
			if (!hasLoadedPrefs)
				LoadLanguagePrefs ();
			return currentLanguage;
		}
		set {
			if (LocalizationSettings.Instance.lockLanguage == false) {
				if (CurrentLanguage != value) {
					currentLanguage = value;
					Messenger.Broadcast (MessageLanguageChanged, MessengerMode.DONT_REQUIRE_LISTENER);
				}
			}
		}
	}

	public static bool HasLanguagePreference { get { return languagePrefsSaver.HasBeenSaved; } }

	static Localization ()
	{
		
	}

	private static void LoadLanguagePrefs ()
	{
		if (HasLanguagePreference)
			currentLanguage = (LocalizationLanguageTypes)(languagePrefsSaver.Value - 1);
		else if (AvailableLanguageTypes.Count > 0)
			currentLanguage = AvailableLanguageTypes [0];

		hasLoadedPrefs = true;
	}

	public static void SaveLanguagePreference ()
	{
		languagePrefsSaver.Value = (int)CurrentLanguage + 1;
	}

	public static void ClearLanguagePreference ()
	{
		languagePrefsSaver.Delete ();
	}

}
