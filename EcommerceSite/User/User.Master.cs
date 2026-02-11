using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                //load control 
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(sliderUserControl);
            }
            if (!IsPostBack)
            {
                getNestedCategory();
            }
        }
        private void getNestedCategory()
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
        protected void rCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType==ListItemType.Item|| e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField categoryId = e.Item.FindControl("hfCategoryID") as HiddenField;
                Repeater repSubCategory = e.Item.FindControl("rSubCategory") as Repeater;
                con=new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_SubCategory", con);
                cmd.Parameters.AddWithValue("@Action", "ActiveSubCategory");
                cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(categoryId.Value));
                cmd.CommandType = CommandType.StoredProcedure;
                adapter=new SqlDataAdapter(cmd);
                tbl= new DataTable();
                adapter.Fill(tbl);
                if (tbl.Rows.Count > 0)
                {
                    repSubCategory.DataSource = tbl;
                    repSubCategory.DataBind();
                }
            }
        }
    }
}