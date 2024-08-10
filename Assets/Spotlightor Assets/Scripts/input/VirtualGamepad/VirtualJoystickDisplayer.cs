/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VirtualJoystickDisplayer : MonoBehaviour
{
	public VirtualJoystick joystick;
	public float handleMaxOffsetX = 0.5f;
	public Image handleImage;
	public Sprite handleNormalSprite;
	public Sprite handlePressedSprite;

	void Update ()
	{
		if (joystick.enabled) {
			handleImage.GetComponent<RectTransform> ().anchoredPosition = Vector2.right * joystick.RawStrength.x * handleMaxOffsetX;
		
			if (joystick.HaveBeenUsed) {
				Vector3 screenPos = new Vector3 (joystick.CenterPosition.x, joystick.CenterPosition.y, 10);
				Vector3 viewportPos = Camera.main.ScreenToViewportPoint (screenPos);

				Canvas canvas = GetComponentInParent<Canvas> ();
				Vector2 canvasSize = canvas.GetComponent<RectTransform> ().sizeDelta;

				Vector2 pos = Vector2.zero;
				pos.x = canvasSize.x * viewportPos.x;
				pos.y = canvasSize.y * viewportPos.y;

				GetComponent<RectTransform> ().anchoredPosition = pos;
			}
		
			handleImage.sprite = joystick.IsPressed ? handlePressedSprite : handleNormalSprite;
		}
	}
}
