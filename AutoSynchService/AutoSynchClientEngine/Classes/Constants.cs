namespace AutoSynchClientEngine.Classes
{
    public class Constants
    {
        public const string SqlServer = "sqlserver";
        public const string Sqlite = "sqlite";
    }
    public enum SynchTypes
    {
        full,    // 0
        structure_only,    // 1
        only_sys_tables,   // 2
        only_purchase_tables,    // 3
        only_sale_tables, // 4
        except_sale_master_detail_tables, // 5
        only_sale_master_detail_tables, // 6
        except_product_sale_master_detail_tables, // 7
        custom, // 8
        products_quick, // 9
        products_recent, // 10
        //products_ledger_quick, // 9
    }
    public enum SynchMethods
    {
        database_structure,    // 0
        database_data,    // 1
        file_transfer,   // 2
        database_structure_alter,
    }
}
