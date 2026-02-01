<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="EcommerceSite.Admin.Product" %>

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
                var reader = new FileReader();
                reader.onload = function (e) {
                    var controlName = input.id.substr(input.id.indexOf("_") + 1);
                    if (controlName == 'fuFirstImage') {
                        $('#<%=imageProduct1.ClientID%>').show();
                        $('#<%=imageProduct1.ClientID%>').prop('src', e.target.result)
                            .width(200)
                            .height(200);
                    }
                    else if (controlName == 'fuSecondImage') {
                        $('#<%=imageProduct2.ClientID%>').show();
                         $('#<%=imageProduct2.ClientID%>').prop('src', e.target.result)
                             .width(200)
                             .height(200);
                    }
                    else if (controlName == 'fuThirdImage') {
                        $('#<%=imageProduct3.ClientID%>').show();
                          $('#<%=imageProduct3.ClientID%>').prop('src', e.target.result)
                              .width(200)
                              .height(200);
                    }
                    else if (controlName == 'fuFourthImage') {
                        $('#<%=imageProduct4.ClientID%>').show();
                           $('#<%=imageProduct4.ClientID%>').prop('src', e.target.result)
                               .width(200)
                               .height(200);
                       }
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Product
                    </h4>
                    <hr>
                    <div class="form-body">

                        <div class="row">
                            <div class="col-md-6">
                                <label>Product Name</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtProductName" CssClass="form-control" placeholder="Enter Product Name" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvProductName" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Category</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                       >
                                        <asp:ListItem Value="0">Select Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategory" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="ddlCategory" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Sub-Category</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlSubCategory" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem Value="0">Select Sub-Category</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSubCategory" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="ddlSubCategory"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Product Price</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtProductPrice" CssClass="form-control" placeholder="Enter Product Price" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvProductPrice" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtProductPrice"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revProductPrice" runat="server" ControlToValidate="txtProductPrice"
                                        ValidationExpression="\d+(?:.\d{1,2})?" ErrorMessage="Product Price Invalid" Font-Size="Smaller" SetFocusOnError="true" ForeColor="Red"
                                        Display="Dynamic">  </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Color</label>
                                <div class="form-group">
                                    <asp:ListBox ID="lboxColor" runat="server" CssClass="fore-control" SelectionMode="Multiple"
                                        ToolTip="Use CTRL Key to select multiple items">
                                        <asp:ListItem Value="1">Blue</asp:ListItem>
                                        <asp:ListItem Value="2">Red</asp:ListItem>
                                        <asp:ListItem Value="3">Pink</asp:ListItem>
                                        <asp:ListItem Value="4">Purple</asp:ListItem>
                                        <asp:ListItem Value="5">Brown</asp:ListItem>
                                        <asp:ListItem Value="6">Gray</asp:ListItem>
                                        <asp:ListItem Value="7">Green</asp:ListItem>
                                        <asp:ListItem Value="8">Yellow</asp:ListItem>
                                        <asp:ListItem Value="9">White</asp:ListItem>
                                        <asp:ListItem Value="10">Black</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Size</label>
                                <div class="form-group">
                                    <asp:ListBox ID="lboxSize" runat="server" CssClass="fore-control" SelectionMode="Multiple"
                                        ToolTip="Use CTRL Key to select multiple items">
                                        <asp:ListItem Value="1">XS</asp:ListItem>
                                        <asp:ListItem Value="2">SM</asp:ListItem>
                                        <asp:ListItem Value="3">M</asp:ListItem>
                                        <asp:ListItem Value="4">L</asp:ListItem>
                                        <asp:ListItem Value="5">XL</asp:ListItem>
                                        <asp:ListItem Value="6">XLL</asp:ListItem>

                                    </asp:ListBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Quantity</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtQuantity" CssClass="form-control" placeholder="Enter Product Qty" TextMode="Number" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuantity" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtQuantity"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Company Name:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtCompanyName" CssClass="form-control" placeholder="Enter Company Name"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCompanyName" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Short Description:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtShortdescription" CssClass="form-control" placeholder="Enter Short Description" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvShortdescription" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtShortdescription"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Long Description:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtLongDescription" CssClass="form-control" placeholder="Enter Long Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLongDescription" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtLongDescription"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Additional Description:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtAdditionalDescription" CssClass="form-control" placeholder="Enter Long Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAdditionalDescription" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtAdditionalDescription"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Tags (Search keyword):</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtTags" CssClass="form-control" placeholder="Enter Tags" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTags" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="txtTags"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Product Image 1:</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuFirstImage" CssClass="form-control" ToolTip=" .jpg, .png, .jpeg" runat="server"
                                        onChange="ImagePreview(this)" />

                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Product Image 2:</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuSecondImage" CssClass="form-control" ToolTip=" .jpg, .png, .jpeg" runat="server" onChange="ImagePreview(this)" />

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Product Image 3:</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuThirdImage" CssClass="form-control" ToolTip=" .jpg, .png, .jpeg" runat="server" onChange="ImagePreview(this)" />

                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Product Image 4:</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuFourthImage" CssClass="form-control" ToolTip=" .jpg, .png, .jpeg" runat="server" onChange="ImagePreview(this)" />

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Default Image</label>
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rblDefaultImage" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">&nbsp; First &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp; Second &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="3">&nbsp; Third &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="4">&nbsp; Fourth &nbsp;</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvDefaultImage" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true"
                                        runat="server" ErrorMessage="Required" ControlToValidate="rblDefaultImage"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hfDefImagePos" runat="server" Value="0" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Customized</label>
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsCustomized" runat="server" Text="&nbsp; IsCustomized" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Active</label>
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 align-content-sm-between pl-3">
                                <span>
                                    <asp:Image ID="imageProduct1" runat="server" CssClass="img-thumbnail" AlternateText="" Style="display: none" />
                                </span>
                                <span>
                                    <asp:Image ID="imageProduct2" runat="server" CssClass="img-thumbnail" AlternateText="" Style="display: none" />
                                </span>
                                <span>
                                    <asp:Image ID="imageProduct3" runat="server" CssClass="img-thumbnail" AlternateText="" Style="display: none" />
                                </span>
                                <span>
                                    <asp:Image ID="imageProduct4" runat="server" CssClass="img-thumbnail" AlternateText="" Style="display: none" />
                                </span>
                            </div>
                        </div>
                        <%-- <label>Category Image</label>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuCategoryImage" CssClass="form-control" runat="server"
                                        onchange="ImagePreview(this);" />
                                    <asp:HiddenField ID="hfCategoryID" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>
                        <div class="=row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" />
                                </div>
                            </div>
                        </div>--%>
                    </div>

                     <div class="form-actiona pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add"  CausesValidation="True" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" CausesValidation="True" OnClick="btnClear_Click" />
                        </div>
                    </div>
                    <div>
                        

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




