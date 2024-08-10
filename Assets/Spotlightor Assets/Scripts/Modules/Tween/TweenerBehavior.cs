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

public class TweenerBehavior : MonoBehaviour
{
	[System.Serializable]
	public class ValueChangedEvent : UnityEvent<float>
	{
	}
	public bool playOnAwake = false;
	public Tweener tweener;
	public ValueChangedEvent valueChanged;
	public UnityEvent started;
	public UnityEvent completed;

	public float Value { get { return this.tweener.Value; } }

	void Awake ()
	{
		if (tweener == null)
			tweener = new Tweener ();

		tweener.ValueChanged += HandleTweenerValueChanged;
		tweener.Completed += HandleTweenerCompleted;
	}

	void Start ()
	{
		if (playOnAwake)
			Play ();
	}

	void HandleTweenerValueChanged (Tweener source, float newValue)
	{
		if (valueChanged != null)
			valueChanged.Invoke (newValue);
	}
	
	void HandleTweenerCompleted (Tweener source)
	{
		if (completed != null)
			completed.Invoke ();
	}

	public void Play ()
	{
		Stop ();
		StartCoroutine ("Tween");
	}

	public void Stop ()
	{
		StopCoroutine ("Tween");
	}

	private IEnumerator Tween ()
	{
		tweener.TimeElapsed = 0;

		if (started != null)
			started.Invoke ();

		while (!tweener.IsCompleted) {
			yield return null;
			tweener.TimeElapsed += Time.deltaTime;
		}
	}
}
