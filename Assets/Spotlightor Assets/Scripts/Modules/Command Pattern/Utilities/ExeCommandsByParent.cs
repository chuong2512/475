/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExeCommandsByParent : MonoBehaviour
{
	public enum ExeEventTypes
	{
		Start,
		End,
	}

	public ExeEventTypes exeEventType = ExeEventTypes.Start;

	public CoroutineCommandBehavior ParentCommand {
		get {
			if (transform.parent != null)
				return transform.parent.GetComponent<CoroutineCommandBehavior> ();
			else
				return null;
		}
	}

	void OnEnable ()
	{
		CoroutineCommandBehavior parentCommand = ParentCommand;
		if (parentCommand != null) {
			parentCommand.Started += HandleParentCommandStarted;
			parentCommand.Ended += HandleParentCommandEnd;
		}
	}

	void OnDisable ()
	{
		CoroutineCommandBehavior parentCommand = ParentCommand;
		if (parentCommand != null) {
			parentCommand.Started -= HandleParentCommandStarted;
			parentCommand.Ended -= HandleParentCommandEnd;
		}
	}

	void HandleParentCommandStarted (CoroutineCommandBehavior source)
	{
		if (exeEventType == ExeEventTypes.Start)
			ExeCommands ();
	}

	void HandleParentCommandEnd (CoroutineCommandBehavior source)
	{
		if (exeEventType == ExeEventTypes.End)
			ExeCommands ();
	}

	private void ExeCommands ()
	{
		List<CoroutineCommandBehavior> commands = new List<CoroutineCommandBehavior> (GetComponents<CoroutineCommandBehavior> ());
		commands.ForEach (c => c.Execute ());
	}
}
