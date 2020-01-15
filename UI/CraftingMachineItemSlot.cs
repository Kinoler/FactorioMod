using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FactorioMod.Factorio;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace FactorioMod.UI
{
    public class CraftingMachineItemSlot : UIElement
    {
        private const int Context = ItemSlot.Context.BankItem;
        private const float Scale = 1f;
        internal Item Item;
        internal Func<Item, bool> ValidItemFunc;
        internal Func<Item, Item> OnSlotChange;

        public CraftingMachineItemSlot()
        {
            Width.Set(Main.inventoryBack9Texture.Width * Scale, 0f);
			Height.Set(Main.inventoryBack9Texture.Height * Scale, 0f);
		}

        protected override void DrawSelf(SpriteBatch spriteBatch) {
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = Scale;
			Rectangle rectangle = GetDimensions().ToRectangle();

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
				Main.LocalPlayer.mouseInterface = true;
				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
                {
                    Item tempItem = Item;
                    ItemSlot.Handle(ref Item, Context);
                    if (tempItem != Item)
                    {
                        Item = OnSlotChange(Item);
                    }
                }
			}
            ItemSlot.Draw(spriteBatch, ref Item, Context, rectangle.TopLeft());
			Main.inventoryScale = oldScale;
		}
	}
}