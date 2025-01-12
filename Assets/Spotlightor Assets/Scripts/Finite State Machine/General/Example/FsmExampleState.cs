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

public abstract class FsmExampleState : MonoBehaviour, IFsmState<FsmExample, FsmExample.StateTypes>
{
	public Color tintColor = Color.green;
	private FsmExample owner;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	public abstract void Jump ();
	
	#region IFsmState[FsmExample,FsmExample.StateTypes] implementation
	public FsmExample Owner {
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}

	public abstract FsmExample.StateTypes StateId {
		get ;
	}

	public virtual void BeginState (FsmExample.StateTypes previousState)
	{
		Owner.GetComponent<Renderer>().material.color = tintColor;
		enabled = true;
	}

	public virtual void EndState (FsmExample.StateTypes newState)
	{
		enabled = false;
	}
	#endregion
	
	void OnGUI ()
	{
		Vector2 screenPos = Camera.main.WorldToScreenPoint (Owner.transform.position);
		GUI.Box (new Rect (screenPos.x - 100, screenPos.y - 50, 200, 100), string.Format ("State: {0}\nPress space bar.", StateId.ToString ()));
	}
}
