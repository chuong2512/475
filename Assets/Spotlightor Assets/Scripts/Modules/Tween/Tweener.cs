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

[System.Serializable]
public class Tweener
{
	public delegate void BasicEventHandler (Tweener source);

	public delegate void ValueChangedEventHandler (Tweener source,float newValue);

	public event ValueChangedEventHandler ValueChanged;
	public event BasicEventHandler Completed;

	public float from = 0;
	public float to = 1;
	public float time = 1;
	[SerializeField]
	private iTween.EaseType
		easeType = iTween.EaseType.linear;
	private float timeElapsed = 0;
	private iTween.EasingFunction easeFunction = null;

	public float Value { get { return EaseFunction (from, to, Progress); } }

	private iTween.EasingFunction EaseFunction {
		get {
			if (easeFunction == null)
				easeFunction = iTween.GetEasingFunctionByType (EaseType);
			return easeFunction;
		}
	}

	public iTween.EaseType EaseType {
		get { return easeType; }
		set {
			easeType = value;
			easeFunction = iTween.GetEasingFunctionByType (easeType);
		}
	}

	public bool IsCompleted{ get { return Progress >= 1; } }

	public float Progress { get { return Mathf.Clamp01 (time > 0 ? TimeElapsed / time : (TimeElapsed > 0 ? 1 : 0)); } }

	public float TimeElapsed {
		get { return timeElapsed;}
		set {
			bool isCompletedBeforeUpdate = this.IsCompleted;
			float oldValue = this.Value;
			timeElapsed = value;
			float newValue = this.Value;

			if (ValueChanged != null && newValue != oldValue)
				ValueChanged (this, newValue);
			if (Completed != null && IsCompleted && isCompletedBeforeUpdate == false) 
				Completed (this);
		}
	}

	public Tweener ()
	{
	}

	public Tweener (float from, float to, float time, iTween.EaseType easeType)
	{
		this.from = from;
		this.to = to;
		this.time = time;
		this.EaseType = easeType;
	}
}
