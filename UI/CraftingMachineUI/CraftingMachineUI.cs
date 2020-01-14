using FactorioMod.Factorio.Crafting;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace FactorioMod.UI.CraftingMachineUI
{
    internal class CraftingMachineUI : UIState
    {
        private readonly CraftingMachineState _machine;
        private CraftingMachineItemSlot[] _reqItemSlot;
        private CraftingMachineItemSlot _craftItemSlot;

        public CraftingMachineUI(CraftingMachineState machine)
        {
            this._machine = machine;
            machine.OnCraftEnd += MachineOnCraftEnd;
        }

        private void MachineOnCraftEnd(CraftingMachineState machine)
        {
            _craftItemSlot.Item = machine.CreatedItem;
        }

        public override void OnInitialize()
        {
            int left = 50;
            int reqLength = _machine.Recipe.Ingredients.Length;
            _reqItemSlot = new CraftingMachineItemSlot[reqLength];
            for (int i = 0; i < reqLength; i++)
            {
                int scopedIndex = i;
                int type = _machine.Recipe.Ingredients[i].type;
                _reqItemSlot[i] = new CraftingMachineItemSlot()
                {
                    Item = _machine.Ingredients[i],
                    Left = {Pixels = left + 50 * i},
                    Top = {Pixels = 270},
                    ValidItemFunc = item => item.IsAir || !item.IsAir && item.type == type,
                    OnSlotChange = item => _machine.IngredientsUpdated(item, scopedIndex)
                };
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
