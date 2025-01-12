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

[RequireComponent(typeof(TextMesh), typeof(TextDisplayer))]
public class KeepTextMeshWidthInThresholder : MonoBehaviour
{

	public float maxWidth = 1;
	public float defaultCharacterSize = 0.02f;
	private TextMesh textMesh;
	
	protected Font TextFont {
		get { return MyTextMesh.font;}
	}
	
	protected TextMesh MyTextMesh {
		get {
			if (textMesh == null)
				textMesh = GetComponent<TextMesh> ();
			return textMesh;
		}
	}
	
	void Start ()
	{
		UpdateCharacterSize ();
		GetComponent<TextDisplayer> ().TextUpdated += HandleTextUpdated;
	}
	
	void HandleTextUpdated (string text)
	{
		UpdateCharacterSize ();
	}

	void UpdateCharacterSize ()
	{
		string wrappedText = MyTextMesh.text;
		int charIndex = 0;
		float maxLineWidth = -1f;
		float currentLineWidth = 0.0f;
		while (charIndex < wrappedText.Length) {
			char character = wrappedText [charIndex];
			if (character == '\n' || character == '\r') {
				maxLineWidth = Mathf.Max (currentLineWidth, maxLineWidth);
				currentLineWidth = 0;
			} else {
				CharacterInfo characterInfo;
				
				float characterWidth = 0;
				if (textMesh.font.GetCharacterInfo (character, out characterInfo)) {
#if UNITY_5
					characterWidth = characterInfo.advance;
#endif
					#if !UNITY_5
					characterWidth = characterInfo.width;
					#endif
				} else
					Debug.LogWarning (string.Format ("Unrecognized character encountered: {0}({1})", character, (int)character));
				
				currentLineWidth += characterWidth;
			}
			
			charIndex++;
		}
		maxLineWidth = Mathf.Max (currentLineWidth, maxLineWidth);
		float maxWidthWithoutCharacterSize = 10 * maxWidth / defaultCharacterSize;
		
		if (maxLineWidth <= maxWidthWithoutCharacterSize)
			MyTextMesh.characterSize = defaultCharacterSize;
		else
			MyTextMesh.characterSize = defaultCharacterSize * maxWidthWithoutCharacterSize / maxLineWidth;
	}
	
	void OnDrawGizmosSelected ()
	{
		Gizmos.DrawLine (transform.position, transform.TransformPoint (Vector3.right * maxWidth));
	}
}
