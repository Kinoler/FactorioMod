using System;
using FactorioMod.Factorio.Helpers;
using FactorioMod.Factorio.Models;
using Terraria;

namespace FactorioMod.Factorio.Crafting
{
    public abstract class CraftingMachineState
    {
        public event CraftingMachineStateEvent OnCraftBegin;
        public event CraftingMachineStateEvent OnCraftEnd;

        private readonly double _craftingSpeed;
        private EnergySource _energy_source;
        private Energy _energy_usage;
        private Func<double> _precentage;
        private Recipe[] _availableRecipe;


        public bool CraftStarted { get; private set; }

        public double Precentage => _precentage.Invoke();

        public Inventory Ingredients { get; }

        public Item CreatedItem { get; set; }

        public FactorioRecipe Recipe { get; }

        public Recipe[] AvailableRecipe => _availableRecipe ?? (_availableRecipe = GetAvailableRecipes());

        public delegate void CraftingMachineStateEvent(CraftingMachineState machine);

        protected CraftingMachineState(double craftingSpeed)
        {
            Ingredients = new Inventory();
            Recipe = new FactorioRecipe();
            _craftingSpeed = craftingSpeed;
        }

        public Item IngredientsUpdated(Item item, int index)
        {
            if (index < 0 || index >= Ingredients.Items.Length)
                return new Item();

            Ingredients.Items[index] = item;

            TryCraftItem();
            return Ingredients.Items[index];

        }

        public Item CreatedItemUpdated(Item item)
        {
            CreatedItem = item;
            TryCraftItem();
            return CreatedItem;
        }

        public void CraftItem()
        {
            CraftStarted = false;
            if (CreatedItem.type == 0)
            {
                CreatedItem = Recipe.CreateItem.Clone();
                CreatedItem.stack = 0;
            }
            CreatedItem.stack += Recipe.CreateItem.stack;
            OnCraftEnd?.Invoke(this);
            TryCraftItem();
        }

        private void TryCraftItem()
        {
            if (!CraftStarted && CraftActions.Craft(this, Recipe))
            {
                CraftStarted = true;
                _precentage = FactorioTimer.SubscribeAction(CraftActions.CalculateCraftTime(_craftingSpeed, Recipe), CraftItem);
                OnCraftBegin?.Invoke(this);
            }
        }

        protected abstract Recipe[] GetAvailableRecipes();
    }
}
