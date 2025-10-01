using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Infrastructure.Helpers
{
    public class SqlServerInClauseParam<T>
    {
        public const char ParamIndicator = '@';     /*@paramName*/
        public readonly string Prefix;
        public const string Suffix = "Param";

        public readonly NpgsqlDbType DbDataType;
        public readonly List<T> Data;

        public SqlServerInClauseParam(NpgsqlDbType dataType, List<T> data, string prefix = "")
        {
            Prefix = prefix;
            DbDataType = dataType;
            Data = data;
        }

        private string Name(int index)
        {
            var name = String.Format("{0}{1}{2}", Prefix, index, Suffix);
            return name;
        }

        public string ParamsString()
        {
            string listString = "";
            for (int i = 0; i < Data.Count; i++)
            {
                if (!String.IsNullOrEmpty(listString))
                {
                    listString += ", ";
                }
                listString += String.Format("{0}{1}", ParamIndicator, Name(i));
            }
            return listString;
        }

        private List<NpgsqlParameter> ParamList()
        {
            var paramList = new List<NpgsqlParameter>();
            for (int i = 0; i < Data.Count; i++)
            {
                var data = new NpgsqlParameter
                { ParameterName = Name(i), NpgsqlDbType = DbDataType, Value = Data[i] };
                paramList.Add(data);
            }
            return paramList;
        }

        public NpgsqlParameter[] Params()
        {
            var paramList = ParamList();
            return paramList.ToArray();
        }

        public NpgsqlParameter[] Params(params NpgsqlParameter[] additionalParameters)
        {
            var paramList = ParamList();
            foreach (var param in additionalParameters)
            {
                paramList.Add(param);
            }
            return paramList.ToArray();
        }
    }
}
