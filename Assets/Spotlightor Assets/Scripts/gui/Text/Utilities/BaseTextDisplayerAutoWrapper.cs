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
public class BaseTextDisplayerAutoWrapper : MonoBehaviour
{

	public float maxWidth = 5;
	private TextMesh textMesh;
	
	protected TextMesh MyTextMesh {
		get {
			if (textMesh == null)
				textMesh = GetComponent<TextMesh> ();
			return textMesh;
		}
	}
	
	void Start ()
	{
		WrapText ();
		GetComponent<TextDisplayer> ().TextUpdated += HandleTextUpdated;
	}
	
	void HandleTextUpdated (string text)
	{
		WrapText ();
	}
	
	[ContextMenu("Wrap")]
	void WrapText ()
	{
		// TextMesh cannot get local bounds as MeshFilter.mesh, so we need to rotate to identity and use renderer.bounds to get the right size.
		Quaternion currentRotation = transform.rotation;
		Transform parent = transform.parent;
		Vector3 currentLocalScale = transform.localScale;
		Vector3 currentLocalPosition = transform.localPosition;

		transform.parent = null;
		transform.rotation = Quaternion.identity;

		string text = MyTextMesh.text;
		string wrappedText = "";
		int charIndex = 0;
		while (charIndex < text.Length) {
			char character = text [charIndex];
			wrappedText += character;
			if (character == '\n' || character == '\r') {

			} else {
				MyTextMesh.text = wrappedText;
				float currentLineWidth = GetComponent<Renderer>().bounds.size.x;

				if (currentLineWidth > maxWidth) 
					wrappedText = wrappedText.Insert (wrappedText.Length - 1, "\n");
			}
			
			charIndex++;
		}
		
		MyTextMesh.text = wrappedText;

		transform.parent = parent;
		transform.localScale = currentLocalScale;
		transform.rotation = currentRotation;
		transform.localPosition = currentLocalPosition;
	}

	void OnDrawGizmosSelected ()
	{
		if (enabled) {
			Vector3 lineStartPosition = Vector3.zero;
			if (MyTextMesh.anchor == TextAnchor.LowerRight || MyTextMesh.anchor == TextAnchor.MiddleRight || MyTextMesh.anchor == TextAnchor.UpperRight)
				lineStartPosition -= Vector3.right * maxWidth;
			if (MyTextMesh.anchor == TextAnchor.LowerCenter || MyTextMesh.anchor == TextAnchor.MiddleCenter || MyTextMesh.anchor == TextAnchor.UpperCenter)
				lineStartPosition -= Vector3.right * 0.5f * maxWidth;

			Gizmos.DrawLine (transform.TransformPoint (lineStartPosition), transform.TransformPoint (lineStartPosition + Vector3.right * maxWidth));
		}
	}
}
