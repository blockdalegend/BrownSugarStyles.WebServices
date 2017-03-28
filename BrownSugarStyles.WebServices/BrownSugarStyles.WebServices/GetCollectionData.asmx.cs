using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace BrownSugarStyles.WebServices
{
    /// <summary>
    /// Summary description for GetCollectionData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
    public class GetCollectionData : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
      
        public string ProductDesc()
        {

            
            DataTable dt = new DataTable("Results");
            dt.Namespace = "Products";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "prcGetCollectionData";
            conn.Open();
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            

            da.Fill(dt);
            
            conn.Close();

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            string jsonStr = string.Empty;
            jsonStr = DataTableToJSONWithJavaScriptSerializer(ds);
            return jsonStr;

        }

        public string DataTableToJSONWithJavaScriptSerializer(DataSet dataset)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            Dictionary<string, object> ssvalue = new Dictionary<string, object>();

            foreach (DataTable table in dataset.Tables)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                string tablename = table.TableName;
                foreach (DataRow row in table.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }

                ssvalue.Add(tablename, parentRow);
            }

            return jsSerializer.Serialize(ssvalue);
        }
    }
}
