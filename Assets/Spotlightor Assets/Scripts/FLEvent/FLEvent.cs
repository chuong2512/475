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
using System;

public class FLEvent : FLEventBase
{
	public const string START = "start";
	public const string PAUSE = "pause";
	public const string STOP = "stop";
	public const string COMPLETE = "complete";
	public const string CHANGE = "change";
	public const string OPEN = "open";
	public const string CLOSE = "close";
	public const string ACTIVATE = "activate";
	public const string DEACTIVATE = "deactivate";
	
	public FLEvent (string type):base(type)
	{
	}

	public FLEvent (Enum type):base(type)
	{
	}
}
