using System.Collections.Generic;
using System.Linq;
using FactorioMod.Factorio.Interfaces;
using Terraria;

namespace FactorioMod.Factorio
{
    public static class CraftActions
    {
        public static Dictionary<int, int> ToHasItem(IStore store)
        {
            Dictionary<int, int> hasItem = new Dictionary<int, int>();
            foreach (var item in store.GetItems())
            {
                int itemId = item.netID;
                if (hasItem.ContainsKey(itemId))
                {
                    hasItem[itemId] += item.stack;
                }
                else
                {
                    hasItem.Add(itemId, item.stack);
                }
            }

            return hasItem;
        }

        public static bool CanBeCraft(IStore store, FactorioRecipe recipe) =>
            recipe.RequiredItems?.All(el =>
                (el.type == 0 || ToHasItem(store).TryGetValue(el.netID, out int val) && val >= el.stack)) ??
            false;

        public static bool Craft(IStore store, FactorioRecipe recipe)
        {
            if (!CanBeCraft(store, recipe))
                return false;

            SpendIngredients(store, recipe);
            return true;
        }

        public static void SpendIngredients(IStore store, FactorioRecipe recipe)
        {
            foreach (var requiredItems in recipe.RequiredItems)
            {
                int requiredStack = requiredItems.stack;
                int inventoryIndex = 0;
                foreach (var storedItem in store.GetItems())
                {
                    if (!storedItem.IsTheSameAs(requiredItems))
                        continue;

                    if (storedItem.stack > requiredStack)
                    {
                        storedItem.stack -= requiredStack;
                        requiredStack = 0;
                    }
                    else
                    {
                        requiredStack -= storedItem.stack;
                        store.SetItem(inventoryIndex, new Item());
                    }

                    inventoryIndex++;
                }
            }
        }

        public static int CalculateCraftTime(double craftingSpeed, FactorioRecipe recipe) => (int)(recipe.EnergyRequired / craftingSpeed * 1000);
    }
}
