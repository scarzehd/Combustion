using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Combustion.UI
{
    using Battle;
	using UnityEngine.Events;
	using Utility;

	public class MenuController : VisualElement
    {
        public static MenuController Instance;

        public new class UxmlFactory : UxmlFactory<MenuController, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        //Game ui
        public VisualElement ButtonBar { get; private set; }

        public VisualElement TextBox { get; private set; }

        private List<Button> buttons;

        private List<VisualElement> menuItems;

        private int menuIndex;

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
			}

            buttons[2].RegisterCallback<NavigationSubmitEvent>((evt) =>
            {
                CreateActMenu();
            });

            TextBox = this.Q("TextBox");
        }

        public void SelectCurrentMenuItem() {
            SelectMenuItem(menuIndex);
		}

        public void SelectMenuItem(int index) {
            if (BattleManager.Instance.turnState == BattleManager.TurnState.Player)
			{
                menuIndex = index;

                menuItems[index].Focus();
			}
		}

        public void SelectNextMenuItem() {
            menuIndex++;

            if (menuIndex % 3 == 0)
                menuIndex -= 3;

            SelectMenuItem(menuIndex);
		}

        public void SelectPreviousMenuItem() {
            menuIndex--;

            if (menuIndex % 3 < 0 || menuIndex % 3 == 2)
                menuIndex += 3;

            SelectMenuItem(menuIndex);
        }

        public void SelectRightMenuItem() {
            menuIndex += 3;

            if (menuIndex > menuItems.Count - 1)
                menuIndex -= menuItems.Count;

            SelectMenuItem(menuIndex);
        }

        public void SelectLeftMenuItem() {
            menuIndex -= 3;

            if (menuIndex < 0)
                menuIndex += menuItems.Count;

            SelectMenuItem(menuIndex);
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

        public void RemoveActMenu() {
            TextBox.Clear();
		}

        private void CreateActMenu() {
            SerializableDictionary<string, UnityEvent> actChoices = BattleManager.Instance.currentEnemy.actChoices;

            foreach (var (name, evt) in actChoices)
			{
                Button choice = new Button() { text = "* " + name };

                choice.RegisterCallback<ClickEvent>((e) =>
                {
                    evt.Invoke();
                });

                choice.pickingMode = PickingMode.Ignore;

                choice.AddToClassList("actChoice");

                TextBox.Add(choice);

                menuItems.Add(choice);

                choice.RegisterCallback<NavigationMoveEvent>((e) =>
                {
                    switch (e.direction)
					{
                        case NavigationMoveEvent.Direction.Left: SelectLeftMenuItem(); break;
                        case NavigationMoveEvent.Direction.Right: SelectRightMenuItem(); break;
                        case NavigationMoveEvent.Direction.Up: SelectPreviousMenuItem(); break;
                        case NavigationMoveEvent.Direction.Down: SelectNextMenuItem(); break;
					}

                    e.PreventDefault();
                });
			}

            SelectCurrentMenuItem();
		}
    }
}
