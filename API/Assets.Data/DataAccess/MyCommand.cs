using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Assets.Data.DataAccess;
public static class MyCommand
{

    public static SqlCommand CmdProc(string proc, string conString)
    {
        var con = new SqlConnection(conString);
        var cmd = new SqlCommand
        {
            CommandType = CommandType.StoredProcedure,
            CommandText = proc,
            Connection = con
        };
        return cmd;
    }


    public static async Task<string> GetJson(SqlDataReader reader)
    {
        var jsonResult = new StringBuilder();
        if (!reader.HasRows)
        {
            jsonResult.Append("[]");
        }
        else
        {
            while (await reader.ReadAsync())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }
        }
        return jsonResult.ToString();
    }

    public static string GetJson2(SqlDataReader reader)
    {
        var jsonResult = new StringBuilder();
        if (!reader.HasRows)
        {
            jsonResult.Append("[]");
        }
        else
        {
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }
        }
        return jsonResult.ToString();
    }
    static JsonSerializerOptions option = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    public static string ToJson(object source)
    {
        return JsonSerializer.Serialize(source, option);
    }
    public static string DataTableStringBuilder(DataTable dataTable)
    {
        if (dataTable == null)
        {
            return string.Empty;
        }

        var jsonStringBuilder = new StringBuilder();
        if (dataTable.Rows.Count > 0)
        {
            jsonStringBuilder.Append("[");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                jsonStringBuilder.Append("{");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    jsonStringBuilder.AppendFormat("\"{0}\":\"{1}\"{2}",
                            dataTable.Columns[j].ColumnName.ToString(),
                            dataTable.Rows[i][j].ToString(),
                            j < dataTable.Columns.Count - 1 ? "," : string.Empty);

                jsonStringBuilder.Append(i == dataTable.Rows.Count - 1 ? "}" : "},");
            }
            jsonStringBuilder.Append("]");
        }

        return jsonStringBuilder.ToString();
    }
}