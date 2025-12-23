using System.Collections.Generic;
using System.Data.SqlClient;
using MVC_ADO_CRUD.Models;
using System.Data;

namespace MVC_ADO_CRUD.Services
{
    public class HomeServices
    {
        private readonly DBHelper.DBHelper _db;

        public HomeServices(DBHelper.DBHelper db)
        {
            _db = db;
        }

        public List<Customer> GetAllData()
        {
            DataTable dt = _db.ExecuteQuery("");
            List<Customer> list = new();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Customer()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr[""].ToString(),
                    Email = dr[""].ToString()

                });

            }
            return list;
        }
        public void InsertCustomer(Customer c)
        {
            _db.ExecuteNonQuery("", new List<SqlParameter>
            {
                new SqlParameter("",c.Name),
                new SqlParameter("",c.Email)
            });
        }
       
    }
}
