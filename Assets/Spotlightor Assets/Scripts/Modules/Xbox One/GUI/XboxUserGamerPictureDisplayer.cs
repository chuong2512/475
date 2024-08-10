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
using System;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_XBOXONE
using Users;
#endif
public class XboxUserGamerPictureDisplayer : ContentDisplayerChildBehavior<XboxUser>
{
	public Texture2D defaultPicture;
	#if UNITY_XBOXONE
	public Picture.Size pictureSize = Picture.Size.Small;
	#endif
	private Texture2D gamerPicture;

	public override void UpdateDisplay (XboxUser xboxUser)
	{
		GetComponent<TextureDisplayer> ().Display (defaultPicture);

		#if UNITY_XBOXONE
		int pictureDimension = Picture.GetDimension (pictureSize);
		gamerPicture = new Texture2D (pictureDimension, pictureDimension, TextureFormat.BGRA32, false);

		UsersManager.GetGamerPictureAsync (xboxUser.Index, (int)pictureSize, gamerPicture.GetNativeTexturePtr (), OnAsyncImageLoaded);
		#endif
	}
	#if UNITY_XBOXONE
	void OnAsyncImageLoaded (uint hresult, IntPtr texture, int userId)
	{
		this.Log ("Gamer Picture for User {0} loaded", userId);
		GetComponent<TextureDisplayer> ().Display (gamerPicture);
	}
	#endif
}
