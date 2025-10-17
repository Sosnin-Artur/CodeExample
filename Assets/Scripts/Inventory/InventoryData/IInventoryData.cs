public interface IInventoryData : IData
{
    public IItemData[] Items { get; set; }
}