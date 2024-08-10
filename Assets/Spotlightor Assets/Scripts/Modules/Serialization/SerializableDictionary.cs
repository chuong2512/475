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
using System .Collections.Generic;

public class SerializableDictionary<T,U> : ISerializationCallbackReceiver
{
	public List<T> keys;
	public List<U> values;

	public Dictionary<T,U> Dictionary{ get; private set; }

	public virtual void OnBeforeSerialize ()
	{
		if (Application.isEditor && Application.isPlaying == false)
			ValidateKeyValuePairs ();
		else
			FillKeyValuesByDictionary ();
	}

	private void FillKeyValuesByDictionary ()
	{
		keys = new List<T> ();
		values = new List<U> ();

		if (Dictionary != null) {
			foreach (KeyValuePair<T,U> kvp in Dictionary) {
				keys.Add (kvp.Key);
				values.Add (kvp.Value);
			}
		}
	}

	private void ValidateKeyValuePairs ()
	{
		if (keys != null && values != null) {
			while (values.Count != keys.Count) {
				if (values.Count > keys.Count)
					values.RemoveAt (values.Count - 1);
				else
					values.Add (default(U));
			}
		}
	}

	public virtual void OnAfterDeserialize ()
	{
		UpdateDictionary ();
	}

	public void UpdateDictionary ()
	{
		Dictionary = new Dictionary<T, U> ();
		int keyValuePairCount = Mathf.Min (keys.Count, values.Count);

		for (int i = 0; i < keyValuePairCount; i++) {
			T key = keys [i];
			U value = values [i];

			Dictionary.Add (key, value);
		}
	}
}
