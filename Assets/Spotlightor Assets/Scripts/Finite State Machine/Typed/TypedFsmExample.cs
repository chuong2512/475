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

[RequireComponent(typeof(Renderer))]
public class TypedFsmExample : MonoBehaviour, ITypedFsmClient<TypedFsmExample>
{
	public class IdleState : TypedFsmState<TypedFsmExample>
	{
		#region implemented abstract members of TypedFsmState[TypedFsmExample]
		public override void BeginStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> previousState)
		{
			owner.GetComponent<Renderer>().material.color = Color.grey;
			owner.StartCoroutine ("DelayAndGotoAttackState");
		}

		public override void EndStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> newState)
		{
			owner.StopCoroutine ("DelayAndGotoAttackState");
		}

		public override void ExecuteStateLogicOf (TypedFsmExample owner)
		{
			
		}
		#endregion
		
	}

	public class AttackState : TypedFsmState<TypedFsmExample>
	{
		#region implemented abstract members of TypedFsmState[TypedFsmExample]
		public override void BeginStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> previousState)
		{
			owner.GetComponent<Renderer>().material.color = Color.red;
			owner.StartCoroutine ("DelayAndGotoIdleState");
		}

		public override void EndStateOf (TypedFsmExample owner, TypedFsmState<TypedFsmExample> newState)
		{
			owner.StopCoroutine ("DelayAndGotoIdleState");
		}

		public override void ExecuteStateLogicOf (TypedFsmExample owner)
		{
			owner.transform.Rotate (Vector3.up, owner.speed * Time.deltaTime);
		}
		#endregion
		
	}
	
	public float speed = 3;
	public RandomRangeFloat idleTimeRange;
	public RandomRangeFloat attackTimeRange;
	private  TypedFsmState<TypedFsmExample> currentState;

	#region ITypedFsmClient[TypedFsmExample] implementation
	public TypedFsmState<TypedFsmExample> CurrentState {
		get {
			return currentState;
		}
		set {
			currentState = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start ()
	{
		TypedFsm<TypedFsmExample>.GotoState<IdleState> (this);
	}
	
	public IEnumerator DelayAndGotoAttackState ()
	{
		yield return new WaitForSeconds(idleTimeRange.RandomValue);
		TypedFsm<TypedFsmExample>.GotoState<AttackState> (this);
	}
	
	public IEnumerator DelayAndGotoIdleState ()
	{
		yield return new WaitForSeconds(attackTimeRange.RandomValue);
		TypedFsm<TypedFsmExample>.GotoState<IdleState> (this);
	}
	
	void Update ()
	{
		CurrentState.ExecuteStateLogicOf (this);
	}
}
