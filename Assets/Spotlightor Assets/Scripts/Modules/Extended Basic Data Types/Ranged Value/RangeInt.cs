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

public class RangeInt
{
	public delegate void BasicEventHandler (RangeInt source);

	public delegate void ValueChangedEventHandler (RangeInt source,int amount);
	
	public event ValueChangedEventHandler ValueChanged;
	public event BasicEventHandler MinValueReached;
	public event BasicEventHandler MaxValueReached;
	
	private int minValue;
	private int maxValue;
	private int currentValue;

	public int MaxValue {
		get { return maxValue; }
		set {
			maxValue = value;
			minValue = Mathf.Min (minValue, maxValue - 1);
			Value = Mathf.Clamp (Value, minValue, maxValue);
		}
	}

	public int MinValue {
		get { return minValue; }
		set {
			minValue = value;
			maxValue = Mathf.Max (maxValue, minValue + 1);
			Value = Mathf.Clamp (Value, minValue, maxValue);
		}
	}

	public int Value {
		get { return currentValue; }
		set {
			if (value == currentValue)
				return;
			
			int oldValue = currentValue;
			currentValue = Mathf.Clamp (value, minValue, maxValue);
			if (currentValue != oldValue) {
				if (ValueChanged != null)
					ValueChanged (this, currentValue - oldValue);
				
				if (currentValue == maxValue && MaxValueReached != null)
					MaxValueReached (this);
				else if (currentValue == minValue && MinValueReached != null)
					MinValueReached (this);
			}
		}
	}

	public float Progress{ get { return Mathf.InverseLerp (minValue, maxValue, currentValue); } }

	public RangeInt (int min, int max, int defaultValue)
	{
		this.MinValue = min;
		this.MaxValue = max;
		this.Value = defaultValue;
	}

	public static implicit operator int (RangeInt safeInt)
	{
		return safeInt.Value;
	}
}
