using Terraria.ModLoader;

namespace FactorioMod.Factorio.Models
{
    public class SimpleItemRepresentation
    {
        public int Id;
        public int Stack;

        public static int GetItemId<TItemType>() where TItemType : ModItem
        {
            return ModContent.ItemType<TItemType>();
        }

        public static SimpleItemRepresentation GetItem<TItemType>(int stack) where TItemType : ModItem
        {
            return new SimpleItemRepresentation(GetItemId<TItemType>(), stack);
        }

        public SimpleItemRepresentation(int id, int stack)
        {
            Id = id;
            Stack = stack;
        }
    }
}
