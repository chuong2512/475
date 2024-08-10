/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(TextDisplayer))]
public class TypeWriterTextDisplayer : MonoBehaviour
{
	public delegate void SimpleEventHandler (TypeWriterTextDisplayer displayer);

	public event SimpleEventHandler TypewritingStarted;
	public event SimpleEventHandler TypewritingCompleted;

	public float charsPerSecond = 30;
	public UnityEvent typewritingStarted;
	public UnityEvent typewritingCompleted;
	private TextDisplayer textDispalyer;
	private string typewrtingText = "";

	public bool IsTypewriting{ get; private set; }

	private string Text {
		set {
			if (textDispalyer == null) {
				textDispalyer = GetComponent<TextDisplayer> ();
				if (textDispalyer == null)
					textDispalyer = gameObject.AddComponent<GenericTextDisplayer> ();
			}
			textDispalyer.Text = value;
		}
	}

	public void TypewriteText (string text)
	{
		typewrtingText = text;
		StopCoroutine ("TypeWriteAllCharacters");
		OnStarted ();

		if (gameObject.activeInHierarchy && this.enabled)
			StartCoroutine ("TypeWriteAllCharacters");
		else
			CompleteTypewriting ();
	}

	public void CompleteTypewriting ()
	{
		StopCoroutine ("TypeWriteAllCharacters");
		Text = typewrtingText;

		OnCompleted ();
	}
	
	private IEnumerator TypeWriteAllCharacters ()
	{
		Text = "";
		string typeWriterText = "";
		for (int i = 0; i < typewrtingText.Length; i++) {
			yield return new WaitForSeconds (1f / charsPerSecond);
			typeWriterText += typewrtingText [i];
			Text = typeWriterText;
		}
		OnCompleted ();
	}

	private void OnStarted ()
	{
		IsTypewriting = true;

		typewritingStarted.Invoke ();
		if (TypewritingStarted != null)
			TypewritingStarted (this);
	}

	private void OnCompleted ()
	{
		IsTypewriting = false;

		typewritingCompleted.Invoke ();
		if (TypewritingCompleted != null)
			TypewritingCompleted (this);
	}
}
