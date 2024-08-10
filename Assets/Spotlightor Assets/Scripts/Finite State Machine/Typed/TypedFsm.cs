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
using System.Collections.Generic;

public abstract class TypedFsmState<T>
{
	public abstract void BeginStateOf (T owner, TypedFsmState<T> previousState);

	public abstract void EndStateOf (T owner, TypedFsmState<T> newState);

	public abstract void ExecuteStateLogicOf (T owner);
}

public interface ITypedFsmClient<T>
{
	TypedFsmState<T> CurrentState {
		get;
		set;
	}
}

public static class TypedFsm <T> where T:ITypedFsmClient<T>
{
	private static Dictionary<System.Type,  TypedFsmState<T>> stateInstances = new Dictionary<System.Type, TypedFsmState<T>> ();
	
	public static void GotoState<U> (T owner) where U: TypedFsmState<T>, new()
	{
		System.Type stateType = typeof(U);
		if (!stateInstances.ContainsKey (stateType)) {
			stateInstances [stateType] = new U ();
		}
		TypedFsmState<T> previousState = owner.CurrentState;
			
		TypedFsmState<T> newState = stateInstances [stateType];
		if (owner.CurrentState != null)
			owner.CurrentState.EndStateOf (owner, newState);
			
		owner.CurrentState = newState;
		if (newState != null)
			newState.BeginStateOf (owner, previousState);
	}
}
