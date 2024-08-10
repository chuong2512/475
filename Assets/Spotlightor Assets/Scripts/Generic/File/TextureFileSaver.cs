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
using System.IO;
using System;

public class TextureFileSaver : MonoBehaviour
{
	public delegate void GenericEventHanlder (TextureFileSaver source);

	public event GenericEventHanlder Completed;

	public Texture2D texture;
	public string filePath;
	public int quality = 75;

	IEnumerator Start ()
	{
		if (texture != null && !string.IsNullOrEmpty (filePath)) {
			#if !UNITY_WEBPLAYER
			string extension = Path.GetExtension (filePath).Substring (1);
			Byte[] fileBytes;
			if (extension.ToLower () == "jpg")
				fileBytes = texture.EncodeToJPG (quality);
			else
				fileBytes = texture.EncodeToPNG ();

			File.WriteAllBytes (filePath, fileBytes);
			#endif
			#if UNITY_WEBPLAYER
			this.LogWarning("Cannot write bytes to file in WebPlayer!");
			#endif
			yield return 1;

			if (Completed != null) 
				Completed (this);

			Destroy (gameObject);
		} else
			Destroy (gameObject);
	}
}
