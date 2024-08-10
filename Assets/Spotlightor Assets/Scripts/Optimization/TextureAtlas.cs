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

public class TextureAtlas : ScriptableObject
{
	public int textureSize = 1024;
	public int padding = 2;
	public Texture2D[] textures;
	[HideInInspector]
	[SerializeField]
	public Rect[] textureRects;
	
	public Texture2D Pack ()
	{
		Texture2D atlas = new Texture2D (textureSize, textureSize);
		textureRects = atlas.PackTextures (textures, padding, textureSize, false);
		return atlas;
	}
	
	public Rect GetTextureAtlasRect (Texture texture)
	{
		int textureIndex = System.Array.IndexOf (textures, texture);
		return textureRects [textureIndex];
	}
}
