/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomEditor (typeof(LocalizationTextsAsset))]
public class LocalizationTextsAssetEditor : Editor
{
	public bool readDataCanCreateKeys = false;
	public int fillCopySourceIndex = 0;
	public int fillCount = 10;
	public string fillEntriesPrefix = "";
	public int fillDigitStartIndex = 0;
	public int fillDigitsCount = 2;
	public LocalizationTextsAsset copySource;
	public string keyFromCopyFromat = "{0}";

	private LocalizationTextsAsset Target {
		get { return target as LocalizationTextsAsset; }
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		if (Target != null) {
			if (GUILayout.Button ("Sort by Key"))
				SortByKey ();
			if (GUILayout.Button ("Log for Excel"))
				LogForExcel ();
			if (Target.textAsset != null) {
				readDataCanCreateKeys = EditorGUILayout.Toggle ("导入数据可以新建key", readDataCanCreateKeys);
				if (GUILayout.Button ("Read from TextAsset (Excel save as Unicode txt)"))
					ReadFromTextAsset ();
			}
			if (GUILayout.Button ("Validate"))
				ValidateByLocalizationSettings ();

			EditorGUILayout.Space ();
			GUILayout.Label ("Fill Entries");
			fillCopySourceIndex = EditorGUILayout.IntField ("Copy Source Index", fillCopySourceIndex);
			fillCount = EditorGUILayout.IntField ("Fill Count", fillCount);
			fillEntriesPrefix = EditorGUILayout.TextField ("Key Prefix", fillEntriesPrefix);
			fillDigitStartIndex = EditorGUILayout.IntField ("Key Digit Start Index ", fillDigitStartIndex);
			fillDigitsCount = EditorGUILayout.IntField ("Key Digits Count", fillDigitsCount);

			if (GUILayout.Button (string.Format ("Fill {0} Entries", fillCount)))
				FillEntries ();

			EditorGUILayout.Space ();
			GUILayout.Label ("Copy from Other Asset");
			copySource = EditorGUILayout.ObjectField ("Copy Source", copySource, typeof(LocalizationTextsAsset), false) as LocalizationTextsAsset;
			keyFromCopyFromat = EditorGUILayout.TextField ("Key Format", keyFromCopyFromat);
			if (copySource != null && GUILayout.Button ("Copy"))
				CopyFromSourceAsset ();

			EditorGUILayout.Space ();
			GUILayout.Label ("Stats");

			Dictionary<LocalizationLanguageTypes, int> charCountByLanguageTypes = new Dictionary<LocalizationLanguageTypes, int> ();
			foreach (LocalizationText t in Target.texts) {
				foreach (LocalizationText.TextByLanguage tbl in t.textsByLanguages) {
					if (charCountByLanguageTypes.ContainsKey (tbl.language) == false)
						charCountByLanguageTypes [tbl.language] = 0;

					if (tbl.language == LocalizationLanguageTypes.Chinese)
						charCountByLanguageTypes [tbl.language] += tbl.text.Length;
					else
						charCountByLanguageTypes [tbl.language] += tbl.text.Split (new char[]{ ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
				}
			}
			foreach (KeyValuePair<LocalizationLanguageTypes, int> kvp in charCountByLanguageTypes)
				GUILayout.Label (string.Format ("{0} char count: {1}", kvp.Key.ToString (), kvp.Value));
		}
	}

	private void LogForExcel ()
	{
		string logMsg = "";

		logMsg += "\"ID\"";
		LocalizationText firstText = Target.GetLocalizationTextByIndex (0);
		List<LocalizationText.TextByLanguage> firstTexts = new List<LocalizationText.TextByLanguage> (firstText.textsByLanguages);
		firstTexts.Sort ((x, y) => ((int)x.language).CompareTo ((int)y.language));
		for (int j = 0; j < firstTexts.Count; j++)
			logMsg += string.Format ("\t\"{0}\"", firstTexts [j].language);
		logMsg += "\n";

		for (int i = 0; i < Target.TextsCount; i++) {
			LocalizationText localizationText = Target.GetLocalizationTextByIndex (i);
			logMsg += string.Format ("\"{0}\"", localizationText.key);
			List<LocalizationText.TextByLanguage> languageTexts = new List<LocalizationText.TextByLanguage> (localizationText.textsByLanguages);
			languageTexts.Sort ((x, y) => ((int)x.language).CompareTo ((int)y.language));
			for (int j = 0; j < languageTexts.Count; j++)
				logMsg += string.Format ("\t\"{0}\"", languageTexts [j].text);
			logMsg += "\n";
		}
		Debug.Log (logMsg);
	}

	private void ReadFromTextAsset ()
	{
		Undo.RecordObject (Target, "Read from TextAsset");

		string text = Target.textAsset.text;
		string lineSpliter = text.Contains ("\r\n") ? "\r\n" : "\n";
		string[] lines = text.Split (new string[]{ lineSpliter }, StringSplitOptions.RemoveEmptyEntries);

		List<string> columnNames = new List<string> (lines [0].Split (new char[]{ '\t' }, StringSplitOptions.None));
		int keyColumnIndex = columnNames.FindIndex (x => x.ToLower () == "id");

		Dictionary<LocalizationLanguageTypes, int> columnIndexByLanguages = new Dictionary<LocalizationLanguageTypes, int> ();
		foreach (LocalizationLanguageTypes language in Localization.AvailableLanguageTypes) {
			int columnIndex = columnNames.FindIndex (x => x.ToLower () == language.ToString ().ToLower ());
			if (columnIndex >= 0)
				columnIndexByLanguages [language] = columnIndex;
		}

		List<string> updateMsgs = new List<string> ();
		for (int i = 1; i < lines.Length; i++) {
			string[] datas = lines [i].Split (new char[]{ '\t' }, StringSplitOptions.None);
			string key = keyColumnIndex < datas.Length ? datas [keyColumnIndex] : null;
			LocalizationText localizationText = Target.GetLocalizationTextByKey (key);

			if (localizationText == null && readDataCanCreateKeys && string.IsNullOrEmpty (key) == false) {
				localizationText = new LocalizationText ();
				localizationText.key = key;
				Target.texts.Add (localizationText);
			}

			if (localizationText != null) {
				foreach (KeyValuePair<LocalizationLanguageTypes, int> pair in columnIndexByLanguages) {
					LocalizationLanguageTypes language = pair.Key;
					int columnIndex = pair.Value;
					string localizedText = datas [columnIndex];

					localizedText = RemoveEndReturns (localizedText);
					localizedText = RemoveDoubleQuotes (localizedText);

					string oldText = localizationText.GetLocalizedTextByLanguage (language);
					if (oldText != localizedText && !string.IsNullOrEmpty (localizedText)) {
						localizationText.SetLocalizedTextByLanguage (language, localizedText);
						updateMsgs.Add (string.Format ("<color={4}>{0}[{1}]\tupdated from |{2}| to |{3}|</color>", key, language, oldText, localizedText, GetLogFormatColorForLanguage (language)));
					}
				}
			}
		}

		updateMsgs.Sort ();
		updateMsgs.ForEach (msg => Debug.Log (msg));

		EditorUtility.SetDirty (target);
		AssetDatabase.SaveAssets ();
	}

	private string GetLogFormatColorForLanguage (LocalizationLanguageTypes language)
	{
		string color = "";
		switch (language) {
		case LocalizationLanguageTypes.Chinese:
			color = "red";
			break;
		case LocalizationLanguageTypes.English:
			color = "blue";
			break;
		case LocalizationLanguageTypes.French:
			color = "yellow";
			break;
		case LocalizationLanguageTypes.German:
			color = "silver";
			break;
		case LocalizationLanguageTypes.Italian:
			color = "cyan";
			break;
		case LocalizationLanguageTypes.Japanese:
			color = "lime";
			break;
		case LocalizationLanguageTypes.Russian:
			color = "lightblue";
			break;
		case LocalizationLanguageTypes.Spanish:
			color = "green";
			break;
		case LocalizationLanguageTypes.ChsTraditional:
			color = "maroon";
			break;
		case LocalizationLanguageTypes.Portuguese:
			color = "purple";
			break;
		case LocalizationLanguageTypes.Turkish:
			color = "brown";
			break;
		case LocalizationLanguageTypes.Polish:
			color = "grey";
			break;
		case LocalizationLanguageTypes.Norwegian:
			color = "navy";
			break;
		case LocalizationLanguageTypes.Finnish:
			color = "magenta";
			break;
		case LocalizationLanguageTypes.Dutch:
			color = "olive";
			break;
		case LocalizationLanguageTypes.Danish:
			color = "orange";
			break;
		default:
			color = "white";
			break;
		}
		return color;
	}

	private static string RemoveEndReturns (string text)
	{
		while (text.Length > 0 && text [text.Length - 1] == '\r') {
			text = text.Substring (0, text.Length - 1);
			Debug.Log (string.Format ("Remove end return from: {0}", text));
		}

		return text;
	}

	private static string RemoveDoubleQuotes (string text)
	{
		if (text.Length >= 2 && text [0] == '\"' && text [text.Length - 1] == '\"')
			text = text.Substring (1, text.Length - 2);

		text = text.Replace ("\"\"", "\"");
		return text;
	}

	private void SortByKey ()
	{
		List<LocalizationText> newTexts = new List<LocalizationText> (Target.texts);
		newTexts.Sort ((x, y) => x.key.CompareTo (y.key));
		Undo.RecordObject (Target, "Sort texts by key");
		Target.texts = newTexts;

		EditorUtility.SetDirty (target);
		AssetDatabase.SaveAssets ();
	}

	private void ValidateByLocalizationSettings ()
	{
		bool changed = false;
		foreach (LocalizationText localizationText in Target.texts) {
			List<LocalizationText.TextByLanguage> updatedTextsByLanguages = new List<LocalizationText.TextByLanguage> (localizationText.textsByLanguages);
			int i = 0;
			while (i < updatedTextsByLanguages.Count) {
				LocalizationText.TextByLanguage text = updatedTextsByLanguages [i];
				if (Localization.AvailableLanguageTypes.IndexOf (text.language) == -1) {
					updatedTextsByLanguages.RemoveAt (i);
					Debug.Log (string.Format ("{0} - {1} text node removed.", localizationText.key, text.language));
					changed = true;
				} else
					i++;
			}
		
			foreach (LocalizationLanguageTypes language in Localization.AvailableLanguageTypes) {
				if (updatedTextsByLanguages.Find (element => element.language == language) == null) {
					LocalizationText.TextByLanguage newText = new LocalizationText.TextByLanguage ();
					newText.language = language;

					newText.text = localizationText.GetLocalizedTextByLanguage (LocalizationLanguageTypes.English);
					if (newText.text == "")
						newText.text = localizationText.GetLocalizedTextByLanguage (LocalizationLanguageTypes.Chinese);
					if (newText.text == "")
						newText.text = localizationText.key;
				
					updatedTextsByLanguages.Add (newText);
					Debug.Log (string.Format ("{0} add {1} text node.", localizationText.key, language));
					changed = true;
				}
			}
		
			localizationText.textsByLanguages = updatedTextsByLanguages;
		}
		if (changed) {
			EditorUtility.SetDirty (target);
			AssetDatabase.SaveAssets ();
		}
	}

	private void FillEntries ()
	{
		if (fillCopySourceIndex < Target.texts.Count) {
			List<LocalizationText> updatedTexts = new List<LocalizationText> (Target.texts);
			LocalizationText copySourceText = Target.texts [fillCopySourceIndex];

			string keyFormat = fillEntriesPrefix + "{0:";
			for (int i = 0; i < fillDigitsCount; i++)
				keyFormat += "0";
			keyFormat += "}";

			for (int i = fillDigitStartIndex; i < fillCount + fillDigitStartIndex; i++) {
				LocalizationText text = new LocalizationText ();
				text.key = string.Format (keyFormat, i);
				text.textsByLanguages = copySourceText.textsByLanguages;

				updatedTexts.Add (text);
			}

			Target.texts = updatedTexts;

			EditorUtility.SetDirty (target);
			AssetDatabase.SaveAssets ();
		} else
			this.LogWarning ("Copy source out of index!");
	}

	private void CopyFromSourceAsset ()
	{
		List<LocalizationText> texts = copySource.texts;
		texts.ForEach (t => t.key = string.Format (keyFromCopyFromat, t.key));
		Undo.RecordObject (Target, "Copy from " + copySource.name);
		Target.texts.AddRange (texts);
	}

}
