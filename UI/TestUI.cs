using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FactorioMod.UI
{
    public class TestUI : UIState
    {
        private UIText text;
        private UIPanel button;

        public override void OnInitialize()
        {
            UIPanel panel = new UIPanel();
            panel.Width.Set(300, 0);
            panel.Height.Set(300, 0);
            panel.HAlign = panel.VAlign = 0.5f; // 1
            Append(panel);

            UIText header = new UIText("My UI Header");
            header.HAlign = 0.5f; // 1
            header.Top.Set(15, 0); // 2
            panel.Append(header);

            button = new UIPanel(); // 1
            button.Width.Set(100, 0);
            button.Height.Set(50, 0);
            button.HAlign = 0.5f;
            button.Top.Set(25, 0); // 2
            button.OnClick += OnButtonClick; // 3
            panel.Append(button);

            text = new UIText("Click me!");
            text.HAlign = text.VAlign = 0.5f; // 4
            button.Append(text); // 5
        }

        private void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            text.SetText("I was clicked!");
        }

        public override void Update(GameTime gameTime)
        {
            if (text.IsMouseHovering || button.IsMouseHovering)
            {
                Main.hoverItemName = "Click to see what happens";
            }
        }
    }
}
