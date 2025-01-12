/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof(TubePlacer), true)]
public class TubePlacerEditor : Editor
{
	private TubePlacer Placer{ get { return target as TubePlacer; } }

	void OnSceneGUI ()
	{
		if (!Placer.enabled)
			return;

		List<TubePart> childParts = GetChildTubeParts ();

		if (Placer.head != null && Placer.head.prefabs != null && Placer.head.prefabs.Count > 0) {
			if (childParts.Count == 0 || IsFromPartSetting (childParts [0], Placer.head) == false) {
				InsertTubePartInstance (Placer, Placer.head.DefaultPrefab, 0);
				Placer.UpdatePartsPlacement ();
			}
		}
		childParts = GetChildTubeParts ();
		if (Placer.tail != null && Placer.tail.prefabs != null && Placer.tail.prefabs.Count > 0) {
			if (childParts.Count <= 1 || IsFromPartSetting (childParts [childParts.Count - 1], Placer.tail) == false) {
				InsertTubePartInstance (Placer, Placer.tail.DefaultPrefab, childParts.Count);
				Placer.UpdatePartsPlacement ();
			}
		}

		if (Placer.head == null || Placer.head.prefabs == null || Placer.head.prefabs.Count == 0)
			DrawInsertNewPartButton (Placer, Placer.transform.position, Placer.transform.rotation, 0);
		else
			DrawInsertNewPartButton (Placer, childParts [0].EndWorldPosition, childParts [0].EndWorldRotation, 1);

		childParts = GetChildTubeParts ();
		foreach (TubePart part in childParts)
			DrawTubePartSceneEditor (part, Placer);
	}

	private List<TubePart> GetChildTubeParts ()
	{
		List<TubePart> childParts = new List<TubePart> ();
		for (int i = 0; i < Placer.transform.childCount; i++) {
			TubePart part = Placer.transform.GetChild (i).GetComponent<TubePart> ();
			if (part != null && part.gameObject.activeSelf)
				childParts.Add (part);
		}
		return childParts;
	}

	public static void DrawTubePartSceneEditor (TubePart part, TubePlacer placer)
	{
		TubePlacerEditor.DrawVariationButton (part, placer);
		
		if (part != null && !TubePlacerEditor.IsFromPartSetting (part, placer.head) && !TubePlacerEditor.IsFromPartSetting (part, placer.tail)) {
			TubePlacerEditor.DrawInsertNewPartButton (placer, part.EndWorldPosition, part.EndWorldRotation, part.transform.GetSiblingIndex () + 1);
			
			TubePlacerEditor.DrawDeleteButton (part, placer);
			
			if (part != null)
				TubePlacerEditor.DrawBendButton (part, placer);
		}
	}

	public static void DrawVariationButton (TubePart part, TubePlacer placer)
	{
		TubePlacer.PartSetting partSetting = FindPartSettingOfInstance (placer, part);
		
		if (partSetting != null && partSetting.prefabs.Count > 1) {
			Object currentPartPrefab = PrefabUtility.GetPrefabParent (part);
			int currentPrefabIndex = partSetting.prefabs.IndexOf (currentPartPrefab as TubePart);
			if (currentPrefabIndex >= 0) {
				Handles.color = new Color (0, 0, 1, 0.5f);
				if (Handles.Button ((part.transform.position + part.EndWorldPosition) * 0.5f, part.transform.rotation, placer.buttonSize * 1f, placer.buttonSize * 1f, Handles.SphereHandleCap)) {
					int newPrefabIndex = currentPrefabIndex + 1;
					if (newPrefabIndex >= partSetting.prefabs.Count)
						newPrefabIndex = 0;
					
					TubePart varPart = InsertTubePartInstance (placer, partSetting.prefabs [newPrefabIndex], part.transform.GetSiblingIndex ());
					DestroyImmediate (part.gameObject);
					#if UNITY_5
					UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
					#else
					EditorApplication.MarkSceneDirty ();
					#endif

					if (Selection.activeGameObject == null || Selection.activeGameObject != placer.gameObject)
						Selection.activeGameObject = varPart.gameObject;
				}
			}
		}
	}

	public static bool DrawInsertNewPartButton (TubePlacer placer, Vector3 position, Quaternion rotation, int siblingIndex)
	{
		Handles.color = new Color (0, 1, 0, 0.5f);
		if (Handles.Button (position, rotation, placer.buttonSize, placer.buttonSize, Handles.CubeHandleCap)) {
			if (placer.straight != null && placer.straight.DefaultPrefab != null) {
				TubePart newPart = InsertTubePartInstance (placer, placer.straight.DefaultPrefab, siblingIndex);
				if (Selection.activeGameObject == null || Selection.activeGameObject != placer.gameObject)
					Selection.activeGameObject = newPart.gameObject;
			}
			return true;
		} else
			return false;
	}

	public static void DrawDeleteButton (TubePart part, TubePlacer placer)
	{
		Handles.color = new Color (1, 0, 0, 0.5f);
		if (Handles.Button (part.EndWorldPosition + part.EndWorldRotation * Vector3.forward * -placer.buttonSize * 0.75f, part.EndWorldRotation, placer.buttonSize * 0.5f, placer.buttonSize * 0.5f, Handles.CubeHandleCap)) {
			DestroyImmediate (part.gameObject);
			#if UNITY_5
			UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
			#else
			EditorApplication.MarkSceneDirty ();
			#endif
		}
	}

	public static void DrawBendButton (TubePart part, TubePlacer placer)
	{
		Vector2 partDirType = Vector2.zero;
		if (IsFromPartSetting (part, placer.straight))
			partDirType = Vector2.zero;
		else {
			int index = placer.bendRight != null ? placer.bendRight.FindIndex (s => IsFromPartSetting (part, s)) : -1;
			if (index >= 0)
				partDirType.x = index + 1;
			index = placer.bendLeft.FindIndex (s => IsFromPartSetting (part, s));
			if (index >= 0)
				partDirType.x = -index - 1;
			index = placer.bendUp.FindIndex (s => IsFromPartSetting (part, s));
			if (index >= 0)
				partDirType.y = index + 1;
			index = placer.bendDown.FindIndex (s => IsFromPartSetting (part, s));
			if (index >= 0)
				partDirType.y = -index - 1;
		}
		
		Vector2 newPartDirType = partDirType;
		if (placer.bendRight.Count > 0 && DrawDirectionButton (placer, part.transform.position + part.transform.forward * 0.5f, part.transform.rotation, Vector3.right)) {
			partDirType.x = Mathf.Clamp (partDirType.x + 1, -placer.bendLeft.Count, placer.bendRight.Count);
			partDirType.y = 0;
		}
		if (placer.bendLeft.Count > 0 && DrawDirectionButton (placer, part.transform.position + part.transform.forward * 0.5f, part.transform.rotation, Vector3.left)) {
			partDirType.x = Mathf.Clamp (partDirType.x - 1, -placer.bendLeft.Count, placer.bendRight.Count);
			partDirType.y = 0;
		}
		if (placer.bendUp.Count > 0 && DrawDirectionButton (placer, part.transform.position + part.transform.forward * 0.5f, part.transform.rotation, Vector3.up)) {
			partDirType.y = Mathf.Clamp (partDirType.y + 1, -placer.bendDown.Count, placer.bendUp.Count);
			partDirType.x = 0;
		}
		if (placer.bendDown.Count > 0 && DrawDirectionButton (placer, part.transform.position + part.transform.forward * 0.5f, part.transform.rotation, Vector3.down)) {
			partDirType.y = Mathf.Clamp (partDirType.y - 1, -placer.bendDown.Count, placer.bendUp.Count);
			partDirType.x = 0;
		}
		if (newPartDirType != partDirType) {
			TubePart partPrefab = placer.straight.DefaultPrefab;
			if (partDirType.x > 0)
				partPrefab = placer.bendRight [(int)partDirType.x - 1].DefaultPrefab;
			if (partDirType.x < 0)
				partPrefab = placer.bendLeft [-(int)partDirType.x - 1].DefaultPrefab;
			if (partDirType.y > 0)
				partPrefab = placer.bendUp [(int)partDirType.y - 1].DefaultPrefab;
			if (partDirType.y < 0)
				partPrefab = placer.bendDown [-(int)partDirType.y - 1].DefaultPrefab;

			TubePart bendPart = InsertTubePartInstance (placer, partPrefab, part.transform.GetSiblingIndex ());
			DestroyImmediate (part.gameObject);

			if (Selection.activeGameObject == null || Selection.activeGameObject != placer.gameObject)
				Selection.activeGameObject = bendPart.gameObject;
		}
	}

	public static bool DrawDirectionButton (TubePlacer placer, Vector3 position, Quaternion baseRotation, Vector3 dir)
	{
		Handles.color = dir.y == 0 ? Color.red : Color.green;
		return Handles.Button (position + baseRotation * dir * placer.bendArrowOffset, baseRotation * Quaternion.LookRotation (dir), 1f, 1f, Handles.ArrowHandleCap);
	}

	public static TubePart InsertTubePartInstance (TubePlacer placer, TubePart prefab, int siblingIndex)
	{
		TubePart instance = PrefabUtility.InstantiatePrefab (prefab) as TubePart;
		instance.transform.SetParent (placer.transform, false);
		instance.transform.SetSiblingIndex (siblingIndex);
		#if UNITY_5
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty (UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene ());
		#else
		EditorApplication.MarkSceneDirty ();
		#endif
		return instance;
	}

	private static TubePlacer.PartSetting FindPartSettingOfInstance (TubePlacer placer, TubePart part)
	{
		TubePlacer.PartSetting partSetting = null;
		if (IsFromPartSetting (part, placer.straight))
			partSetting = placer.straight;
		else if (IsFromPartSetting (part, placer.head))
			partSetting = placer.head;
		else if (IsFromPartSetting (part, placer.tail))
			partSetting = placer.tail;
		else {
			if (placer.bendRight != null)
				partSetting = placer.bendRight.Find (s => IsFromPartSetting (part, s));
			if (partSetting == null && placer.bendLeft != null)
				partSetting = placer.bendLeft.Find (s => IsFromPartSetting (part, s));
			if (partSetting == null && placer.bendUp != null)
				partSetting = placer.bendUp.Find (s => IsFromPartSetting (part, s));
			if (partSetting == null && placer.bendDown != null)
				partSetting = placer.bendDown.Find (s => IsFromPartSetting (part, s));
		}
		return partSetting;
	}

	private static bool IsFromPartSetting (Object instance, TubePlacer.PartSetting partSetting)
	{
		if (partSetting != null && partSetting.prefabs != null && partSetting.prefabs.Count > 0) {
			foreach (TubePart partPrefab in partSetting.prefabs) {
				if (PrefabUtility.GetPrefabParent (instance) == partPrefab)
					return true;
			}
		}
		
		return false;
	}

}
