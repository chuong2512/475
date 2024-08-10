/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof(DesignRater))]
public class DesignRaterEditor : Editor
{
	private DesignRater Rater{ get { return target as DesignRater; } }

	private bool displaySettings = false;

	public override void OnInspectorGUI ()
	{
		List<DesignRater.DesignRatingResult> results = Rater.results;

		if (results == null)
			results = new List<DesignRater.DesignRatingResult> ();

		if (Rater.ratings == null)
			Rater.ratings = new List<DesignRating> ();

		while (results.Count < Rater.ratings.Count)
			results.Add (new DesignRater.DesignRatingResult ());
		
		while (results.Count > Rater.ratings.Count)
			results.RemoveAt (results.Count - 1);

		for (int i = 0; i < Rater.ratings.Count; i++) {
			DesignRating rating = Rater.ratings [i];
			if (rating != null) {

				DesignRater.DesignRatingResult result = results [i];

				GUILayout.BeginHorizontal ();

				EditorGUILayout.LabelField (rating.title, GUILayout.Width (80));

				if (rating.multiChoices) {
					for (int j = 0; j < rating.choices.Count; j++) {
						bool selected = result.chosenIndexes.Contains (j);
						bool newSelected = EditorGUILayout.ToggleLeft (rating.choices [j].OptionName, selected, GUILayout.Width (80));
						if (selected != newSelected) {
							if (newSelected)
								result.chosenIndexes.Add (j);
							else
								result.chosenIndexes.Remove (j);
						}
					}


				} else {
					if (result.chosenIndexes.Count == 0)
						result.chosenIndexes.Add (0);
				
					int selected = result.chosenIndexes [0];
					selected = EditorGUILayout.Popup (selected, rating.OptionNames, GUILayout.Width (80));
					result.chosenIndexes [0] = selected;
				}

				GUILayout.EndHorizontal ();
			}
		}

		displaySettings = EditorGUILayout.BeginToggleGroup ("Display Settings", displaySettings);
		if (displaySettings)
			DrawDefaultInspector ();
		EditorGUILayout.EndToggleGroup ();

		serializedObject.ApplyModifiedProperties ();
	}
}
