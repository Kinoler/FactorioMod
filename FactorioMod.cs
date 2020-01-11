using System.Collections.Generic;
using FactorioMod.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace FactorioMod
{
	public class FactorioMod : Mod
	{
        public static ModHotKey OpenHotKey;

        internal UserInterface MyInterface;
        internal TestUI MyUI;

        public FactorioMod()
        {
        }

        public override void Load()
        {
            OpenHotKey = RegisterHotKey("OpenUI", "Y");
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyUI = new TestUI();
                MyUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            }
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        internal void SwichMyUI()
        {
            MyInterface?.SetState(MyInterface?.CurrentState == null ? MyUI : null);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
        }
    }
}