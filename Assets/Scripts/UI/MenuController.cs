using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Combustion.UI
{
    using Battle;
    using Utility;

	public class MenuController : VisualElement
    {
        public static MenuController Instance;

        public new class UxmlFactory : UxmlFactory<MenuController, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        //Game ui
        public VisualElement ButtonBar { get; private set; }

        private List<Button> buttons;

        private List<VisualElement> menuItems;

        private int buttonIndex;

        public MenuController() {
            Instance = this;

            this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

            buttons = new List<Button>();

            menuItems = new List<VisualElement>();

            buttonIndex = 0;
		}

        private void OnGeometryChange(GeometryChangedEvent evt) {
            ButtonBar = this.Q("GameButtons");

            buttons = new List<Button>();

			buttons.Add(this.Q<Button>("FIGHT"));
            buttons.Add(this.Q<Button>("MAGIC"));
            buttons.Add(this.Q<Button>("ACT"));
            buttons.Add(this.Q<Button>("ITEM"));
			buttons.Add(this.Q<Button>("DEFEND"));

			for (int i = 0; i < buttons.Count; i++)
			{
                Button button = buttons[i];

                button.RegisterCallback<NavigationMoveEvent>((evt) =>
				{
                    switch(evt.direction)
					{
                        case NavigationMoveEvent.Direction.Up:
                        case NavigationMoveEvent.Direction.Down: break;
                        case NavigationMoveEvent.Direction.Left: SelectPreviousButton(); break;
                        case NavigationMoveEvent.Direction.Right: SelectNextButton(); break;
					}

                    evt.PreventDefault();
                });

                button.RegisterCallback<NavigationSubmitEvent>((evt) =>
                {
                    Debug.Log(button.name);
                });

                button.RegisterCallback<BlurEvent>((evt) =>
                {
                    if (evt.relatedTarget is Button newButton)
					{
                        if (buttons.Contains(newButton))
						{
                            return;
						}
					}

                    Scheduler.Schedule(0, () => { SelectCurrentButton(); });
                });
			}
        }

        public void SelectCurrentButton() {
            SelectButton(buttonIndex);
		}

        public void SelectButton(int index) {
            if (BattleManager.Instance.turnState == BattleManager.TurnState.Player)
			{
                buttonIndex = index;

                buttons[index].Focus();
            }
        }

        public void SelectPreviousButton() {
            buttonIndex--;

            if (buttonIndex < 0)
                buttonIndex = 4;

            SelectButton(buttonIndex);
		}

        public void SelectNextButton() {
            buttonIndex++;

            if (buttonIndex > 4)
                buttonIndex = 0;

            SelectButton(buttonIndex);
        }

        private void CreateActMenu() {

		}
    }
}
