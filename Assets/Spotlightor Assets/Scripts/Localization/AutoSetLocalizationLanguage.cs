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

public class AutoSetLocalizationLanguage : MonoBehaviour
{
	void Start ()
	{
		if (!Localization.HasLanguagePreference) {
			LocalizationLanguageTypes autoSelectedLanguage = LocalizationLanguageTypes.English;

			#if UNITY_PS4
			UnityEngine.PS4.Utility.SystemLanguage ps4Language = UnityEngine.PS4.Utility.systemLanguage;
			if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.CHINESE_S)
				autoSelectedLanguage = LocalizationLanguageTypes.Chinese;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.CHINESE_T)
				autoSelectedLanguage = LocalizationLanguageTypes.ChsTraditional;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.FRENCH)
				autoSelectedLanguage = LocalizationLanguageTypes.French;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.GERMAN)
				autoSelectedLanguage = LocalizationLanguageTypes.German;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.ITALIAN)
				autoSelectedLanguage = LocalizationLanguageTypes.Italian;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.SPANISH)
				autoSelectedLanguage = LocalizationLanguageTypes.Spanish;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.JAPANESE)
				autoSelectedLanguage = LocalizationLanguageTypes.Japanese;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.RUSSIAN)
				autoSelectedLanguage = LocalizationLanguageTypes.Russian;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.PORTUGUESE_PT || ps4Language == UnityEngine.PS4.Utility.SystemLanguage.PORTUGUESE_BR)
				autoSelectedLanguage = LocalizationLanguageTypes.Portuguese;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.TURKISH)
				autoSelectedLanguage = LocalizationLanguageTypes.Turkish;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.POLISH)
				autoSelectedLanguage = LocalizationLanguageTypes.Polish;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.NORWEGIAN)
				autoSelectedLanguage = LocalizationLanguageTypes.Norwegian;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.FINNISH)
				autoSelectedLanguage = LocalizationLanguageTypes.Finnish;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.DUTCH)
				autoSelectedLanguage = LocalizationLanguageTypes.Dutch;
			else if (ps4Language == UnityEngine.PS4.Utility.SystemLanguage.DANISH)
				autoSelectedLanguage = LocalizationLanguageTypes.Danish;

			#else
			if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseSimplified)
				autoSelectedLanguage = LocalizationLanguageTypes.Chinese;
			else if (Application.systemLanguage == SystemLanguage.ChineseTraditional)
				autoSelectedLanguage = LocalizationLanguageTypes.ChsTraditional;
			else if (Application.systemLanguage == SystemLanguage.French)
				autoSelectedLanguage = LocalizationLanguageTypes.French;
			else if (Application.systemLanguage == SystemLanguage.German)
				autoSelectedLanguage = LocalizationLanguageTypes.German;
			else if (Application.systemLanguage == SystemLanguage.Italian)
				autoSelectedLanguage = LocalizationLanguageTypes.Italian;
			else if (Application.systemLanguage == SystemLanguage.Spanish)
				autoSelectedLanguage = LocalizationLanguageTypes.Spanish;
			else if (Application.systemLanguage == SystemLanguage.Japanese)
				autoSelectedLanguage = LocalizationLanguageTypes.Japanese;
			else if (Application.systemLanguage == SystemLanguage.Russian)
				autoSelectedLanguage = LocalizationLanguageTypes.Russian;
			else if (Application.systemLanguage == SystemLanguage.Portuguese)
				autoSelectedLanguage = LocalizationLanguageTypes.Portuguese;
			else if (Application.systemLanguage == SystemLanguage.Turkish)
				autoSelectedLanguage = LocalizationLanguageTypes.Turkish;
			else if (Application.systemLanguage == SystemLanguage.Polish)
				autoSelectedLanguage = LocalizationLanguageTypes.Polish;
			else if (Application.systemLanguage == SystemLanguage.Norwegian)
				autoSelectedLanguage = LocalizationLanguageTypes.Norwegian;
			else if (Application.systemLanguage == SystemLanguage.Finnish)
				autoSelectedLanguage = LocalizationLanguageTypes.Finnish;
			else if (Application.systemLanguage == SystemLanguage.Dutch)
				autoSelectedLanguage = LocalizationLanguageTypes.Dutch;
			else if (Application.systemLanguage == SystemLanguage.Danish)
				autoSelectedLanguage = LocalizationLanguageTypes.Danish;
			#endif

			if (Localization.AvailableLanguageTypes.Contains (autoSelectedLanguage))
				Localization.CurrentLanguage = autoSelectedLanguage;
		}
	}
}
