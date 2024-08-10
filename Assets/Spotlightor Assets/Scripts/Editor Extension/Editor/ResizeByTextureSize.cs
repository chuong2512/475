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
using UnityEditor;

public class ResizeByTextureSize : ScriptableObject
{
	[MenuItem("Spotlightor/GUI/Resize GUITexture by Texture Size")]
	public static void ResizeGUITextureByTextureSize ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;

	}

	[MenuItem("Spotlightor/GUI/Resize Quad by Texture Size + Default Resolution")]
	public static void ResizePlane1x1ByTextureSizeAndDefaultResolution ()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		foreach (GameObject go in selectedObjects) {
			if (go.GetComponent<Renderer>() != null) {
				Vector3 newScale = Vector3.one;
				Texture texture = go.GetComponent<Renderer>().sharedMaterial.mainTexture;
				newScale.x = texture.width * 0.001f;
				newScale.y = texture.height * 0.001f;
				go.transform.localScale = newScale;
			}
		}
	}
}
