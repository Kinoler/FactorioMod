using System.Collections.Generic;
using System.Linq;
using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Models;
using Terraria;

namespace FactorioMod.Factorio.Helpers
{
    public static class CraftActions
    {
        public static Dictionary<int, int> ToCountEachItem(IEnumerable<Item> store)
        {
            Dictionary<int, int> hasItem = new Dictionary<int, int>();
            foreach (var item in store)
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

        public static bool CanBeCraft(IEnumerable<Item> store, FactorioRecipe recipe) =>
            recipe.Ingredients?.All(el =>
                (el.type == 0 || ToCountEachItem(store).TryGetValue(el.netID, out int val) && val >= el.stack)) ??
            false;

        public static bool CreatedItemMaxStack(Item item) => item != null && (item.type == 0 || item.stack < item.maxStack);

        public static bool Craft(CraftingMachineState machine, FactorioRecipe recipe)
        {
            if (!CanBeCraft(machine.Ingredients, recipe) || !CreatedItemMaxStack(machine.CreatedItem))
                return false;

            SpendIngredients(machine.Ingredients, recipe);
            return true;
        }

        public static void SpendIngredients(IEnumerable<Item> store, FactorioRecipe recipe)
        {
            foreach (var requiredItems in recipe.Ingredients)
            {
                int requiredStack = requiredItems.stack;
                foreach (var storedItem in store)
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
                        storedItem.stack = 0;
                        //store.SetItem(inventoryIndex, new Item());
                    }
                }
            }
        }

        public static int CalculateCraftTime(double craftingSpeed, FactorioRecipe recipe) => (int) (recipe.EnergyRequired / craftingSpeed * 1000);
    }
}
