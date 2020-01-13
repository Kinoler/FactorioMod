using System.Collections.Generic;
using System.Linq;
using FactorioMod.Factorio;
using FactorioMod.Factorio.Crafting;
using FactorioMod.Factorio.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace FactorioMod.Tiles.Placeable.CraftingMachine
{
    public class CraftingMachine : Entity
    {
        private readonly Dictionary<Vector2, CraftingMachineState> _machines;

        public string[] CraftingCategories { get; }
        public double BaseCraftingSpeed { get; set; }

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2); //this style already takes care of direction for us
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Test machine");
            AddMapEntry(new Color(200, 200, 200), name);

            disableSmartCursor = true;
        }
        public override bool HasSmartInteract()
        {
            return true;
        }

        public CraftingMachine() : base()
        {
            this._machines = new Dictionary<Vector2, CraftingMachineState>();
            BaseCraftingSpeed = 1;
            CraftingCategories = new string[] { };
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            _machines.Add(new Vector2(i, j), FactoryHelper.CraftingMachineStateFactory(this));
        }

        public override bool NewRightClick(int i, int j)
        {
            if (_machines[new Vector2(i, j)].Recipe.Recipe == null)
            {
                RecipeFinder finder = new RecipeFinder();
                finder.SetResult(ModContent.ItemType<Items.IntermediateProducts.IronGearWheelItem>());
                Recipe recipe2 = finder.SearchRecipes().First();
                if (recipe2 != null)
                {
                    _machines[new Vector2(i, j)].SelectRecipe(recipe2);
                }
            }

            GetInstance<FactorioMod>().ShowCarftingMachineUI(_machines[new Vector2(i, j)]);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemType<Items.Placeable.TestMachine>();
        }
    }
}
