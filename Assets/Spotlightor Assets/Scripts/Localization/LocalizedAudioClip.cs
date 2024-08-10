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

public class LocalizedAudioClip : ScriptableObject
{
	[System.Serializable]
	public class AudioClipByLanguage : SerializableEnumKeyDictionary<LocalizationLanguageTypes, AudioClip>
	{
		
	}

	[SerializeField]
	private AudioClipByLanguage clipByLanguages;

	public AudioClip CurrentLanguageClip {
		get {
			AudioClip clip = null;
			clipByLanguages.Dictionary.TryGetValue (Localization.CurrentLanguage, out clip);
			if (clip == null && clipByLanguages.values.Count > 0) {
				clipByLanguages.Dictionary.TryGetValue (LocalizationLanguageTypes.English, out clip);

				if (clip == null)
					clip = clipByLanguages.values [0];
			}
			return clip;
		}
	}

	public static implicit operator AudioClip (LocalizedAudioClip localizedAudioClip)
	{
		return localizedAudioClip != null ? localizedAudioClip.CurrentLanguageClip : null;
	}

	void Reset ()
	{
		if (clipByLanguages.keys == null || clipByLanguages.keys.Count == 0) {
			clipByLanguages.keys = new List<LocalizationLanguageTypes> (Localization.AvailableLanguageTypes);
			clipByLanguages.values = new List<AudioClip> (Localization.AvailableLanguageTypes.Count);
		}
	}
}
