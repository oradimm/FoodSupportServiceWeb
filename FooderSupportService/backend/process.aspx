<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="process.aspx.cs" Inherits="FooderSupportService.backend.process" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" runat="server" Text="قيد المراجعة" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_underAuditing" CssClass="red-button" runat="server" Text="قيد التدقيق" Visible="true" OnClick="btn_underAuditing_Click"/>
                <asp:Button ID="btn_underProcess" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="قيد الإجراء" Visible="true" OnClick="btn_underProcess_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button"  runat="server" Text="جميع الطلبات" Visible="true" OnClick="btn_allRequests_Click" />
            </div>
        </div>
    </div>

    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                طلبات تحت الاجراء
            </div>
            <div style="text-align: center;">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" EnableCallBacks="false" Theme="Default" Width="1400px" KeyFieldName="REQ_ID" AutoGenerateColumns="False" RightToLeft="True" OnSelectionChanged="ASPxGridView1_SelectionChanged" OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFilterRow="True" ShowFooter="true"  />
                    
                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_WATER" ShowInColumn="TOTAL_WATER" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_SUPPORT" ShowInColumn="TOTAL_SUPPORT" />
                    </TotalSummary>
                    <Columns>
                        <dx:GridViewDataColumn Caption="رقم المرجع" FieldName="REF"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلب" FieldName="REQ_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="الرقم الشخصي" FieldName="QID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الأسم" FieldName="NAME" Width="300PX"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الجوال" FieldName="MOBILE"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ابل" FieldName="CAMELS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ابقار" FieldName="COWS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="اغنام" FieldName="SHEEPS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ماعز" FieldName="GOATS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="خيل" FieldName="HORSES"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="غزلان" FieldName="GAZALS"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ المراجعة" FieldName="REVIEWED_DATE">
                             <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ التدقيق" FieldName="AUDITED_DATE">
                             <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="حجم المياه - جالون" FieldName="TOTAL_WATER"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="اجمالي الدعم" FieldName="TOTAL_SUPPORT"></dx:GridViewDataColumn>

                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
    <asp:Panel ID="panel_action" runat="server" Visible="false">
        <div class="centered-div">
            <div class="table-container">
                <div class="rtl-div bold" style="width: 1377px;">
                    إجراء الطلب
                </div>
                <div style="text-align: right;">
                    <table>
                        <tr>
                            <td>الإجراء:</td>

                            <td>
                                <asp:RadioButtonList ID="radBtnList_Action" runat="server" CssClass="radioButtonList" OnSelectedIndexChanged="radBtnList_Action_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" Selected="True">موافق عليه</asp:ListItem>
                                    <asp:ListItem Value="2">رفض</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panel_reject_reason" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>سبب الرفض</td>
                                <td>
                                    <asp:TextBox ID="txt_reject_reson" runat="server"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="panel_committee_details" runat="server" Visible="true">
                        <table>
                            <tr>
                                <td>تاريخ انعقاد اللجنة:</td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit_committee_meeting" runat="server" OnValueChanged="ASPxDateEdit_committee_meeting_ValueChanged" AutoPostBack="true"></dx:ASPxDateEdit>

                                </td>
                            </tr>
                            <tr>
                                <td>رقم الإجتماع:</td>
                                <td>


                                    <asp:TextBox ID="txt_committee_sn" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                    -
                                    <asp:TextBox ID="txt_committee_year_month" runat="server" Width="50px" MaxLength="6" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <table>
                        <tr>

                            <td>
                                <asp:Button ID="btn_save" CssClass="red-button" runat="server" Text="اعتماد" Visible="true" OnClick="btn_save_Click" />

                            </td>
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
    </asp:Panel>
</asp:Content>
