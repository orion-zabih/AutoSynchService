namespace AutoSynchPostVoucher.Classes
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
    }
}
