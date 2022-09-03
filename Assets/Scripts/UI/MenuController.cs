using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.UI
{
    using Battle;
	using UnityEngine.EventSystems;

	public class MenuController : VisualElement
    {
        public static MenuController Instance;

        public new class UxmlFactory : UxmlFactory<MenuController, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        //Debug ui
        public Label turnStateLabel;

        public Button switchTurnButton;

        private bool currentDebug = true;

        //Game ui
        public VisualElement buttonBar;

        public Button fightButton;

        public Button magicButton;

        public Button actButton;

        public Button itemButton;

        public Button defendButton;

        public MenuController() {
            Instance = this;

            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
		}

        private void OnGeometryChange(GeometryChangedEvent evt) {
            turnStateLabel = this.Q<Label>("TurnState");

            switchTurnButton = this.Q<Button>("SwitchTurnButton");
            switchTurnButton.RegisterCallback<ClickEvent>((evt) =>
			{
                BattleManager.Instance.AdvanceTurnState();
            });

            buttonBar = this.Q("GameButtons");
            fightButton = this.Q<Button>("FIGHT");
            magicButton = this.Q<Button>("MAGIC");
            actButton = this.Q<Button>("ACT");
            itemButton = this.Q<Button>("ITEM");
            defendButton = this.Q<Button>("DEFEND");
        }

        public void SetDebug(bool debug) {
            if (currentDebug == debug)
                return;

            currentDebug = debug;

            this.Query(className: "debug").ForEach((element) =>
			{
                if (debug)
                {
                    element.RemoveFromClassList("hidden");
                }
                else
                {
                    element.AddToClassList("hidden");
                }
            });
        }
    }
}
