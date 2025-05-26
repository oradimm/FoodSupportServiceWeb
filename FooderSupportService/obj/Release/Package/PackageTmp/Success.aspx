<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="FooderSupportService.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div style="text-align: center; color: #8a1538; font-size: x-large;font-weight: bold;">
                <table>
                    <tr>
                        <td>تم تقديم الطلب بنجاح</td>
                    </tr>
                    <tr>
                        <td>حالة الطلب: تحت المراجعة</td>
                    </tr>
                    <tr>
                        <td>تاريخ تقديم الطلب: <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>رقم الطلب: <asp:Label ID="lbl_requestRefrance" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>في حالة الموافقة او الرفض سيتم ارسال رسالة نصية لرقم الجوال المسجل</td>
                    </tr>
                    <tr>
                        <td style="padding-top:30px;"><asp:Button ID="btn_back" CssClass="red-button" runat="server" Text="عودة" Visible="true" OnClick="btn_back_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
