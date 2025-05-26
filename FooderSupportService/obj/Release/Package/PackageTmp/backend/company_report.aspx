<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="company_report.aspx.cs" Inherits="FooderSupportService.backend.company_report" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OnViewDocBtnClick(image) {
            window.open('https://dotnet.mme.gov.qa/AttachmentsDownloadManager/GetAttachmentProd?DocID=' + image);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" runat="server" Text="الطلبات مستحقة الصرف" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="تقرير التسليم" Visible="true" OnClick="btn_allRequests_Click" />
            </div>
        </div>
    </div>

    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                جميع الطلبات
            </div>
            <div>
                <asp:LinkButton ID="btn_export" runat="server" OnClick="btn_export_Click">تصدير اكسل</asp:LinkButton>
            </div>
            <div style="text-align: center;">
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"></dx:ASPxGridViewExporter>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" EnableCallBacks="false" Theme="Default" Width="1137PX" KeyFieldName="REQ_ID" AutoGenerateColumns="False" RightToLeft="True" OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFooter="true" ShowFilterRow="True" ShowGroupPanel="true" ShowFilterBar="Visible" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" />
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="QID" ShowInColumn="QID" />
                    </GroupSummary>
                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_PROTEIN_F_BAG" ShowInColumn="TOTAL_PROTEIN_F_BAG" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_GRASS_F_BAG" ShowInColumn="TOTAL_GRASS_F_BAG" />
                    </TotalSummary>
                    <SettingsPager PageSize="50"></SettingsPager>
                    <Columns>

                        <dx:GridViewDataColumn Caption="رقم المرجع" FieldName="REF"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلب" FieldName="REQ_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="الرقم الشخصي" FieldName="QID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الأسم" FieldName="NAME" Width="220px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الجوال" FieldName="MOBILE"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="مركز 16 بروتين (كيس)" FieldName="TOTAL_PROTEIN_F_BAG" Width="150"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="عشبية (كيس)" FieldName="TOTAL_GRASS_F_BAG" Width="150"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="حالة الأستلام" FieldName="DELIVERY_STATUS"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الاستلام" FieldName="DELIVERED_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
</asp:Content>
