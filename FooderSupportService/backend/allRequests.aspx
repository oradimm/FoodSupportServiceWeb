<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="allRequests.aspx.cs" Inherits="FooderSupportService.backend.allRequests" %>

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
                <asp:Button ID="btn_underReview" CssClass="red-button" runat="server" Text="قيد المراجعة" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_underAuditing" CssClass="red-button" runat="server" Text="قيد التدقيق" Visible="true" OnClick="btn_underAuditing_Click"/>
                <asp:Button ID="btn_underProcess" CssClass="red-button" runat="server" Text="قيد الإجراء" Visible="true" OnClick="btn_underProcess_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="جميع الطلبات" Visible="true" OnClick="btn_allRequests_Click" />
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
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" EnableCallBacks="false" Theme="Default"  Width="1600PX" KeyFieldName="REQ_ID" AutoGenerateColumns="False" RightToLeft="True" OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFooter="true" ShowFilterRow="True" ShowGroupPanel="true" ShowFilterBar="Visible" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="600" />
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="QID" ShowInColumn="QID" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_WATER" ShowInColumn="TOTAL_WATER" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_SUPPORT" ShowInColumn="TOTAL_SUPPORT" />
                    </GroupSummary>
                     <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_WATER" ShowInColumn="TOTAL_WATER" />
                        <dx:ASPxSummaryItem SummaryType="Sum" FieldName="TOTAL_SUPPORT" ShowInColumn="TOTAL_SUPPORT" />
                    </TotalSummary>
                    <SettingsPager PageSize="50"></SettingsPager>
                    <Columns>
                        <dx:GridViewDataColumn Caption="رقم الاجتماع" FieldName="COMMITTEE_SN"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم المرجع" FieldName="REF"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلب" FieldName="REQ_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="الرقم الشخصي" FieldName="QID"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="الأسم" FieldName="NAME" Width="220px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الجوال" FieldName="MOBILE"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="رقم الحساب" FieldName="IBAN" Width="220"></dx:GridViewDataColumn>
                       
                        <dx:GridViewDataColumn Caption="شهادة IBAN" FieldName="IBAN_FILE" Width="100">
                            <DataItemTemplate>
                                <dx:ASPxButton Width="80" ID="btn_view_iban" runat="server" Text="عرض" OnInit="btn_view_iban_Init"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        
                        <dx:GridViewDataColumn Caption="شهادة التحلية" FieldName="CERT_FILE" Width="100">
                            <DataItemTemplate>
                                <dx:ASPxButton Width="80" ID="btn_view_water_cert" runat="server" Text="عرض" OnInit="btn_view_water_cert_Init"></dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        
                        <dx:GridViewDataColumn Caption="حالة المراجعة" FieldName="REVIEW_STATUS"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ المراجعة" FieldName="REVIEWED_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="مراجعة من قبل" FieldName="REVIEWED_BY"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="م. المراجعة" FieldName="REVIEW_NOTES" Width="220px"></dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="حالة التدقيق" FieldName="AUDIT_STATUS"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ التدقيق" FieldName="AUDITED_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="تدقيق من قبل" FieldName="AUDITED_BY"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="م. التدقيق" FieldName="AUDITING_NOTES" Width="220px"></dx:GridViewDataColumn>
                        
                      
                        <dx:GridViewDataColumn Caption="حالة موافقة اللجنة" FieldName="APPROVAL_STATUS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="م. موافقة اللجنة" FieldName="APPROVAL_NOTES"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ موافقة اللجنة" FieldName="APPROVAL_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn Caption="موافقة اللجنة من قبل" FieldName="APPROVAL_BY" Width="140px"></dx:GridViewDataColumn>
                        
                        <dx:GridViewDataColumn Caption="حجم المياه - جالون" FieldName="TOTAL_WATER" Width="140px"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="اجمالي الدعم" FieldName="TOTAL_SUPPORT"></dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="ابل" FieldName="CAMELS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ابقار" FieldName="COWS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ضأن" FieldName="SHEEPS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ماعز" FieldName="GOATS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="خيل" FieldName="HORSES"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="غزلان" FieldName="GAZALS"></dx:GridViewDataColumn>
                       


                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
</asp:Content>
