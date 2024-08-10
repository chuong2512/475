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

public class DesignRating : ScriptableObject
{
	[System.Serializable]
	public class Choice
	{
		public string name = "Choice";
		public string shortName = "c";
		public float value = 1;

		public string OptionName {
			get {
				string optionName = name;
				if (string.IsNullOrEmpty (shortName) == false)
					optionName += "(" + shortName + ")";
				return optionName;
			}
		}
	}

	public string title = "Rating Title";
	public List<Choice> choices;
	public bool multiChoices = false;

	public string[] OptionNames {
		get {
			List<string> optionNames = new List<string> ();
			choices.ForEach (c => optionNames.Add (c.OptionName));
			return optionNames.ToArray ();
		}
	}
}
