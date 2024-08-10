/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System;
using System.Collections;

#if UNITY_XBOXONE
using Users;
using Storage;
using DataPlatform;
using TextSystems;
using ConsoleUtils;
#endif
public class XboxPluginsInitializer : MonoBehaviour
{
	public bool storage = true;
	public bool textSystems = true;
	public bool dataPlatform = true;
	public bool consoleUtils = true;
	public bool marketPlace = true;
	public bool dlc = true;
	public bool catalogService = true;
	public bool inventoryService = true;
	public bool store = true;
	public bool achievements = true;
	public TextAsset eventsManifest;
	public bool setupGetGamerPicRenderThread = true;
	#if UNITY_XBOXONE
	private static IntPtr usersRenderThreadCallback = IntPtr.Zero;
	private static bool initialized = false;
	#endif

	void Awake ()
	{
		if (Application.platform != RuntimePlatform.XboxOne)
			this.enabled = false;
	}

	#if UNITY_XBOXONE
	void Start ()
	{
		if (Application.platform == RuntimePlatform.XboxOne && !initialized) {
			
			if (storage)
				StorageManager.Create ();

			if (dataPlatform)
				DataPlatformPlugin.InitializePlugin (0);

			if (textSystems)
				TextSystemsManager.Create ();
			
			UsersManager.Create ();

			if (consoleUtils) {
				ConsoleUtilsManager.Create ();
				CurrentApp.Create ();
			}

			if (marketPlace)
				Marketplace.MarketplaceManager.Create ();

			if (dlc)
				Marketplace.DLCManager.Create ();

			if (catalogService)
				Marketplace.CatalogServiceManager.Create ();

			if (inventoryService)
				Marketplace.InventoryServiceManager.Create ();

			if (store)
				Marketplace.StoreManager.Create ();

			if (dlc)
				Marketplace.DLCManager.Create ();

			if (achievements)
				AchievementsManager.Create ();

			if (eventsManifest != null) {
				EventManager.CreateFromText (eventsManifest.text);
				eventsManifest = null;
			}

			if (setupGetGamerPicRenderThread) {
				// This is called when the UsersManager needs to push something to the render thread.
				// This is important for loading GamerPics as we upload the texture for you
				// and need to do this from the render thread. Providing this simple callback hookup
				// allows the plugin to push something to the render thread on need.
				//
				// If you fail to provide this GetGamerPic will never return!
				usersRenderThreadCallback = UsersManager.GetGamerPicRenderThreadEventRequestEventCallback ();
				UsersManager.OnRenderThreadEventRequestRequired += UsersManager_OnRenderThreadEventRequestRequired;
			}

			initialized = true;
		}
	}

	private static void UsersManager_OnRenderThreadEventRequestRequired (int eventId)
	{
		GL.IssuePluginEvent (usersRenderThreadCallback, eventId);
	}
	#endif
}
