using System;

using TheWayOut.Gameplay;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

namespace TheWayOutEditor
{
    [CustomPropertyDrawer(typeof(PeaceInfo))]
    public class PeaceInfoDrawer : PropertyDrawer
    {
        public VisualTreeAsset m_InspectorUXML;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement myInspector = new VisualElement();

            // Load the UXML file and clone its tree into the inspector.
            if (m_InspectorUXML != null)
            {
                VisualElement root = m_InspectorUXML.CloneTree();
                myInspector.Add(root);

                var spriteObjectField = root.Q<ObjectField>("Sprite");

                spriteObjectField.BindProperty(property.FindPropertyRelative("sprite"));
                spriteObjectField.RegisterCallback<ChangeEvent<UnityEngine.Object>, VisualElement>(OnSpriteChange, root);
                root.Q<Toggle>("Up").BindProperty(property.FindPropertyRelative("freeUp"));
                root.Q<Toggle>("Left").BindProperty(property.FindPropertyRelative("freeLeft"));
                root.Q<Toggle>("Right").BindProperty(property.FindPropertyRelative("freeRight"));
                root.Q<Toggle>("Down").BindProperty(property.FindPropertyRelative("freeDown"));
                root.Q<IntegerField>("MinLevel").BindProperty(property.FindPropertyRelative("minLevel"));
            }

            // Return the finished Inspector UI.
            return myInspector;
        }

        private void OnSpriteChange(ChangeEvent<UnityEngine.Object> image, VisualElement root)
        {
            root.Q<VisualElement>("SpriteImage").style.backgroundImage = new StyleBackground(image.newValue as Sprite);
        }
    }
}
