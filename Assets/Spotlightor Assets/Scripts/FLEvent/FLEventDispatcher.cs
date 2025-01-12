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
using System;

public class FLEventDispatcher : IFLEventDispatcher
{
	private Dictionary<string, List<FLEventBase.FLEventHandler>> _handlers;
	private string dispatchingEvent = null;
	private List<FLEventBase.FLEventHandler> delayedHandlersToRemove;
	private List<FLEventBase.FLEventHandler> delayedHandlersToAdd;

	public void DispatchEvent (FLEventBase e)
	{
		if (e.target == null)
			e.target = this;
		if (_handlers != null && _handlers.ContainsKey (e.Type)) {
			List<FLEventBase.FLEventHandler> eventHandlers = _handlers [e.Type];
			int i = 0;
			FLEventBase.FLEventHandler lastHandler = null;
			FLEventBase.FLEventHandler currentHandler = null;
			while (i < eventHandlers.Count) {
				currentHandler = eventHandlers [i];
				if (currentHandler != lastHandler)
					currentHandler (e);
				else
					i++;
				lastHandler = currentHandler;
			}
		}
	}
	
	public void AddEventListener (Enum eventType, FLEventBase.FLEventHandler handler)
	{
		AddEventListener (EnumHelper.EnumToString (eventType), handler);
	}

	public void AddEventListener (string eventType, FLEventBase.FLEventHandler handler)
	{
		if (dispatchingEvent == eventType) {
			if (delayedHandlersToAdd == null)
				delayedHandlersToAdd = new List<FLEventBase.FLEventHandler> ();
			delayedHandlersToAdd.Add (handler);
		} else {
			if (_handlers == null)
				_handlers = new Dictionary<string, List<FLEventBase.FLEventHandler>> ();

			List<FLEventBase.FLEventHandler> eventHandlers;
			if (!_handlers.ContainsKey (eventType))
				_handlers [eventType] = eventHandlers = new List<FLEventBase.FLEventHandler> ();
			else
				eventHandlers = _handlers [eventType];

			if (eventHandlers.IndexOf (handler) == -1)
				eventHandlers.Add (handler);
		}
	}
	
	public void RemoveEventListener (Enum eventType, FLEventBase.FLEventHandler handler)
	{
		RemoveEventListener (EnumHelper.EnumToString (eventType), handler);
	}

	public void RemoveEventListener (string eventType, FLEventBase.FLEventHandler handler)
	{
		if (dispatchingEvent == eventType) {
			if (delayedHandlersToRemove == null)
				delayedHandlersToRemove = new List<FLEventBase.FLEventHandler> ();
			delayedHandlersToRemove.Add (handler);
		} else {
			if (_handlers != null && _handlers.ContainsKey (eventType)) {
				List<FLEventBase.FLEventHandler> eventHandlers = _handlers [eventType];
				eventHandlers.Remove (handler);
			}
		}
	}

	private void AddRemoveDelayedHandlers (string eventType)
	{
		if (delayedHandlersToRemove != null) {
			foreach (FLEventBase.FLEventHandler handler in delayedHandlersToRemove)
				RemoveEventListener (eventType, handler);
		}
		if (delayedHandlersToAdd != null) {
			foreach (FLEventBase.FLEventHandler handler in delayedHandlersToAdd)
				AddEventListener (eventType, handler);
		}
		delayedHandlersToRemove = null;
		delayedHandlersToAdd = null;
	}
}
