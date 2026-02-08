using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.User
{
    public partial class Shop : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        DataView dv;
        clsUtils utils;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getAllProducts();
            }
        }
        void getAllProducts()
        {
            try
            {
                using (con = new SqlConnection(clsUtils.GetConnection()))
                {
                    con.Open();
                    cmd = new SqlCommand("sp_Product", con);
                    cmd.Parameters.AddWithValue("@Action", "ActiveProduct");
                    cmd.CommandType = CommandType.StoredProcedure;
                    adapter = new SqlDataAdapter(cmd);
                    tbl = new DataTable();
                    adapter.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                    {
                        rProducts.DataSource = tbl;
                    }
                    else
                    {
                        rProducts.DataSource = tbl;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate=new CustomTemlate(ListItemType.Footer);
                    }
                    rProducts.DataBind();
                    Session["product"] = tbl;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ ex.Message+ "')</script>");
            }
        }
        private sealed class CustomTemlate : ITemplate
        {
            private ListItemType ListItemType {  get; set; }
            public CustomTemlate(ListItemType listItemType)
            {
                ListItemType = listItemType;
            }

            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<b>No Product da desplay,</b>");
                    container.Controls.Add(footer);
                    
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            tbl = (DataTable)Session["product"];
            if(tbl!=null)
            {
                if (tbl.Rows.Count > 0)
                {
                    dv = new DataView(tbl);
                    var search = txtSearchInput.Value.Trim().Replace("'", "''");
                    dv.RowFilter = $"ShortDescription LIKE '%{search}%'";
                    if (dv.Count > 0)
                    {
                        rProducts.DataSource = dv;
                    }
                    else
                    {
                        rProducts.DataSource = dv;
                        rProducts.FooterTemplate = null;
                        rProducts.FooterTemplate=new CustomTemlate(ListItemType.Footer);
                    }
                }
                else
                {
                    rProducts.DataSource = dv;
                    rProducts.FooterTemplate = null;
                    rProducts.FooterTemplate = new CustomTemlate(ListItemType.Footer);
                }
                rProducts.DataBind();
            }
        }
    }
}