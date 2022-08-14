using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using Combustion.Battle.Nodes;
using System;

namespace Combustion.Editor.Nodes
{
    public class BattleNodesEditor : EditorWindow
    {
        BattleNodesGraphView graphView;
        InspectorView inspectorView;

        [MenuItem("Window/Combustion/Battle Nodes Editor")]
        public static void OpenWindow() {
            BattleNodesEditor wnd = GetWindow<BattleNodesEditor>();
            wnd.titleContent = new GUIContent("BattleNodesEditor");
        }

        public void CreateGUI() {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/Nodes/BattleNodesEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/Nodes/BattleNodesEditor.uss");
            root.styleSheets.Add(styleSheet);

            graphView = root.Q<BattleNodesGraphView>();
            inspectorView = root.Q<InspectorView>();
            graphView.OnNodeSelected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnSelectionChange() {
            BattleTree tree = Selection.activeObject as BattleTree;
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                graphView.PopulateView(tree);
            }
        }

		void OnNodeSelectionChanged(NodeView node) {
            inspectorView.UpdateSelection(node);
		}
    }
}