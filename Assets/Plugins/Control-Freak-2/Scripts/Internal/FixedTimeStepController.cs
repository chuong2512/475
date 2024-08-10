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

//! \cond

using UnityEngine;

namespace ControlFreak2.Internal
{

// -----------------------
public class FixedTimeStepController
	{		
	static public float 
		deltaTime;

	private float
		fixedTime,
		fixedDeltaTime,
		fixedDeltaTimeCombined,
		timeAccum;
	private int
		totalFrameCount,	
		frameSteps;
		
	// ------------------
	public FixedTimeStepController(int framesPerSecond)
		{
		this.SetFPS(framesPerSecond);
		this.Reset();
		}
		
		
	// ----------------------
	public float GetDeltaTime()			{ return this.fixedDeltaTime; }
	public float GetDeltaTimeCombined()	{ return this.fixedDeltaTimeCombined; }
	public int GetFrameCount()			{ return this.totalFrameCount; }
	public int GetFrameSteps()			{ return this.frameSteps; }
	public float GetTime()				{ return this.fixedTime; }


	
	// -------------------
	public void Reset()
		{
		this.fixedTime = 0;	
		this.totalFrameCount = 0;		
		this.frameSteps = 0;	
		this.fixedDeltaTimeCombined = 0;
		}

	// -----------------
	public void SetFPS(int framesPerSecond)
		{
		this.fixedDeltaTime = (1.0f / (float)Mathf.Max(1, framesPerSecond));
		}

	// --------------------
	public void Update(float deltaTime)
		{
		this.timeAccum += deltaTime;	
	
		this.fixedDeltaTimeCombined = 0;
		this.frameSteps = 0;

		while (this.timeAccum > this.fixedDeltaTime)
			{
			this.fixedDeltaTimeCombined += this.fixedDeltaTime;
			this.frameSteps++;
			this.timeAccum -= this.fixedDeltaTime;
			}

		this.totalFrameCount += this.frameSteps;

		this.SetStaticData();
		}
	
	// -----------------------
	public void Execute(System.Action updateCallback)
		{		
		if (this.frameSteps <= 0)
			return;

		this.SetStaticData();
		
		for (int i = 0; i < this.frameSteps; ++i)
			updateCallback();
		}
		

	// ------------------	
	public void SetStaticData()
		{
		deltaTime = this.fixedDeltaTime;
		}
	}
}

//! \endcond

