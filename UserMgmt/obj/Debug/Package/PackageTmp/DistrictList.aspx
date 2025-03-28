﻿<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="DistrictList.aspx.cs" Inherits="UserMgmt.DistrictList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">

    <div role="main">
        <br />
        <div class="">
            <div class="row top_tiles">
                <div class="">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="x_panel">
                            <html>
                            <head>
                                <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
                                <title>User Management</title>
                                  <script>
                                    function chkDuplicateAccessTypeName() {
                                        debugger;
                                        var email = $('#BodyContent_grdDistrictList_txtSearch2').val();
                                        var jsondata = JSON.stringify($('#BodyContent_grdDistrictList_txtSearch2').val());
                                        $.ajax({
                                            type: "POST",
                                            //url: "UserRegistrationForm.aspx/chkDuplicateEmail",
                                            url: "DistrictList.aspx/chkDuplicateAccessTypeName",
                                            data: '{name:' + jsondata + '}',
                                            datatype: "application/json",
                                            contentType: "application/json; charset=utf-8",
                                            cache: false,
                                            async: false,
                                            success: function (msg) {

                                                if (parseInt(msg.d) > 0) {
                                                    //alert("AccessType  Name is already exists");
                                                    //$('#BodyContent_txtAccessTypeName').val("");
                                                    //$('#BodyContent_txtAccessTypeName').focus();
                                                }

                                            }
                                        });
                                    }
                                    $(document).ready(function () {
                                        $('#BodyContent_grdDistrictList_ddlsearch1').change(function () {
                                            $('#BodyContent_grdDistrictList_txtSearch2').val('');
                                        });
                                    });
                                    </script>
                            </head>
                            <body>
                                <div>
                                    <ul class="nav nav-tabs">
                                        <li>
                                            <asp:LinkButton ID="StateMaster" OnClick="StateMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">State Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="DivisionMaster" OnClick="DivisionMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Division Master</span></asp:LinkButton></li>
                                        <li class="active">
                                            <asp:LinkButton ID="DistrictMaster" OnClick="DistrictMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">District Master</span></asp:LinkButton></li>
                                           <li>
                                            <asp:LinkButton ID="ThanaMaster" OnClick="ThanaMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Thana Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleLevelMaster" OnClick="RoleLevelMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Level Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="AccessTypeMaster" OnClick="AccessTypeMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Access Type Master</span></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="RoleMaster" OnClick="RoleMaster_Click" runat="server"><span style="color:#fff;font-size:14px;">Role Master</span></asp:LinkButton></li>
                                    </ul>
                                    <br />
                                    <asp:LinkButton runat="server" CssClass="myButton3 " ID="AddRecord" Style="float: right" OnClick="AddRecord_Click"><i class="fa fa-plus-circle"> ADD NEW RECORD</i></asp:LinkButton>

                                    <div class="row">

                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <div class="x_panel">
                                                <div class="x_title">
                                                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                    <h2>District List</h2>
                                                    <div runat="server" visible="false"   style="float:right">
                                                         <asp:TextBox ID="txtSearch" runat="server" Width="250px" AutoComplete="off"   Height="30px" placeholder="Search District Name"  AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" ></asp:TextBox>
                                                       <span><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></span> 
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                                 <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                <div class="x_content">
                                                    <asp:GridView ID="grdDistrictList" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdDistrictList_PageIndexChanging" OnDataBound="GridView1_DataBound"
                                                        HeaderStyle-BackColor="#26b8b8" HeaderStyle-ForeColor="#ECF0F1" class="table table-striped responsive-utilities jambo_table">
                                                           <PagerSettings FirstPageImageUrl="~/img/icons8-first-50.png" Position="Top" LastPageImageUrl="~/img/icons8-last-50.png" Mode="NextPreviousFirstLast"  NextPageImageUrl="~/img/icons8-next-50.png" PreviousPageImageUrl="~/img/icons8-previous-50.png" />
                                                        <PagerTemplate>
                                                             <asp:DropDownList ID="ddlsearch1" runat="server" Width="150px" Font-Bold="true" Height="25px" ForeColor="Black" Font-Size="12px" style="float:left">
                                                 <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                                  <asp:ListItem Text="District Name" Value="district_name"></asp:ListItem>
                                                <asp:ListItem Text="District Code" Value="district_code"></asp:ListItem>
                                                                 <asp:ListItem Text="Division Name" Value="division_name"></asp:ListItem>
                                                                 <asp:ListItem Text="State Name" Value="state_name"></asp:ListItem>
                                                            </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="txtSearch2" style="float:left ;margin-left:10px" runat="server" Width="200px" AutoComplete="off" Font-Bold="true" CssClass="form-control"  Height="25px" ForeColor="Black" onchange="chkDuplicateAccessTypeName();"  ></asp:TextBox>
                                                       <span><asp:Button ID="Button2" runat="server" style="float:left; margin-left:10px"   Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></span> 
                                                                    <span><asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" CssClass="btn btn-primary left"><i class="fa fa-refresh"> </i></asp:LinkButton></span> 
     
 
                                                              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page"  CommandArgument="First" CssClass="myButton1"><i class="fa fa-step-backward"> </i></asp:LinkButton>
                                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="myButton1"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                <asp:ImageButton ID="btnFirst" runat="server" Width="30px" Height="20px" CommandArgument="First" Visible="false" CommandName="Page"  BackColor="Blue" ForeColor="White"
                    ImageUrl="~/img/icons8-first-50.png" /> <asp:ImageButton ID="btnPrev" runat="server" Visible="false"
                        CommandArgument="Prev" CommandName="Page" Width="30px" Height="20px" BackColor="Blue" ImageUrl="~/img/icons8-previous-50.png" /> <asp:DropDownList
                            ID="DDLPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLPage_SelectedIndexChanged" Visible="false"  Width="250px" ForeColor="Black" Font-Bold="true">
                        </asp:DropDownList>&nbsp;&nbsp; <asp:TextBox ID="txtpage" runat="server" Height="20px" AutoPostBack="true" TextMode="Number" ForeColor="Black" Width="50px" Font-Bold="true" OnTextChanged="txtpage_TextChanged"></asp:TextBox> <asp:Label ID="lblCurrent" Visible="false" runat="server"></asp:Label>
                of
              <asp:Label ID="lblPages" runat="server" Height="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page"  CommandArgument="Next" CssClass="myButton1"><i class="fa fa-chevron-right"></i></asp:LinkButton>
                                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page"  CommandArgument="Last" CssClass="myButton1"><i class="fa fa-step-forward"> </i></asp:LinkButton>
                                                            
                <asp:ImageButton ID="btnNext" Visible="false"
                    runat="server" CommandArgument="Next" Width="30px" Height="20px" CommandName="Page" ForeColor="Blue" BackColor="Blue" ImageUrl="~/img/icons8-next-50.png"  /> <asp:ImageButton
                        ID="btnLast" runat="server" CommandArgument="Last" Width="30px" Visible="false" Height="20px" BackColor="Blue" CommandName="Page" ImageUrl="~/img/icons8-last-50.png" />
            </PagerTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="District Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDistrictName" runat="server" Visible="true" Text='<%#Eval("District_Name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="District Code"  ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDistrictCode" runat="server" Visible="true" Text='<%#Eval("District_Code") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionName" runat="server" Visible="true" Text='<%#Eval("Division_Name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State Name" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStateName" runat="server" Visible="true" Text='<%#Eval("State_Name") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Division Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDivisionCode" runat="server" Visible="true" Text='<%#Eval("Division_Code") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Chemical Lab District Id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllabid" runat="server" Visible="true" Text='<%#Eval("tab_district_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State Code" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStateCode" runat="server" Visible="true" Text='<%#Eval("State_code") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="id" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Width="20px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblid" runat="server" Visible="true" Text='<%#Eval("id") %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="View" ID="btnView" CssClass="myButton" runat="server" OnClick="btnView_Click" CommandName="View"><i class="fa fa-search-plus">
                                                                                    </i> 
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton Text="Edit" ID="btnEdit" CssClass="myButton1" runat="server" OnClick="btnEdit_Click" CommandName="Edit"><i class="fa fa-pencil-square-o"> 
                                                                                    </i> 
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                       <HeaderStyle BackColor="#26B8B8" ForeColor="#ECF0F1" BorderStyle="Solid" BorderWidth="2px" Height="25px" HorizontalAlign="Center"></HeaderStyle>
                                                        <PagerStyle ForeColor="#8C4510"  HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#26B8B8" BorderWidth="2px" Height="5px" HorizontalAlign="Right" ForeColor="#ECF0F1" VerticalAlign="Middle" Font-Size="Medium" Font-Bold="True"  />

                                                        <RowStyle BackColor="Window" BorderStyle="Solid" BorderWidth="2px" Height="25px"></RowStyle>
                                                    </asp:GridView>
                                                </div></ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>
                                </div>



                            </body>
                            </html>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
