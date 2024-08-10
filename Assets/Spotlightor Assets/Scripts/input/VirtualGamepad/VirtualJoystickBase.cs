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

public abstract class VirtualJoystickBase : MonoBehaviour
{
	// iPad2: 132, iPhone Retina: 326, iPhone 8+: 401
	private const float DefaultPpi = 96;

	public static float ValidScreenDpi {
		get {
			float validDpi = ControlFreak2.CFScreen.dpi;

			if (Application.isEditor)
				validDpi = 401;
			
			if (validDpi == 0)
				validDpi = DefaultPpi;// On some device the screen dpi may not be detected.
			else {
				if (Application.isMobilePlatform && Screen.resolutions.Length > 0) {
					float resolutionScale = (float)Screen.width / (float)Screen.resolutions [0].width;
					validDpi *= resolutionScale;
				}
			}
			return validDpi;
		}
	}

	[Header ("BottomLeft = (0,0), TopRight = (1,1)")]
	public Rect inputViewportArea = new Rect (0, 0, 0.5f, 1f);

	[Header ("方向辅助锁定")]
	public int fixedDirectionsCount = 0;
	[Range (0, 1)]
	public float snapToDirStrength = 0;
	public float snapToDirSplitAngle = 45;

	public Vector2 StrengthDirFixed {
		get {
			Vector2 fixedStrength = Strength;
			if (fixedDirectionsCount > 0 && Strength != Vector2.zero) {
				Vector2 fixedDir = Strength.normalized;
				float dirSplitRad = MathAddons.TWO_PI / (float)fixedDirectionsCount;
				float strengthRad = Mathf.Atan2 (fixedDir.y, fixedDir.x);

				float fixedDirRad = (float)Mathf.RoundToInt (strengthRad / dirSplitRad) * dirSplitRad;
				MathAddons.FormatAngleInRadian (ref fixedDirRad);
				if (snapToDirStrength > 0) {
					float snapToDirSplitRad = snapToDirSplitAngle * Mathf.Deg2Rad;
					float snapToDirRad = (float)Mathf.RoundToInt (strengthRad / snapToDirSplitRad) * snapToDirSplitRad;
					MathAddons.FormatAngleInRadian (ref snapToDirRad);

					if (fixedDirRad != snapToDirRad) {
						float fixedToSnapDeltaRad = Mathf.Abs (MathAddons.DeltaRadian (fixedDirRad, snapToDirRad));
						float strengthToFixedDeltaRad = Mathf.Abs (MathAddons.DeltaRadian (strengthRad, fixedDirRad));
						if (strengthToFixedDeltaRad >= fixedToSnapDeltaRad * 0.5f * (1f - snapToDirStrength)) {
							Debug.LogFormat ("{0} snap to dir {1:0.0} from {2:0.0}, delta/targetDelta = {3:0.0}/{4:0.0}", 
								strengthRad * Mathf.Rad2Deg, snapToDirRad * Mathf.Rad2Deg, fixedDirRad * Mathf.Rad2Deg, 
								strengthToFixedDeltaRad * Mathf.Rad2Deg, fixedToSnapDeltaRad * 0.5f * (1f - snapToDirStrength) * Mathf.Rad2Deg);

							fixedDirRad = snapToDirRad;
						}
					}
				}

				fixedDir = new Vector2 (Mathf.Cos (fixedDirRad), Mathf.Sin (fixedDirRad));

				fixedStrength = fixedDir * Strength.magnitude;
			}
			return fixedStrength;
		}
	}

	public abstract Vector2 Strength { get; }

	public static Vector2 ScreenPixelToInches (Vector2 screenPointInPixels)
	{
		return new Vector2 (ScreenPixelToInches (screenPointInPixels.x), ScreenPixelToInches (screenPointInPixels.y));
	}

	public static float ScreenPixelToInches (float screenLengthInPixels)
	{
		return screenLengthInPixels / ValidScreenDpi;
	}

	public static Vector2 ScreenInchesToPixels (Vector2 screenPointInInches)
	{
		return new Vector2 (ScreenInchesToPixels (screenPointInInches.x), ScreenInchesToPixels (screenPointInInches.y));
	}

	public static float ScreenInchesToPixels (float screenLengthInInches)
	{
		return screenLengthInInches * ValidScreenDpi;
	}
}
