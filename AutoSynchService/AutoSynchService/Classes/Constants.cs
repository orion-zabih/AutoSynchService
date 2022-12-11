namespace AutoSynchService.Classes
{
    public class Constants
    {
        
    }
    public enum SynchTypes
    {
        Full,    // 0
        OnlySysTables,   // 1
        OnlyPurchaseTables,    // 2
        OnlySaleTables, // 3
        ExceptSaleMasterDetailTables, // 4
        OnlySaleMasterDetailTables, // 5
        Custom, // 6
    }
}
