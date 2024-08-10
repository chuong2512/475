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
using System.Collections.Generic;

#if !UNITY_WINRT
using System.Security.Cryptography;
#endif
public static class ListArrayExtensionMethods
{

	public static void Shuffle<T> (this IList<T> list)
	{
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = Random.Range (0, n + 1);
			T tempValueHolder = list [k];
			list [k] = list [n];
			list [n] = tempValueHolder;
		}
	}
	#if !UNITY_WINRT
	public static void ShuffleSlowButGood<T> (this IList<T> list)
	{
		
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ();
		int n = list.Count;
		while (n > 1) {
			byte[] box = new byte[1];
			do
				provider.GetBytes (box); while (!(box[0] < n * (byte.MaxValue / n)));
			int k = (box [0] % n);
			n--;
			T value = list [k];
			list [k] = list [n];
			list [n] = value;
		}
	}
	#endif
}
