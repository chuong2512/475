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

[RequireComponent (typeof(ColorDisplayer))]
public class ColorTweenEffect : FunctionalMonoBehaviour
{
	[ColorUsage (true, true, 0, 8, 0.125f, 3)]
	public Color[] colors = new Color[]{ Color.white, new Color (1, 1, 1, 0) };
	public float speed = 1;
	public bool usHsbColorLerp = false;
	public bool ignoreTimeScale = false;
	private int startColorIndex = 0;
	private float progress = 0;
	private float lastUpdateRealTime = 0;
	private ColorDisplayer colorDisplayer;

	public Color StartColor {
		get { return colors [StartColorIndex]; }
	}

	public Color TargetColor {
		get {
			int targetColorIndex = StartColorIndex + 1;
			targetColorIndex %= colors.Length;
			return colors [targetColorIndex];		
		}
	}

	protected int StartColorIndex {
		get { return startColorIndex; }
		set {
			startColorIndex = value % colors.Length;
			if (startColorIndex < 0)
				startColorIndex += colors.Length;
		}
	}

	public ColorDisplayer ColorDisplayer {
		get {
			if (colorDisplayer == null) {
				colorDisplayer = GetComponent<ColorDisplayer> ();
				if (colorDisplayer == null)
					colorDisplayer = gameObject.AddComponent<ColorDisplayer> ();
			}
			return colorDisplayer;
		}
	}

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime) {
			if (colors.Length == 0) {
				Debug.LogWarning ("ColorTweenEffects with 0 colors to tween! Auto remove the component.");
				Destroy (this);
			}
		}
		progress = 0;
	}

	protected override void OnBecameUnFunctional ()
	{
		TintColorAt (0);
	}

	public void TintColorAt (int index)
	{
		TintColor (colors [index]);
	}

	void Update ()
	{
		float deltaTime = Time.deltaTime;
		if (ignoreTimeScale) {
			deltaTime = Time.realtimeSinceStartup - lastUpdateRealTime;
			lastUpdateRealTime = Time.realtimeSinceStartup;
		}
		progress += speed * deltaTime;
		Color color = usHsbColorLerp ? HsbColor.LerpColorByHsb (StartColor, TargetColor, progress) : Color.Lerp (StartColor, TargetColor, progress);
		TintColor (color);
		
		if (progress > 1) {
			progress = 0;
			StartColorIndex++;
		}
	}

	private void TintColor (Color color)
	{
		ColorDisplayer.Display (color);
	}
}
