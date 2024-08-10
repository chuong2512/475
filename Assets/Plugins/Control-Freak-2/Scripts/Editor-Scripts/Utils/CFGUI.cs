/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

#if UNITY_EDITOR

using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
	using ControlFreak2Editor.Internal;
#endif

namespace ControlFreak2Editor
{
public class CFGUI
	{
#if UNITY_EDITOR
		

	public const float INDENT_WIDTH = 15;

	public static bool changed;		// flag set to true, when any of the GUI controls changed 
	public static Object target;
		

	private static CFGUI inst;

	public static CFGUI Inst
		{
		get { 
			if (CFGUI.inst == null) 
				CFGUI.inst = new CFGUI();
			return CFGUI.inst; 
			}
		}


	// -----------------
	public CFGUI()
		{
		}
		

		
	// --------------
	static public void Start()	
		{
		CFGUI.changed = false;
		}


	// ---------------
	static public void End()	
		{
		}



	// ---------------
	static public void CreateUndo(string label, Object obj)
		{
		if (obj == null)
			return;

		Undo.RecordObject(obj, label);
		//EditorUtility.SetDirty(obj);
		}
		

	// ---------------
	static public void CreateUndoMulti(string label, Object[] objs)
		{
		if ((objs == null) || (objs.Length == 0))
			return;
		
		//Undo.RecordObjects(objs, label);

		for (int i = 0; i < objs.Length; ++i)
			{
			Undo.RecordObject(objs[i], label);
			//EditorUtility.SetDirty(objs[i]);
			}
		}


	// ------------
	static public void EndUndo(Object obj)
		{
		if (obj != null)
			{
			EditorUtility.SetDirty(obj);
			Undo.FlushUndoRecordObjects();
			}
		}

	// ---------------
	static public void EndUndoMulti(Object[] objs)
		{
		for (int i = 0; i < objs.Length; ++i)
			EditorUtility.SetDirty(objs[i]);

		Undo.FlushUndoRecordObjects();
		}



	// ------------
	static public void SetDirty(Object obj)
		{
		if (obj != null)
			EditorUtility.SetDirty(obj);
		}

		
	// -----------------------
	// GUI Drawing Methods...
	// -----------------------

	// ----------------
	static public bool Foldout(GUIContent label, bool openFlag)
		{
		return (openFlag = GUILayout.Toggle(openFlag, label, CFEditorStyles.Inst.foldout, GUILayout.ExpandWidth(true)));
		}

	// -----------------
	static public bool BoldFoldout(GUIContent label, ref bool openFlag)
		{
		return (openFlag = GUILayout.Toggle(openFlag, label, CFEditorStyles.Inst.boldFoldout, GUILayout.ExpandWidth(true)));
		}


		
		
	// ----------------
	static public bool PushButton(GUIContent content, bool toggled, GUIStyle style,  
		params GUILayoutOption[] layoutOptions)
		{
		return PushButton(content, content, toggled, style, layoutOptions);
		}


	static public bool PushButton(GUIContent trueContent, GUIContent falseContent, bool toggled, GUIStyle style,  
		params GUILayoutOption[] layoutOptions)
		{
		Color bgColor = GUI.backgroundColor;
			
		//if (toggled)
			

		bool v = GUILayout.Toggle(toggled, (toggled ? trueContent : falseContent), (style != null) ? style : CFEditorStyles.Inst.buttonStyle, layoutOptions);

		if (v != toggled)
			CFGUI.changed = true;

		GUI.backgroundColor = bgColor;
		
		return v;
		}  
			


	// ------------------
	static public string TextField(GUIContent labelContent, string val, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.TextField(val, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}
	
	// ------------------
	static public System.Enum EnumPopup(GUIContent labelContent, System.Enum val, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.EnumPopup(val, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}

		
	// ------------------
	static public Object ObjectField(GUIContent labelContent, Object val, System.Type objType, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.ObjectField(val, objType, true, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}
		
	// ------------------
	static public float Slider(GUIContent labelContent, float val, float minVal, float maxVal, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.Slider(val, minVal, maxVal, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}
		
	// ------------------
	static public int IntSlider(GUIContent labelContent, int val, int minVal, int maxVal, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.IntSlider(val, minVal, maxVal, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}
		


	// ------------------
	static public void MinMaxSlider(GUIContent labelContent, ref float valA, ref float valB, float minVal, float maxVal, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			EditorGUILayout.MinMaxSlider(ref valA, ref valB, minVal, maxVal, GUILayout.ExpandWidth(true)); 
		EditorGUILayout.EndHorizontal();
		}


			
	// -----------------
	static void PrefixLabel(GUIContent labelContent, float width)
		{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(width));
			EditorGUILayout.PrefixLabel(labelContent);
		EditorGUILayout.EndHorizontal();
		}

	// ------------------
	static public float FloatField(GUIContent labelContent, float val, float minVal, float maxVal, float maxLabelWidth, params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			val = EditorGUILayout.FloatField(val, GUILayout.ExpandWidth(true));
			val = Mathf.Clamp(val, minVal, maxVal); 
		EditorGUILayout.EndHorizontal();
	
		return val;
		}


	// ------------------
	static public float FloatFieldEx(GUIContent labelContent, float val, float minVal, float maxVal, float displayScale, bool displayAsInt, float maxLabelWidth, 
		params GUILayoutOption[] layoutOptions)
		{
		EditorGUILayout.BeginHorizontal(layoutOptions);
			
			EditorGUILayout.LabelField(labelContent, GUILayout.MaxWidth(maxLabelWidth));
			//PrefixLabel(labelContent, maxLabelWidth);

			if (displayAsInt)
				{
				int intVal = Mathf.FloorToInt(val * displayScale);
				int newIntVal = EditorGUILayout.IntField(intVal, GUILayout.ExpandWidth(true));
				if (newIntVal != intVal)
					val = ((float)newIntVal / displayScale); 
				}
			else
				{
				float scaledVal = (val * displayScale);
				float newScaledVal = EditorGUILayout.FloatField(scaledVal, GUILayout.ExpandWidth(true));
				if (newScaledVal != scaledVal)
					val = (newScaledVal / displayScale);
				}

		EditorGUILayout.EndHorizontal();
	
		val = Mathf.Clamp(val, minVal, maxVal);

		return val;
		}






	// -------------------
	static public int TriState(string label, string toolTip, int curState, GUIStyle normalStyle, GUIStyle mixedStyle, 
		params GUILayoutOption[] layoutOptions)
		{
		Color bgColor = GUI.backgroundColor;
			
		if (curState < 0)	
			GUI.backgroundColor = bgColor * 0.5f;

		bool v = GUILayout.Toggle((curState != 0), new GUIContent(label, toolTip), 
			((curState < 0) ? mixedStyle : normalStyle), layoutOptions);
			
		if ((curState < 0) && !v)
			{	
			curState = 0;
			CFGUI.changed = true;
			}
		else if ((curState != 0) != v)
			{
			curState = (v ? 1 : 0);
			CFGUI.changed = true;
			}
		GUI.backgroundColor = bgColor;
		
		return curState;		
		}
 
		
	// ----------------------
	static public void BeginIndentedVertical() { BeginIndentedVertical(null); }
	static public void BeginIndentedVertical(GUIStyle style)
		{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(INDENT_WIDTH);
		if (style == null)
			EditorGUILayout.BeginVertical();
		else
			EditorGUILayout.BeginVertical(style);
		}

	// ----------------------
	static public void EndIndentedVertical()
		{
			EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();
		}



#endif	

	}
}

#endif
