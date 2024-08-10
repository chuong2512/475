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

public class GamepadVibrationShaker : Shaker
{
	[SingleLineLabel ()]
	public AnimationCurve vibrationByShakeIntensity = new AnimationCurve (new Keyframe (0.2f, 0), new Keyframe (1, 1));
	public float stopVibrationTimeScale = 0.1f;
	public bool logInfo = false;
	private List<GamepadVibrator> vibrators = new List<GamepadVibrator> ();

	private float VibrationIntensity {
		set {
			value = Mathf.Clamp01 (value);
			vibrators.ForEach (vb => vb.VibrationIntensity = value);
		}
	}

	void Awake ()
	{
		vibrators = new List<GamepadVibrator> (GetComponents<GamepadVibrator> ());
	}

	public override void Shake (Vector3 intensity)
	{
		if (sensor != null) {
			float cleanIntensity = intensity.magnitude;
			float vibration = vibrationByShakeIntensity.Evaluate (cleanIntensity);
			if (logInfo && cleanIntensity > 0)
				Debug.LogFormat ("Virbiration: {0:0.00}\tfrom Shake: {1:0.00}", vibration, cleanIntensity);

			if (Time.timeScale < stopVibrationTimeScale)
				vibration = 0;
			
			VibrationIntensity = vibration;
		}
	}

	void OnDestroy ()
	{
		VibrationIntensity = 0;
	}

	void OnDisable ()
	{
		VibrationIntensity = 0;
	}
}
