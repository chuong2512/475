/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CustomEditor (typeof(LocalizedTextDisplayerText))]
public class LocalizedTextDisplayerTextEditor : Editor
{
	private LocalizedTextDisplayerText Target {
		get { return target as LocalizedTextDisplayerText; }
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (Target != null && Target.text.textsAsset != null && Target.text.textsAsset.GetLocalizationTextByKey (Target.text.key) != null) {
			int displayedLanguageButtons = 0;
			foreach (LocalizationLanguageTypes language in System.Enum.GetValues(typeof(LocalizationLanguageTypes))) {
				LocalizationText localizationText = Target.text.textsAsset.GetLocalizationTextByKey (Target.text.key);
				bool hasTextOfLanguage = localizationText.FindTextByLanguage (language) != null;
				if (hasTextOfLanguage) {
					displayedLanguageButtons++;
					if (displayedLanguageButtons == 1) {
						GUILayout.BeginHorizontal (GUILayout.Width(30));
						GUILayout.Label ("Display");
					}
					if (GUILayout.Button (string.Format ("{0}", language.ToString ()))) {
						Component textDisplayComponent = null;
						textDisplayComponent = Target.GetComponent<Text> ();
						if (textDisplayComponent == null)
							textDisplayComponent = Target.GetComponent<TextMesh> ();

						if (textDisplayComponent != null)
							Undo.RecordObject (textDisplayComponent, "Display Localized Text");
						
						Target.DisplayLocalizedTextByLanguage (language);
					}
					if (displayedLanguageButtons == 5) {
						GUILayout.EndHorizontal ();
						displayedLanguageButtons = 0;
					}
				}
			}
			if (displayedLanguageButtons > 0)
				GUILayout.EndHorizontal ();
		}
	}
}
