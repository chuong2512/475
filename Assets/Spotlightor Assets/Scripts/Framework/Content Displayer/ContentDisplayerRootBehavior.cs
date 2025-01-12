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
using System.Linq;

/// <summary>
/// Simplified Composite component of COMPOSITE pattern. 
/// Use Unity's hierarchy to add children automaticly.
/// </summary>
public class ContentDisplayerRootBehavior<T> : ContentDisplayerChildBehavior<T>
{
	public delegate void DisplayUpdatedEventHandler (ContentDisplayerRootBehavior<T> source,T cotent);

	public event DisplayUpdatedEventHandler DisplayUpdated;

	private T content;
	private List<ContentDisplayerChildBehavior<T>> children;

	public T Content {
		get{ return this.content;} 
		private set {
			this.content = value;
		}
	}
	
	public List<ContentDisplayerChildBehavior<T>> Children {
		get {
			if (children == null) {
				children = new List<ContentDisplayerChildBehavior<T>> ();

				List<ContentDisplayerChildBehavior<T>> childDisplayers = GetComponentsInChildren (typeof(ContentDisplayerChildBehavior<T>), true).Cast<ContentDisplayerChildBehavior<T>> ().ToList ();
				childDisplayers.Remove (this);

				foreach (ContentDisplayerChildBehavior<T> child in childDisplayers) {
					if (child is ContentDisplayerRootBehavior<T> && child.gameObject == this.gameObject)
						this.LogWarning ("You shouldn't attach more than 1 ContentDisplayerRootBehavior in the same GameObject! It may cause infinite loop when UpdateDisplay.");
					else
						children.Add (child);
				}
			}

			return children;
		}
	}

	public void UpdateDisplayOfCurrentContent ()
	{
		UpdateDisplay (this.Content);
	}

	public override void UpdateDisplay (T content)
	{
		Content = content;
		
		Children.ForEach (child => child.UpdateDisplay (Content));

		if (DisplayUpdated != null)
			DisplayUpdated (this, content);
	}

	public U FindChild<U> () where U : ContentDisplayerChildBehavior<T>
	{
		return Children.Find (child => child is U) as U;
	}
	
	public List<U> FindChildren<U> () where U : ContentDisplayerChildBehavior<T>
	{
		return Children.FindAll (child => child is U).Cast<U> ().ToList ();
	}
}
