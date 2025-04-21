<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="requests.aspx.cs" Inherits="WaterSupportService.backend.requests" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="قيد المراجعة" Visible="true" OnClick="btn_underReview_Click" />
                <asp:Button ID="btn_underAuditing" CssClass="red-button" runat="server" Text="قيد التدقيق" Visible="true" OnClick="btn_underAuditing_Click"/>
                <asp:Button ID="btn_underProcess" CssClass="red-button" runat="server" Text="قيد الإجراء" Visible="true" OnClick="btn_underProcess_Click" />
                <asp:Button ID="btn_allRequests" CssClass="red-button" runat="server" Text="جميع الطلبات" Visible="true" OnClick="btn_allRequests_Click" />
            </div>

        </div>
    </div>

    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                طلبات تحت المراجعة
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
                        <dx:GridViewDataColumn Caption="ضان" FieldName="SHEEPS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ماعز" FieldName="GOATS"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="خيول" FieldName="HORSES"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="غزلان" FieldName="GAZALS"></dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="حجم المياه - جالون" FieldName="TOTAL_WATER"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="اجمالي الدعم" FieldName="TOTAL_SUPPORT"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="BANK_PROFILE" Visible="false"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WATER_CERT_PROFILE" Visible="false"></dx:GridViewDataColumn>
                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
    <asp:Panel ID="panel_action" runat="server" Visible="false">
        <div class="centered-div">
            <div class="table-container">
                <div class="rtl-div bold" style="color: orangered;">
                    بيانات المستفيد المسجلة في نظام مواقع وأعداد الثروة الحيوانية و نظام معلومات المزارع (حالياً)  
                </div>
                <div style="text-align: center;">
                    <asp:Table ID="tbl_ownerships" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="1200px" Font-Bold="true">
                        <asp:TableRow>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px"> رقم الاستمارة</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">نوع الترخيص</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">الموقع</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الابار</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">صلاحية المزرعة</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ اخر حصر</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">ابل</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">ابقار</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">ضان</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">ماعز</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">خيول</asp:TableCell>
                            <asp:TableCell BorderStyle="Solid" BorderWidth="1px">غزلان</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
        <div class="centered-div">
            <div class="table-container">
                <div class="rtl-div bold" style="width: 1377px;">
                    إجراء الطلب
                </div>
                <div style="text-align: right;">
                    <table>
                        <tr>
                            <td>شهادة الـ IBAN :</td>
                            <td>
                                <asp:LinkButton ID="lb_view_iban_cert" runat="server" OnClick="lb_view_iban_cert_Click">عرض</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td>شهادة تحلية المياه:</td>
                            <td>
                                <asp:LinkButton ID="lb_view_water_cert" runat="server" OnClick="lb_view_water_cert_Click">عرض</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td>رقم الحساب:</td>
                            <td>
                                <asp:TextBox ID="txt_iban8" Enabled="false" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban7" Enabled="false" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban6" Enabled="false" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban5" Enabled="false" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban4" Enabled="false" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban3" Enabled="false" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban2" Enabled="false" runat="server" Width="25px" MaxLength="2"></asp:TextBox>
                                -
                            <asp:TextBox ID="txt_iban1" Enabled="false" runat="server" Width="25px" MaxLength="2"></asp:TextBox></td>
                            <td>
                                <asp:LinkButton ID="lb_save_iban" Enabled="false" runat="server" OnClick="lb_save_iban_Click">حفظ</asp:LinkButton></td>
                        </tr>
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
                                    <asp:TextBox TextMode="MultiLine" ID="txt_reject_reson" runat="server" Width="500px" Height="100px"></asp:TextBox>

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
