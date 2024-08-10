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

public static class SaveSlotsStorage
{
	public const string SlotsResourcesPath = "Save/Slots";

	private static List<SaveSlot> slots;

	private static SaveSlot activeSaveSlot;

	public static List<SaveSlot> Slots { get { return slots; } }

	static SaveSlotsStorage ()
	{
		slots = new List<SaveSlot> (Resources.LoadAll<SaveSlot> (SlotsResourcesPath));
		if (slots.Count == 0) {
			Debug.LogFormat ("Failed to load any SaveSlots at Resources path: {0}. A [default] slot will be created.", SlotsResourcesPath);

			SaveSlot slot = ScriptableObject.CreateInstance<SaveSlot> ();
			slot.name = "default";
			slot.isDefault = true;
			slots.Add (slot);

			Debug.Log (string.Format ("Default SaveLost created: {0}", slot.name));
		}
	}

	public static SaveSlot ActiveSaveSlot {
		get {
			if (activeSaveSlot == null) {
				activeSaveSlot = slots.Find (s => s.isDefault);
				if (activeSaveSlot == null) {
					activeSaveSlot = slots [0];
					Debug.LogWarning (string.Format ("No SaveSlot is marked as isDefault. The 1st one:{0} will be picked", activeSaveSlot.name));
				}
			}
			return activeSaveSlot;
		}
		set {
			activeSaveSlot = value;
		}
	}

	public static SaveSlot GetSaveSlot (string slotName)
	{
		SaveSlot saveSlot = slots.Find (s => s.name == slotName);
		return saveSlot;
	}
}
