using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace MVC_ADO_CRUD.DBHelper
{
    public class DBHelper
    {
        private readonly string _conStr;

        public DBHelper(IConfiguration configuration)
        {
            _conStr = configuration.GetConnectionString("DBCS");
        }



        // ===============================
        // 1️⃣ INSERT / UPDATE / DELETE
        // ===============================
        public int ExecuteNonQuery(string spName, List<SqlParameter>? parameters = null)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            con.Open();
            return cmd.ExecuteNonQuery();
        }

        // ===============================
        // 2️⃣ SELECT (DataTable)
        // ===============================
        public DataTable ExecuteQuery(string spName, List<SqlParameter>? parameters = null)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // ===============================
        // 3️⃣ EXECUTE SCALAR (ID, COUNT)
        // ===============================
        public object ExecuteScalar(string spName, List<SqlParameter>? parameters = null)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            con.Open();
            return cmd.ExecuteScalar();
        }

        // ===============================
        // 4️⃣ DATASET (MULTIPLE TABLES)
        // ===============================
        public DataSet ExecuteDataSet(string spName, List<SqlParameter>? parameters = null)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        // ===============================
        // 5️⃣ INLINE SQL (SELECT ONLY)
        // ===============================
        public DataTable ExecuteInlineQuery(string query, List<SqlParameter>? parameters = null)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(query, con);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // ===============================
        // 6️⃣ TRANSACTION SUPPORT
        // ===============================
        public void ExecuteTransaction(List<(string spName, List<SqlParameter> parameters)> commands)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            try
            {
                foreach (var item in commands)
                {
                    using SqlCommand cmd = new SqlCommand(item.spName, con, tran);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(item.parameters.ToArray());
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // ===============================
        // 7️⃣ OUTPUT PARAMETER SUPPORT
        // ===============================
        public Dictionary<string, object> ExecuteWithOutput(
            string spName,
            List<SqlParameter> parameters,
            List<string> outputParamNames)
        {
            using SqlConnection con = new SqlConnection(_conStr);
            using SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddRange(parameters.ToArray());

            con.Open();
            cmd.ExecuteNonQuery();

            Dictionary<string, object> result = new();
            foreach (var name in outputParamNames)
                result[name] = cmd.Parameters[name].Value;

            return result;
        }
    }
}
