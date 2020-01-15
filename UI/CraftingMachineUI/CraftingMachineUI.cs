using System;
using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Crafting.AssemblingMachine;
using FactorioMod.Factorio.Crafting.Furnace;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace FactorioMod.UI.CraftingMachineUI
{
    public class CraftingMachineUI<TCraftingMachine> : UIState where TCraftingMachine: CraftingMachineState
    {
        protected readonly TCraftingMachine _machine;
        protected CraftingMachineItemSlot[] _reqItemSlot;
        protected CraftingMachineItemSlot _craftItemSlot;

        protected int _leftWindow = 50;
        protected int _topWindow = 270;

        public CraftingMachineUI(TCraftingMachine machine)
        {
            this._machine = machine;
        }

        public override void OnInitialize()
        {
            int reqLength = _machine.Recipe.Ingredients.Length;
            _reqItemSlot = new CraftingMachineItemSlot[reqLength];
        }

        public CraftingMachineItemSlot CreateSlot(int left, int top, Func<Item> itemFunc, Func<Item, Item> onSlotChangeFunc, bool canTake = true)
        {
            CraftingMachineItemSlot itemSlot = new CraftingMachineItemSlot()
            {
                Item = itemFunc.Invoke(),
                Left = { Pixels = _leftWindow + left },
                Top = { Pixels = _topWindow + top },
                ValidItemFunc = item =>
                {
                    return item.IsAir == canTake == true || item.type == itemFunc.Invoke().type;
                },
                OnSlotChange = onSlotChangeFunc
            };
            Append(itemSlot);
            return itemSlot;
        }

        public void CreateIngredientsSlot(int left, int top, int index)
        {
            int scopedIndex = index;
            var createIngredientItemSlot = CreateSlot(
                left, 
                top, 
                () => _machine.Ingredients[scopedIndex],
                item => _machine.UpdateIngredient(item, scopedIndex));

            Append(createIngredientItemSlot);
        }

        public void CreateResultSlot(int left, int top)
        {
            var createResultItemSlot = CreateSlot(
                left,
                top,
                () => _machine.CreatedItem,
                item => _machine.UpdateCreatedItem(item));

            Append(createResultItemSlot);
        }

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
            Main.HidePlayerCraftingMenu = true;
        }
	}
}
