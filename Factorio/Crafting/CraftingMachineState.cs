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

        public Item UpdateIngredient(Item item, int index)
        {
            if (index < 0 || index >= Ingredients.Count)
                return null;

            Ingredients[index] = item;

            if (Ingredients[index].type == 0)
            {
                Ingredients[index] = Recipe.Ingredients[index].Clone();
                Ingredients[index].stack = 0;
            }

            TryCraftItem();
            return Ingredients[index];

        }

        public Item UpdateCreatedItem(Item item)
        {
            CreatedItem = item;
            if (CreatedItem.type == 0)
            {
                CreatedItem = Recipe.CreateItem.Clone();
                CreatedItem.stack = 0;
            }

            TryCraftItem();
            return CreatedItem;
        }

        public void CraftItem()
        {
            CraftStarted = false;
            CreatedItem.stack += Recipe.CreateItem.stack;
            if (OnCraftEnd != null)
            {
                OnCraftEnd?.Invoke(this);
            }

            TryCraftItem();
        }

        private void TryCraftItem()
        {
            if (!CraftStarted && CraftActions.Craft(this, Recipe))
            {
                CraftStarted = true;
                _precentage = FactorioTimer.SubscribeAction(CraftActions.CalculateCraftTime(_craftingSpeed, Recipe), CraftItem);
                if (OnCraftBegin != null)
                {
                    OnCraftBegin?.Invoke(this);
                }
            }
        }

        protected abstract Recipe[] GetAvailableRecipes();
    }
}
