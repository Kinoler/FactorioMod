using FactorioMod.Factorio.Helpers;
using FactorioMod.Factorio.Models;
using Terraria;

namespace FactorioMod.Factorio.Crafting
{
    public abstract class CraftingMachineState
    {
        private readonly double _craftingSpeed;
        private EnergySource _energy_source;
        private Energy _energy_usage;

        private bool craftStarted;
        private ulong eventTime;

        public AssemblingInventory Inventory { get; }
        public Item CreatedItem { get; set; }
        public FactorioRecipe Recipe { get; }

        public delegate void CreatedItemChanged(CraftingMachineState machine);
        public event CreatedItemChanged onCreatedItemChanged;

        public CraftingMachineState(double power)
        {
            Inventory = new AssemblingInventory();
            //Inventory.PropertyChanged += ItemsUpdated;
            Recipe = new FactorioRecipe();
            _craftingSpeed = power;
        }

        public Item ItemsUpdated(Item item, int index)
        {
            if (index < 0 || index >= Inventory.Items.Length)
                return new Item();

            Inventory.Items[index] = item;

            TryCreate();
            return Inventory.Items[index];

        }

        public Item CreatedItemUpdated(Item item)
        {
            CreatedItem = item;
            TryCreate();
            return CreatedItem;
        }

        private void TryCreate()
        {
            if (!craftStarted && CraftActions.Craft(this, Recipe))
            {
                craftStarted = true;

                eventTime = FactorioTimer.SubscribeAlarm(CraftActions.CalculateCraftTime(_craftingSpeed, Recipe), CraftItem);
            }
        }

        public void CraftItem()
        {
            craftStarted = false;
            if (CreatedItem.type == 0)
            {
                CreatedItem = Recipe.CreateItem.Clone();
                CreatedItem.stack = 0;
            }
            CreatedItem.stack += Recipe.CreateItem.stack;
            onCreatedItemChanged?.Invoke(this);
            TryCreate();
        }

        public void SelectRecipe(Recipe recipe)
        {
            Recipe.SetRecipe(recipe);
            Inventory.CloneFrom(Recipe.RequiredItems);

            CreatedItem = Recipe.CreateItem.Clone();
            CreatedItem.stack = 0;
        }

        public void SelectRecipe(int recipeId)
        {
            SelectRecipe(Main.recipe[recipeId]);
        }

        public void ResetRecipe(int recipeId)
        {
            Recipe.ResetRecipe();
            Inventory.Clear();
            CreatedItem = null;
        }
    }
}
