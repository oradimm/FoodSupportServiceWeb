<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyRequests.aspx.cs" Inherits="FooderSupportService.MyRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div" style="padding-bottom:50px;">
        <asp:Button ID="btn_new_request" CssClass="red-button" runat="server" Text="طلب جديد" Visible="true" OnClick="btn_new_request_Click" />
    </div>
    <div class="centered-div" runat="server" id="errorBox" visible="false">
        <div class="table-container">
             <div class="error-box">
        <p><strong>
            <asp:Label ID="lbl_error_msg" runat="server" Text=""></asp:Label></strong></p>
    </div>
        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                طلباتي
            </div>
            <div style="text-align: center;">
                <asp:Table ID="tbl_myRequests" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="1200px" Font-Bold="true" >
                    <asp:TableRow>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="0 px" ColumnSpan="3" ></asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1 px" ColumnSpan="2">كمية العلف (كيس)</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="0 px" ColumnSpan="4"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px" >رقم الطلب</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ الطلب</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الاغنام</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">16 بروتين</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عشبي</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ الأستحقاق</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ انتهاء الصرف</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">حالة الطلب</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">ملاحظات</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>
   
</asp:Content>
