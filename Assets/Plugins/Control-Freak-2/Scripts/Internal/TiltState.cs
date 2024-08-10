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
[System.Serializable]
public class TiltState
	{
	public Vector2 
		analogRange;
			
	private Vector3 
		curVec,		
		nextFrVec;

	private Vector2 
		anglesNeutral,
		anglesAbsCur,
		anglesAbsRaw,
		//anglesSmoothVel,

		anglesCur,	
		analogCur;


	private bool 
		calibrated,
		setRefAnglesFlag;
		
		
	// --------------------
	public TiltState()
		{
		//this.deadzone = new Vector2(10, 10);
		this.analogRange = new Vector2(25, 25);
		this.anglesNeutral = new Vector2(0, 135);
		this.calibrated	= false;

		this.Reset();
		}
		

	
	// ------------------
	public void Reset()
		{
		this.calibrated = false;
		this.analogCur	= Vector2.zero;
		this.anglesCur	= Vector2.zero;
		}



	// ---------------------
	public void InternalApplyVector(Vector3 v)
		{
		this.nextFrVec += v;
		}

	// ----------------------
	static public bool IsAvailable()
		{
		return (Input.acceleration != Vector3.zero);
		}
		


	// -------------------
	public void Calibate()
		{
		this.setRefAnglesFlag = true;
		}

	// ---------------------
	public bool IsCalibrated()	
		{ return this.calibrated; }


	// ----------------------
	public void Update()
		{
		this.curVec = this.nextFrVec;
		this.nextFrVec = Vector3.zero;

		float lenSq = this.curVec.sqrMagnitude;
		if (lenSq < 0.000001f)
			{
			this.curVec = Vector3.zero;
			this.anglesAbsRaw = this.anglesNeutral;
			}
		else 
			{
			if (Mathf.Abs(lenSq - 1) > 0.001f)
				this.curVec.Normalize();

			this.anglesAbsRaw.x = Mathf.Atan2(this.curVec.x, Mathf.Max(Mathf.Abs(this.curVec.y), Mathf.Abs(this.curVec.z))) * Mathf.Rad2Deg;
			this.anglesAbsRaw.y = Mathf.Atan2(-this.curVec.y, this.curVec.z) * Mathf.Rad2Deg;

			}

		this.anglesAbsCur = this.anglesAbsRaw;
			

		// Delayed reset...

		if (this.setRefAnglesFlag)
			{
			this.calibrated			= true;
			this.setRefAnglesFlag	= false;
			this.anglesNeutral		= this.anglesAbsRaw;	// TODO : ??
			this.anglesNeutral.x = 0;
			}

		// Caluclate analog values...
			
		this.anglesCur = (this.anglesAbsCur - this.anglesNeutral);	//this.GetAngles();


		// If the accelerometer hasn't been calibrated yes, zero pitch angle...
			
		if (!this.calibrated)
			this.anglesCur.y = 0;


		for (int i = 0; i < 2; ++i)
			{
			float angle = this.anglesCur[i];
			float absAngle = Mathf.Abs(angle);
			float range = this.analogRange[i];

			this.analogCur[i] = ((absAngle >= range) ? 1.0f : ((absAngle) / (range))) * ((angle < 0) ? -1 : 1);			
			}

		}

		
	// --------------------
	/// Get Angles Relative to Neutral Angles. (X = Roll angle, Y = Pitch angle)
	// --------------------
	public Vector2 GetAngles() 	
		{ 
		return (this.anglesCur); 
		}

	// --------------------
	/// Get Analog Values (X = Roll normalized angle, Y = Pitch normalized angle)
	// --------------------
	public Vector2 GetAnalog()
		{	
		return this.analogCur;
		}

	}
}

//! \endcond
