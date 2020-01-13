using FactorioMod.Factorio;
using FactorioMod.Factorio.Crafting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace FactorioMod.UI
{
    internal class CraftingMachineUI : UIState
    {
        private readonly CraftingMachineState _machine;
        private CraftingMachineItemSlot[] _reqItemSlot;
        private CraftingMachineItemSlot _craftItemSlot;

        public CraftingMachineUI(CraftingMachineState machine)
        {
            this._machine = machine;
            machine.onCreatedItemChanged += Machine_onCreatedItemChanged;
        }

        private void Machine_onCreatedItemChanged(CraftingMachineState machine)
        {
            _craftItemSlot.Item = machine.CreatedItem;
        }

        public override void OnInitialize()
        {
            int left = 50;
            int reqLength = _machine.Recipe.RequiredItems.Length;
            _reqItemSlot = new CraftingMachineItemSlot[reqLength];
            for (int i = 0; i < reqLength; i++)
            {
                int scopedIndex = i;
                int type = _machine.Recipe.RequiredItems[i].type;
                _reqItemSlot[i] = new CraftingMachineItemSlot()
                {
                    Item = _machine.Inventory[i],
                    Left = {Pixels = left + 50 * i},
                    Top = {Pixels = 270},
                    ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == type,
                    OnSlotChange = item => _machine.ItemsUpdated(item, scopedIndex)
                };
                // Here we limit the items that can be placed in the slot. We are fine with placing an empty item in or a non-empty item that can be prefixed. Calling Prefix(-3) is the way to know if the item in question can take a prefix or not.
                Append(_reqItemSlot[i]);
            }

            _craftItemSlot = new CraftingMachineItemSlot()
            {
                Item = _machine.CreatedItem,
                Left = {Pixels = left + 50 * reqLength},
                Top = {Pixels = 300},
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == _machine.Recipe.CreateItem.type,
                OnSlotChange = item => _machine.CreatedItemUpdated(item)
            };
            Append(_craftItemSlot);
        }

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
            Main.HidePlayerCraftingMenu = true;
        }
	}
}
