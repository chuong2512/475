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

namespace ControlFreak2.Demos.Characters
{
public class DualStickShooterCharaAnimator : MonoBehaviour 
	{
	public DualStickShooterCharaMotor charaMotor;
	public Animator animator;

	public string 
		forwardBackwardParamName	= "Vertical",
		strafeParamName				= "Horizontal",
		speedParamName					= "Speed";





	// ------------------
	void OnEnable()
		{
		if (this.animator == null)
			this.animator = this.GetComponent<Animator>();

		if (this.charaMotor == null)
			this.charaMotor = this.GetComponent<DualStickShooterCharaMotor>();			
		}


	// -----------------	
	void Update()
		{
		if ((this.animator == null) || (this.charaMotor == null))
			return;

			Vector2 v = new Vector2(this.charaMotor.GetLocalDir().x, this.charaMotor.GetLocalDir().z);


		if (!string.IsNullOrEmpty(this.speedParamName))	
			this.animator.SetFloat(this.speedParamName, v.magnitude); 

		if (!string.IsNullOrEmpty(this.forwardBackwardParamName))	
			this.animator.SetFloat(this.forwardBackwardParamName, v.y); 
		if (!string.IsNullOrEmpty(this.strafeParamName))	
			this.animator.SetFloat(this.strafeParamName, v.x); 
		}
	}
}
