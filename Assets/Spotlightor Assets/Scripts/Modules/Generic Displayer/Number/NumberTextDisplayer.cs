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

public abstract class NumberTextDisplayer : NumberDisplayer
{
	private float numberValue = 0;
	private TextDisplayer textDisplayer;

	public float NumberValue {
		get { return numberValue;}
		set {
			Display (value);
			StopAllCoroutines ();
		}
	}

	public TextDisplayer TextDisplayer {
		get {
			if (textDisplayer == null) {
				textDisplayer = GetComponent<TextDisplayer> ();
				if (textDisplayer == null)
					textDisplayer = gameObject.AddComponent<GenericTextDisplayer> ();
			}
			return textDisplayer;
		}
	}

	protected override void Display (float value)
	{
		numberValue = value;
		TextDisplayer.Text = FormatNumberValueToString (numberValue);
	}
	
	protected abstract string FormatNumberValueToString (float numberValue);
	
	public void TweenValueTo (float targetValue, float time)
	{
		StopAllCoroutines ();
		StartCoroutine (DoTweenValueTo (targetValue, time));
	}
	
	private IEnumerator DoTweenValueTo (float targetValue, float time)
	{
		float timeElapsed = 0;
		float startValue = numberValue;
		while (timeElapsed < time) {
			yield return null;
			timeElapsed += Time.deltaTime;
			float newScore = Mathf.Lerp ((float)startValue, (float)targetValue, timeElapsed / time);
			Display (newScore);
		}
	}
}
