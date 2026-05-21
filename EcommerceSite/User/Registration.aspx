<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EcommerceSite.User.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%#lblMsg.ClientID%>").style.display = "none";
        }, seconds * 1000);
    };
</script>
<script>
    function ImagePreview(input) {
        if (input.files && input.files[0]) {
            $('#<%=imgUser.ClientID%>').show();
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#<%=imgUser.ClientID%>').prop('src', e.target.result)
                    .width(200)
                    .height(200);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container pt-5">
        <div class="align-self-end mb-4">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="text-center mb-4 pt-5">
            <h2 class="section-title px-5"><span class="px-2">
                <asp:Label ID="lblHeaderMsg" runat="server" Text="User Registration"></asp:Label></span></h2>
        </div>
        <div class="row px-xl-5">

            <div class="col-md-6">

                <div class="contact-form">
                    <div id="success"></div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is required" ControlToValidate="txtName"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter Full Name" ToolTip="Full Name"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="reName" runat="server" ErrorMessage="Name Must be characters" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ControlToValidate="txtName"
                            ValidationExpression="^[a-zA-Z\s]*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Username is required"
                            ControlToValidate="txtUsername" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter Username" ToolTip="Username"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revUsername" runat="server" ErrorMessage="Name Must be characters" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ControlToValidate="txtName"
                            ValidationExpression="^[a-zA-Z\s]*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email is required"
                            ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email" ToolTip="Email"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Must be Email" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ControlToValidate="txtName"
                            ValidationExpression="^[a-zA-Z\s]*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ErrorMessage="Mobile No. is must be 10 digits"
                            ControlToValidate="txtMobile" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"  placeholder="Enter Mobile No." ToolTip="Mobile"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Must be numbers" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtMobile" ValidationExpression="^[0-9]{10}"></asp:RegularExpressionValidator>
                    </div>
                </div>


            </div>
              <div class="col-md-6">

      <div class="contact-form">
          <div class="pb-3">
              <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Address is required" ControlToValidate="txtAddress"
                  ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
              <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Full Address"
                TextMode="MultiLine"  ToolTip="Full Address"></asp:TextBox>
          
          </div>
          <div class="pb-3">
              <asp:RequiredFieldValidator ID="rfvPostCode" runat="server" ErrorMessage="PostCode is required"
                  ControlToValidate="txtPostCode" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
              <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" placeholder="Enter PostCode" ToolTip="PostCode"></asp:TextBox>
              <asp:RegularExpressionValidator ID="revPostCode" runat="server" ErrorMessage="Must be numbers" ForeColor="Red"
                  Display="Dynamic" SetFocusOnError="true" Font-Size="Small" ControlToValidate="txtPostCode"
                  ValidationExpression="^[0-9]{6}$"></asp:RegularExpressionValidator>
          </div>
          <div class="pb-3">
              <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password is required"
                  ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
              <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                  TextMode="Password" placeholder="Enter Password" ToolTip="Password"></asp:TextBox>
              
          </div>
          <div class="pb-3">
             <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" ToolTip="User Image" onChange="ImagePreview(this);" />
          </div>
      </div>
  </div>
            <div class="row pl-4 mb-5">
                <div class="ml-2">
                    <asp:Button ID="btnRegisterOrUpdate" runat="server" Text="Register" CssClass="btn btn-primary py-2 pr-4"
                        OnClick="btnRegisterOrUpdate_Click" />
                    <asp:Label ID="lblAlreadyUser" runat="server" CssClass="pl-3 text-black-100" 
                        Text="Already Registerd? <a href'Login.aspx' class='badge badge-info'>Login here..</a>"></asp:Label>
                 </div>
                <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail ml-2 mt-2" AlternateText="Image" style="display:none;" />
            </div>
        </div>
    </div>
</asp:Content>
