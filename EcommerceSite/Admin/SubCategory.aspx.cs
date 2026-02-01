using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.Admin
{
	public partial class SubCategory : System.Web.UI.Page
	{
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breakCumbTitle"] = "Manage SubCategory";
            Session["breakCumbPage"] = "SubCategory";

            if (!IsPostBack)
            {
                GetCategories();
                GetSubCategories();
            }
            lblMsg.Visible = false;
            
        }
        protected void rSubCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_SubCategory", con);
                cmd.Parameters.AddWithValue("@Action", "GetByID");
                cmd.Parameters.AddWithValue("@SubCategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                adapter.Fill(tbl);
                txtSubCategoryName.Text = tbl.Rows[0]["SubCategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(tbl.Rows[0]["IsActive"]);
                ddlCategory.SelectedValue = tbl.Rows[0]["CategoryID"].ToString() ;
                hfSubCategoryId.Value = tbl.Rows[0]["SubCategoryID"].ToString();
                btnAddOrUpdate.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_SubCategory", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@SubCategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "SubCategory Deleted Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetCategories();
                    Clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally { con.Close(); }
            }
        }
        void Clear()
        {
            txtSubCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfSubCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            ddlCategory.ClearSelection();
        }
        void GetCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "GetAll");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            ddlCategory.DataSource = tbl;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
        }
        void GetSubCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_SubCategory", con);
            cmd.Parameters.AddWithValue("@Action", "GetAll");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            rSubCategory.DataSource = tbl;
            rSubCategory.DataBind();
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string ActionName = string.Empty;
            
            int SubCategoryID = Convert.ToInt32(hfSubCategoryId.Value);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_SubCategory", con);
            cmd.Parameters.AddWithValue("@Action", SubCategoryID == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@SubCategoryID", SubCategoryID);
            cmd.Parameters.AddWithValue("@SubCategoryName", txtSubCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoryID",ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            
           
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ActionName = SubCategoryID == 0 ? "Inserted" : "Updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "SubCategory " + ActionName + " Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetSubCategories();
                    Clear();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally { con.Close(); }
            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }

}