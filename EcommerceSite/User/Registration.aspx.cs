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

namespace EcommerceSite.User
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        ProductDAL productDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnRegisterOrUpdate_Click(object sender,EventArgs e)
        {
            string actionName=string.Empty, imagePath=string.Empty, fileExtention=string.Empty;
            bool isValidToExcute = false;
            int userId=Convert.ToInt32(Request.QueryString["id"]);
            con=new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_User", con);
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@Postcode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if(clsUtils.IsValidExtention(fuUserImage.FileName))
                {
                    string newImageName = clsUtils.GetUniqueID();
                    fileExtention=Path.GetExtension(fuUserImage.FileName).Trim();
                    imagePath = "Images/User/" + newImageName.ToString() + fileExtention;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User") + newImageName.ToString() + fileExtention);
                    cmd.Parameters.AddWithValue("@ImageUrl",imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    lblMsg.Visible= false;
                    lblMsg.Text = "";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExcute= false;
                }
                
            }
            else
            {
                isValidToExcute = true;
            }
            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ? "  registration is succefull <b><a href='Login.aspx'>Click'here</a></b> to do login" :
                        "detail updated successfull <b><a href='Profile.aspx'>Can check here</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b>" + actionName;
                    lblMsg.CssClass = "alert alert-success";
                    if (userId != 0)
                    {
                        Response.AddHeader("REFRESH", "3;URL=Profile.aspx");
                    }
                }
                catch(SqlException ex)
                {
                    if(ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> username already exist, try other one..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                catch(Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }

        }
        void clear()
        {
            txtName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPostCode.Text = string.Empty;
        }
    }
}