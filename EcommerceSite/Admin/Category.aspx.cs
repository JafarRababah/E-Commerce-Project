using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EcommerceSite.Admin
{

    

    public partial class Category : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breakCumbTitle"] = "Manage Category";
            Session["breakCumbPage"] = "Category";
            if (!IsPostBack)
            {
                GetCategories();
            }
            lblMsg.Visible = false;
            
        }
        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible=false;
            if (e.CommandName == "edit")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Category", con);
                cmd.Parameters.AddWithValue("@Action", "GetByID");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                adapter = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                adapter.Fill(tbl);
                txtCategoryName.Text = tbl.Rows[0]["CategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(tbl.Rows[0]["IsActive"]);
                imagePreview.ImageUrl = string.IsNullOrEmpty(tbl.Rows[0]["CategoryImage"].ToString()) ? "../Images/No_image.png" : "../" + tbl.Rows[0]["CategoryImage"].ToString();
                imagePreview.Height = 200;
                imagePreview.Width = 200;
                hfCategoryID.Value = tbl.Rows[0]["CategoryID"].ToString();
                btnAddOrUpdate.Text = "Update";
            }
            else if (e.CommandName == "delete")
            {
                con = new SqlConnection(clsUtils.GetConnection());
                cmd = new SqlCommand("sp_Category", con);
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@CategoryID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;


                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.Text = "Category Deleted Successfully";
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
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked= false;
            hfCategoryID.Value = "0";
            btnAddOrUpdate.Text = "Add";
            imagePreview.ImageUrl = string.Empty;
        }
        void GetCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "GetAll");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter=new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            rCategory.DataSource = tbl;
            rCategory.DataBind();
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string ActionName= string.Empty, ImagePath = string.Empty,FileExtention=string.Empty;
            bool IsValidExecute= false;
            int CategoryID=Convert.ToInt32(hfCategoryID.Value);
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", CategoryID == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if(fuCategoryImage.HasFile)
            {
                if (clsUtils.IsValidExtention(fuCategoryImage.FileName))
                    {
                    string newImageName = clsUtils.GetUniqueID();
                    FileExtention = Path.GetExtension(fuCategoryImage.FileName);
                    ImagePath = "Images/Category/" + newImageName.ToString() + FileExtention;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + newImageName.ToString() + FileExtention); 
                    cmd.Parameters.AddWithValue("@CategoryImageUrl",ImagePath);
                    IsValidExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "Please Select .jpg, .png or .jpeg images";
                    lblMsg.CssClass = "alert alert-danger";
                    IsValidExecute = false;
                }
                
            }
            else
            {
                IsValidExecute = true;
            }
            if (IsValidExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ActionName = CategoryID == 0 ? "Inserted" : "Updated";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Category " + ActionName + " Successfully";
                    lblMsg.CssClass = "alert alert-success";
                    GetCategories();
                    Clear();
                }
                catch (Exception ex) {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally { con.Close(); }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
    }
}