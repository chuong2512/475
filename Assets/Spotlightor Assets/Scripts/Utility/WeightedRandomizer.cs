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

public class WeightedRandomizer<T>
{
	private List<T> elements;
	private List<float> elementWeights;

	public T MaxWeightElement {
		get {
			T maxWeightElement = default(T);
			float maxWeight = float.MinValue;
			for (int i = 0; i < elements.Count; i++) {
				float weight = elementWeights [i];
				if (weight > maxWeight) {
					maxWeight = weight;
					maxWeightElement = elements [i];
				}
			}
			return maxWeightElement;
		}
	}
	
	public WeightedRandomizer ()
	{
		elements = new List<T> ();
		elementWeights = new List<float> ();
	}
	
	public void Add (T element, float weight)
	{
		elements.Add (element);
		elementWeights.Add (weight);
	}
	
	public void Remove (T element)
	{
		int index = elements.IndexOf (element);
		if (index != -1) {
			elements.RemoveAt (index);
			elementWeights.RemoveAt (index);
		} else
			Debug.LogWarning ("Cannot find element to remove: " + element.ToString ());
	}

	public float GetWeightOf (T element)
	{
		int index = elements.IndexOf (element);
		if (index != -1) 
			return elementWeights [index];
		else {
			Debug.LogWarning ("Cannot find element: " + element.ToString ());
			return 0;
		}
	}

	public void SetElementWeight (T element, float weight)
	{
		int index = elements.IndexOf (element);
		if (index != -1) 
			elementWeights [index] = weight;
		else
			Debug.LogWarning ("Cannot find element to modify: " + element.ToString ());
	}
	
	public T GetRandomElement ()
	{
		T result = default(T);
		if (elements.Count > 0) {
			float totalWeight = 0;
			foreach (float weight in elementWeights)
				totalWeight += weight;
			
			float randomValue = Random.Range (0, totalWeight);
			float currentWeight = 0;
			for (int i = 0; i < elements.Count; i++) {
				currentWeight += elementWeights [i];
				if (currentWeight >= randomValue) {
					result = elements [i];
					break;
				}
			}
		}
		return result;
	}
	
	public override string ToString ()
	{
		string str = "[Randomizer]\n";
		for (int i = 0; i < elements.Count; i++) 
			str += elements [i].ToString () + "\t\t" + elementWeights [i].ToString () + "\n";
		return str;
	}
}
