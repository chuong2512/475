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

public abstract class CoroutineCommandBehavior : CommandBehavior
{
	private const string ExecutingNamePostfix = " <";

	public delegate void BasicEventHandler (CoroutineCommandBehavior source);

	public event BasicEventHandler Started;
	public event BasicEventHandler Ended;

	private bool isExecuting = false;
	private float startTime = -1;

	public float TimeExecuted{ get { return isExecuting ? Time.time - StartTime : 0; } }

	public float StartTime { 
		get { return isExecuting ? startTime : -1; }
		private set { startTime = value; }
	}

	public bool IsExecuting { 
		get{ return this.isExecuting; } 
		private set {
			if (isExecuting != value) {
				this.isExecuting = value;
				#if UNITY_EDITOR
				if (this.isExecuting)
					this.name = this.name + ExecutingNamePostfix;
				else {
					int indexOfExecutingNamePostfix = this.name.IndexOf (ExecutingNamePostfix);
					if (indexOfExecutingNamePostfix >= 0)
						this.name = this.name.Substring (0, indexOfExecutingNamePostfix);
				}
				#endif
			}
		}
	}

	public override void Execute ()
	{
		if (IsExecuting == false)
			StartCoroutine ("ExecuteCoroutine");
		else
			this.LogWarning ("Command {0} is executing, cannot Execute again!", this);
	}

	public IEnumerator ExecuteCoroutine ()
	{
		if (IsExecuting == false) {
			IsExecuting = true;
			if (Started != null)
				Started (this);
			
			StartTime = Time.time;

			if (Application.isEditor)
				InvokeRepeating ("ShowTimeExcecutedOnName", 0.0f, 0.1f);

			yield return StartCoroutine ("CoroutineCommand");

			if (Application.isEditor)
				CancelInvoke ("ShowTimeExcecutedOnName");

			IsExecuting = false;
			if (Ended != null)
				Ended (this);
		} else
			this.LogWarning ("Command {0} is executing, cannot Execute again!", this);
	}

	private void ShowTimeExcecutedOnName ()
	{
		string nameWithTime = this.name;
		int markStartIndex = nameWithTime.IndexOf (ExecutingNamePostfix);
		if (markStartIndex < 0)
			markStartIndex = nameWithTime.Length;
		this.name = nameWithTime.Substring (0, markStartIndex) + ExecutingNamePostfix + string.Format ("{0:0.0}", TimeExecuted);
	}

	private void OnDisable ()
	{
		if (IsExecuting)
			Stop ();
	}

	public void Stop ()
	{
		StopCoroutine ("ExecuteCoroutine");
		StopCoroutine ("CoroutineCommand");
		if (Application.isEditor)
			CancelInvoke ("ShowTimeExcecutedOnName");
		IsExecuting = false;
	}

	protected abstract IEnumerator CoroutineCommand ();
}
