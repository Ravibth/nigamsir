using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class CompareDTO
    {
        public Config Config { get; set; }
        public SchedularLogs SchedularLogs { get; set; }
        public List<Mapping> Mappings { get; set; }
    }
    public class Config
    {
        public string DestinationConnection { get; set; }
        public string SourceConnection { get; set; }
        public int MaxAttempts { get; set; }
    }
    public class SchedularLogs
    {
        public string DBName { get; set; }
        public string TableName { get; set; }
        public string Connection { get; set; }

    }
    public class Mapping
    {
        public string? NodeTitle { get; set; }
        public bool? IsExecutable { get; set; }
        //public string? SourceDBName { get; set; }
        public string? SourceTableName { get; set; }
        public string? SourceSelectQuery { get; set; }
        public string? SourceConnection { get; set; }
        //public string? DestinationDBName { get; set; }
        public string? DestinationTableName { get; set; }
        //public string? DestinationTableNameAs { get; set; }
        public string? DestinationSelectQuery { get; set; }
        public string? DestinationConnection { get; set; }
        public string? PrimaryKey { get; set; }
        //public string? ColumnsToUpdate { get; set; }
        public string? ColumnsToCompare { get; set; }
        //public string? InClause { get; set; }
        //public string? InClauseOperator { get; set; }
        //public bool? DestinationIsActiveColumnRequired { get; set; }
        //public string? DestinationIsActiveColumnName { get; set; }
        public bool? IsSyncToBeMade { get; set; }
        public bool? IsInsertToBeMade { get; set; }
        public bool? IsUpdateToBeMade { get; set; }

        //public bool? isOperationToBePerformedIfDestIsEmpty { get; set; }
        public string? IsInsertCheck { get; set; }
        public string? ComparingColumns { get; set; }
        public string? EventsOnInsert { get; set; }
        public string? SourceFilterQuery { get; set; }//NEW

        public string? ColumnMapping { get; set; }//NEW
        //public int? BatchSize { get; set; }//NEW
        //public string? OffSetColumn { get; set; }//NEW
        public string? IgnoreOnUpdate { get; set; }//NEW
        public string? UpdateCondition { get; set; }//NEW
        //public string? DestinationInsertQuery { get; set; }
        public string? ExtraValueCondtion { get; set; }
        public string? ExtraWhereCondtion { get; set; }

    }
}
