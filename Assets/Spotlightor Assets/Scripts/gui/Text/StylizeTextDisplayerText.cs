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

[RequireComponent(typeof(TextDisplayer))]
public class StylizeTextDisplayerText : MonoBehaviour
{
	public enum CaseConversionTypes
	{
		None = 0,
		ToUpper = 1,
		ToLower = 2
	}
	public CaseConversionTypes caseConverstion = CaseConversionTypes.None;
	private TextMesh myTextMesh;

	public TextMesh MyTextMesh {
		get {
			if (myTextMesh == null)
				myTextMesh = GetComponent<TextMesh> ();
			return myTextMesh;
		}
	}
	
	void Awake ()
	{
		Stylize ();
		GetComponent<TextDisplayer> ().TextUpdated += HandleTextUpdated;
	}

	void HandleTextUpdated (string text)
	{
		StylizeText (text);
	}
	
	public void Stylize ()
	{
		if (MyTextMesh)
			StylizeText (MyTextMesh.text);
	}
	
	private void StylizeText (string text)
	{
		string styledText = text;
		switch (caseConverstion) {
		case CaseConversionTypes.ToLower:
			styledText = styledText.ToLower ();
			break;
		case CaseConversionTypes.ToUpper:
			styledText = styledText.ToUpper ();
			break;
		}
		
		if (MyTextMesh)
			MyTextMesh.text = styledText;
	}
}
