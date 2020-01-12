using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FactorioMod.Annotations;
using FactorioMod.Factorio.Interfaces;
using Terraria;
using Terraria.ModLoader;

namespace FactorioMod.Factorio
{
    public class MachineBase
    {
        private readonly double _power;
        private bool craftStarted;
        private ulong eventTime;

        public MachineInventory Inventory { get; }
        public Item CreatedItem { get; set; }
        public FactorioRecipe Recipe { get; }

        public event PropertyChangedEventHandler CreatedItemChanged;

        public MachineBase(double power)
        {
            Inventory = new MachineInventory();
            Inventory.PropertyChanged += ItemsUpdated;
            Recipe = new FactorioRecipe();
            _power = power;
        }

        public void ItemsUpdated(object sender, EventArgs args)
        {
            if (!craftStarted && CraftActions.Craft(Inventory, Recipe))
            {
                craftStarted = true;

                eventTime = FactorioTimer.SubscribeAlarm(CraftActions.CalculateCraftTime(_power, Recipe), CraftItem);
            }
        }

        public void CraftItem()
        {
            CreatedItem.stack += Recipe.CreateItem.stack;
            CreatedItemChanged?.Invoke(this, new PropertyChangedEventArgs(null)); ;
        }

        public void SelectRecipe(int recipeId)
        {
            Recipe.SetRecipe(recipeId);
            Inventory.CloneFrom(Recipe.RequiredItems);
            CreatedItem.CloneDefaults(Recipe.CreateItem.type);
        }

        public void ResetRecipe(int recipeId)
        {
            Recipe.ResetRecipe();
            Inventory.Clear();
            CreatedItem = null;
        }
    }
}
