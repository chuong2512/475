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

[ExecuteInEditMode ()]
public class CameraDevGuideLines : MonoBehaviour
{
	public enum GuideLineTypes
	{
		Split3 = 0,
		SafeAreaForPS4 = 9,
	}

	public Color color = new Color (1, 1, 1, 0.3f);
	public GuideLineTypes guideLineType = GuideLineTypes.Split3;

	void Start ()
	{
		if (!Application.isEditor)
			Destroy (this);
	}

	void OnGUI ()
	{
		GUI.color = color;
		float lineSize = 2;
		switch (guideLineType) {
		case GuideLineTypes.Split3:
			{
				GUI.DrawTexture (new Rect (0, Screen.height * (1f / 3f) - 0.5f * lineSize, Screen.width, lineSize), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (0, Screen.height * (2f / 3f) - 0.5f * lineSize, Screen.width, lineSize), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (Screen.width * (1f / 3f) - 0.5f * lineSize, 0, lineSize, Screen.height), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (Screen.width * (2f / 3f) - 0.5f * lineSize, 0, lineSize, Screen.height), Texture2D.whiteTexture);
				break;
			}
		case GuideLineTypes.SafeAreaForPS4:
			{
				GUI.DrawTexture (new Rect (0, Screen.height * 0.05f - 0.5f * lineSize, Screen.width, lineSize), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (0, Screen.height * 0.95f - 0.5f * lineSize, Screen.width, lineSize), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (Screen.width * 0.05f - 0.5f * lineSize, 0, lineSize, Screen.height), Texture2D.whiteTexture);
				GUI.DrawTexture (new Rect (Screen.width * 0.95f - 0.5f * lineSize, 0, lineSize, Screen.height), Texture2D.whiteTexture);
				break;
			}
		}
	}
}
