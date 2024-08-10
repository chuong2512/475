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
using System;

[System.Serializable]
public class LocalizationText
{
	[System.Serializable]
	public class TextByLanguage
	{
		public LocalizationLanguageTypes language;
		public string text;

		public TextByLanguage ()
		{
			
		}

		public TextByLanguage (LocalizationLanguageTypes language, string text)
		{
			this.language = language;
			this.text = text;
		}
	}

	public string key = "";
	public List<TextByLanguage> textsByLanguages = new List<TextByLanguage> ();

	public string GetLocalizedTextByLanguage (LocalizationLanguageTypes language)
	{
		TextByLanguage textByLanguage = FindTextByLanguage (language);
		if (textByLanguage != null)
			return textByLanguage.text;
		else
			return "";
	}

	public void SetLocalizedTextByLanguage (LocalizationLanguageTypes language, string text)
	{
		TextByLanguage textByLanguage = FindTextByLanguage (language);
		if (textByLanguage != null)
			textByLanguage.text = text;
		else
			this.textsByLanguages.Add (new TextByLanguage (language, text));
	}

	public TextByLanguage FindTextByLanguage (LocalizationLanguageTypes language)
	{
		return textsByLanguages.Find (t => t.language == language);
	}

	public string GetAllTexts ()
	{
		string allTexts = "";
		foreach (TextByLanguage textByLanguage in textsByLanguages)
			allTexts += textByLanguage.text;
		return allTexts;
	}

	public override string ToString ()
	{
		return GetLocalizedTextByLanguage (Localization.CurrentLanguage);
	}

	public static implicit operator string (LocalizationText t)
	{
		return t.ToString ();
	}
}

public class LocalizationTextsAsset : ScriptableObject
{
	[System.Serializable]
	public class FontOfLanguage
	{
		public LocalizationLanguageTypes language;
		public Font[] fonts;
	}

	public Font[] fonts;
	public FontOfLanguage[] fontOfLanguages;
	public string additionalChars;
	public TextAsset textAsset;
	public List<LocalizationText> texts;

	public int TextsCount {
		get { return texts != null ? texts.Count : 0; }
	}

	public string GetLocalizedTextStringByIndex (int index)
	{
		LocalizationText text = GetLocalizationTextByIndex (index);
		return text == null ? "NULL" : text;
	}

	public LocalizationText GetLocalizationTextByIndex (int index)
	{
		if (texts != null && texts.Count > 0 && index >= 0 && index < texts.Count) {
			return texts [index];
		}
		return null;
	}

	public string GetLocalizedTextStringByKey (string key)
	{
		LocalizationText text = GetLocalizationTextByKey (key);
		return text == null ? "NULL" : text;
	}

	public LocalizationText GetLocalizationTextByKey (string key)
	{
		if (string.IsNullOrEmpty (key))
			return null;
		else
			return texts.Find (element => element.key == key);
	}

	public string GetAllTextsByLanguage (LocalizationLanguageTypes language)
	{
		string sum = additionalChars;
		foreach (LocalizationText lt in texts) {
			string text = lt.GetLocalizedTextByLanguage (language);
			for (int i = 0; i < text.Length; i++) {
				if (sum.IndexOf (text [i]) == -1)
					sum += text [i];
			}
		}
		return sum;
	}

	public string GetAllTexts ()
	{
		string sum = additionalChars;
		foreach (LocalizationText lt in texts) {
			string text = lt.GetAllTexts ();
			for (int i = 0; i < text.Length; i++) {
				if (sum.IndexOf (text [i]) == -1)
					sum += text [i];
			}
		}
		return sum;
	}
}
