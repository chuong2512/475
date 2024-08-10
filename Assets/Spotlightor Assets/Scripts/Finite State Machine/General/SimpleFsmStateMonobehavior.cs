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

/// <summary>
/// T: State machine owner. U: State types num
/// </summary>
public abstract class SimpleFsmStateMonobehavior<T,U> : MonoBehaviour, IFsmState<T,U> where T : class
{
	private T owner;

	#region IFsmState[T,U] implementation

	public abstract U StateId {
		get ;
	}

	public virtual void BeginState (U previousState)
	{
		enabled = true;
	}

	public virtual void EndState (U newState)
	{
		enabled = false;
	}

	public T Owner {
		get {
			return owner;
		}
		set {
			owner = value;
		}
	}

	#endregion

	void Reset ()
	{
		enabled = false;
	}
}
