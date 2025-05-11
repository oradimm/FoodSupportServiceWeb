<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="section_head.aspx.cs" Inherits="FooderSupportService.backend.section_head" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="طلبات تحت الإجراء" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button" runat="server" Text="جميع الطلبات" Visible="true" OnClick="btn_allRequests_Click" />
            </div>

        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                طلبات تحت الإجراء
            </div>
            <div style="text-align: center;">
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Default" Width="1400px" KeyFieldName="REQ_ID" AutoGenerateColumns="False" RightToLeft="True"  OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior  AllowSelectSingleRowOnly="False" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFilterRow="True" ShowFooter="true" />

                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                    </TotalSummary>

                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="اختيار" VisibleIndex="0"  CellStyle-BackColor="#F5F5F5">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn Caption="رقم المرجع" FieldName="REF"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلب" FieldName="REQ_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="مصدر الطلب" FieldName="REQUEST_SOURCE"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الرقم الشخصي" FieldName="QID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الأسم" FieldName="NAME" Width="300PX"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الجوال" FieldName="MOBILE"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="إجمالي عدد الأغنام (االضأن)" FieldName="SHEEPS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="مركز 16 بروتين (كيس)" FieldName="TOTAL_PROTEIN_F_BAG"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="عشبية (كيس)" FieldName="TOTAL_GRASS_F_BAG"></dx:GridViewDataColumn>
                          <dx:GridViewDataColumn Caption="ملاحظات" FieldName="REVIEW_NOTES"></dx:GridViewDataColumn>
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
                        <td>إجراء الطلبات المحددة:</td>

                        <td>
                            <asp:RadioButtonList ID="radBtnList_Action" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:ListItem Value="1" Selected="True">موافق عليه</asp:ListItem>
                                <asp:ListItem Value="2">رفض</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td>ملاحظات</td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" ID="txt_reject_reson" runat="server" Width="500px" Height="100px"></asp:TextBox>

                        </td>
                    </tr>
                </table>

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
</asp:Content>
