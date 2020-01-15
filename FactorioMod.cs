using System.Collections.Generic;
using FactorioMod.Factorio;
using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using FactorioMod.Factorio.Crafting.Furnace;
using FactorioMod.UI;
using FactorioMod.UI.CraftingMachineUI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace FactorioMod
{
	public class FactorioMod : Mod
	{
        public static ModHotKey OpenHotKey;

        internal UserInterface ExamplePersonUserInterface;

        public FactorioMod()
        {
        }

        public override void Load()
        {
            OpenHotKey = RegisterHotKey("OpenUI", "Y");
            if (!Main.dedServ)
            {
                ExamplePersonUserInterface = new UserInterface();
            }
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            ExamplePersonUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "FactorioMod: Carfting Machine UI",
                    delegate {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        ExamplePersonUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }

        internal void ShowCarftingMachineUI(CraftingMachineState machine)
        {
            Main.playerInventory = true;
            Main.npcChatText = "";

            switch (machine)
            {
                case AssemblingMachineState assemblingMachineState:
                    SetUserInterfaceState(new AssemblingMachineUI(assemblingMachineState));
                    break;
                case FurnaceState furnaceState:
                    SetUserInterfaceState(new FurnaceUI(furnaceState));
                    break;
            }
        }

        void SetUserInterfaceState(UIState newState)
        {
            GetInstance<FactorioMod>().ExamplePersonUserInterface.SetState(ExamplePersonUserInterface?.CurrentState == null ? newState : null);
        }
    }
}