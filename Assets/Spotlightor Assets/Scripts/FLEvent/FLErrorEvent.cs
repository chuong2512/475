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

public class FLErrorEvent : FLEventBase
{
	public const string IO_ERROR = "ioError";
	private string _error;
	public string error {
		get { return _error; }
	}
	public FLErrorEvent (string type, string error) : base(type)
	{
		_error = error;
	}
	public override FLEventBase Clone ()
	{
		return new FLErrorEvent(Type, _error);
	}
}

