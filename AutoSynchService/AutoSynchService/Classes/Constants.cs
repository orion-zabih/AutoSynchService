namespace AutoSynchService.Classes
{
    public class Constants
    {
        
    }
    public enum SynchTypes
    {
        full,    // 0
        only_sys_tables,   // 1
        only_purchase_tables,    // 2
        only_sale_tables, // 3
        except_sale_master_detail_tables, // 4
        only_sale_master_detail_tables, // 5
        custom, // 6
    }
}
