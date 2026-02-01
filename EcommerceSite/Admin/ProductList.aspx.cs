using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.Admin
{
	public partial class ProductList : System.Web.UI.Page
	{
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        ProductDAL productDAL;
        protected void Page_Load(object sender, EventArgs e)
		{
            Session["breadCumbTitle"] = "Product List";
            Session["breadCumbPage"] = "Product List";
            if (!IsPostBack)
            {
                GetProducts();
            }
            lblMsg.Visible=false;
		}
        private void GetProducts()
        {
            productDAL = new ProductDAL();
            tbl = new DataTable();
            tbl = productDAL.ProductWithDefaultImg();
            rProductList.DataSource = tbl;
            rProductList.DataBind();
        }
        protected void rProductList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                Response.Redirect("Product.asp+?Id" + e.CommandArgument);
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("Product", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd = new SqlCommand("sp_Product", con);
                cmd.Parameters.AddWithValue("@Action", "GetAllByID");
                cmd.Parameters.AddWithValue("@ProductID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

            }
        }
    }
}