/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[ExecuteInEditMode ()]
public class EasyDebugGuiButton : MonoBehaviour
{
	public enum PivotPositionTypes
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public bool drawInEditorMode = false;
	public float fadeOutDelay = 3;

	[Header ("Button Appearance")]
	public PivotPositionTypes pivotPosition;
	public Rect position = new Rect (0, 0, 200, 50);
	public string label = "Easy Debug";
	public GUISkin skin;

	[Space ()]
	public UnityEvent clicked;
	private float startRealTime = 0;

	void Start ()
	{
		startRealTime = Time.realtimeSinceStartup;
	}

	void OnGUI ()
	{
		if (!Application.isPlaying && Application.isEditor && !drawInEditorMode)
			return;

		if (skin != null)
			GUI.skin = skin;

		if (Application.isPlaying && fadeOutDelay >= 0 && Time.realtimeSinceStartup - startRealTime > fadeOutDelay)
			GUI.color = new Color (1, 1, 1, 0);

		Rect positionByPivot = position;
		if (pivotPosition == PivotPositionTypes.TopRight || pivotPosition == PivotPositionTypes.BottomRight)
			positionByPivot.x = Screen.width - position.width - position.x;
		if (pivotPosition == PivotPositionTypes.BottomLeft || pivotPosition == PivotPositionTypes.BottomRight)
			positionByPivot.y = Screen.height - position.height - position.y;
		if (GUI.Button (positionByPivot, label)) {
			clicked.Invoke ();
		}
	}
}
