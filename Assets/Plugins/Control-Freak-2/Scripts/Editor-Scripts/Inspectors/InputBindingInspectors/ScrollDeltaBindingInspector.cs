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
using UnityEditor;
using UnityEngine;

using ControlFreak2Editor;
using ControlFreak2;
using ControlFreak2.Internal;
using ControlFreak2Editor.Internal;

namespace ControlFreak2Editor.Inspectors
{


// ----------------------
// Scroll Delta Inspector.
// ----------------------
public class ScrollDeltaBindingInspector
	{	
	private GUIContent			labelContent;
	private Object				undoObject;
	//private Editor				editor;

	private AxisBindingInspector
		deltaBinding;
	private DigitalBindingInspector
		positiveDigitalBinding,
		negativeDigitalBinding;


	// ------------------
	public ScrollDeltaBindingInspector(Object undoObject, GUIContent labelContent)
		{
		this.labelContent	= labelContent;
		//this.editor		= editor;
		this.undoObject	= undoObject;
		this.deltaBinding = new AxisBindingInspector(undoObject, new GUIContent("Scroll Delta Binding"), false, InputRig.InputSource.Scroll);
		this.positiveDigitalBinding = new DigitalBindingInspector(undoObject, new GUIContent("Positive Digital Binding", "Key or Button to turn on whenever this source scrolls in the positive direction (UP or RIGHT)."));
		this.negativeDigitalBinding = new DigitalBindingInspector(undoObject, new GUIContent("Negative Digital Binding", "Key or Button to turn on whenever this source scrolls in the negative direction (DOWN or LEFT)."));
		}



	// ------------------
	public void Draw(ScrollDeltaBinding bind, InputRig rig)
		{
		bool	bindingEnabled	= bind.enabled;

		EditorGUILayout.BeginVertical();

		if (bindingEnabled = EditorGUILayout.ToggleLeft(this.labelContent, bindingEnabled, GUILayout.MinWidth(30)))
			{

			CFGUI.BeginIndentedVertical(CFEditorStyles.Inst.transpSunkenBG);

				this.deltaBinding.Draw(bind.deltaBinding, rig);
				this.positiveDigitalBinding.Draw(bind.positiveDigitalBinding, rig);
				this.negativeDigitalBinding.Draw(bind.negativeDigitalBinding, rig);
					
	
			CFGUI.EndIndentedVertical();

			GUILayout.Space(InputBindingGUIUtils.VERT_MARGIN);
			}

		EditorGUILayout.EndVertical();


		if ((bindingEnabled	!= bind.enabled)  )
			{
			CFGUI.CreateUndo("Scroll Delta Binding modification.", this.undoObject);
			
			bind.enabled		= bindingEnabled;
			
			CFGUI.EndUndo(this.undoObject);
			}
		}
	} 

	
		
}
#endif
