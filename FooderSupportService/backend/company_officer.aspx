<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="company_officer.aspx.cs" Inherits="FooderSupportService.backend.company_officer" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="الطلبات مستحقة الصرف" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button" runat="server" Text="تقرير التسليم" Visible="true" OnClick="btn_allRequests_Click" />
            </div>

        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                الطلبات مستحقة الصرف
            </div>
            <div style="text-align: center;">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Default" Width="1400px" KeyFieldName="REQ_ID" AutoGenerateColumns="False" RightToLeft="True" OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFilterRow="True" ShowFooter="true" />

                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                    </TotalSummary>

                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="اختيار" VisibleIndex="0" CellStyle-BackColor="#F5F5F5">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn Caption="رقم المرجع" FieldName="REF"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلب" FieldName="REQ_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>

                        <dx:GridViewDataColumn Caption="الرقم الشخصي" FieldName="QID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الأسم" FieldName="NAME" Width="300PX"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الجوال" FieldName="MOBILE"></dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="مركز 16 بروتين (كيس)" FieldName="TOTAL_PROTEIN_F_BAG"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="عشبية (كيس)" FieldName="TOTAL_GRASS_F_BAG"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ بداية الأستحقاق" FieldName="SUPPORT_START">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ نهاية الأستحقاق" FieldName="SUPPORT_END">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>

    <div class="centered-div">
        <div class="table-container" style="font-size: large">
            <div class="rtl-div bold" style="width: 1377px;">
                إجراء الطلب
            </div>
            <div style="text-align: right;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_send_otp" CssClass="red-button" runat="server" Text="ارسال رمز التحقق" Visible="true" OnClick="btn_send_otp_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>رمز التحقق:</td>
                        <td>
                            <asp:TextBox ID="txt_otp" Width="100" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_save" CssClass="red-button" runat="server" Text="اتمام تسليم الأعلاف" Visible="true" OnClick="btn_save_Click" /></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lbl_message" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
