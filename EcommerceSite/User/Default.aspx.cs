using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.User
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategories();
            }
        }
        private void getCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "ActiveCategory");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            rCategory.DataSource = tbl;
            rCategory.DataBind();
        }
    }
}