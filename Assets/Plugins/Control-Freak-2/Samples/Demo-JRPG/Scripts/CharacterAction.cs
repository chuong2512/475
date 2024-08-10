/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

using UnityEngine;

namespace ControlFreak2.Demos.RPG
{

public class CharacterAction : MonoBehaviour 
	{
	public LevelData
		levelData;

	public string 
		actionButonName = "Action";

	public string
		actionAnimatorTrigger = "Action";


	private Animator
		animator;



	// ------------------
	void OnEnable()
		{
		this.animator = this.GetComponent<Animator>();
		}

	// ------------------
	void Update()
		{
		if (!string.IsNullOrEmpty(this.actionButonName) && CF2Input.GetButtonDown(this.actionButonName))
			this.PerformAction();
		
		}
	
	
	// --------------------
	public void PerformAction()	
		{
		if (this.levelData == null)
			return;

		InteractiveObjectBase 
			obj = this.levelData.FindInteractiveObjectFor(this);
		
		if (obj != null)
			{
			if (!string.IsNullOrEmpty(this.actionAnimatorTrigger) && (this.animator != null)) 
				this.animator.SetTrigger(this.actionAnimatorTrigger);

			obj.OnCharacterAction(this);
			}
		}


	}
}
