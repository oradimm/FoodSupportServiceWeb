<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="officer.aspx.cs" Inherits="FooderSupportService.backend.officer" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <asp:Button ID="btn_underReview" CssClass="red-button" Font-Bold="true" Font-Underline="true" runat="server" Text="طلبات تحت المراجعة" Visible="true" OnClick="btn_underReview_Click" />
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
                    <Settings ShowFilterRow="True" ShowFooter="true" />

                    <TotalSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" FieldName="REF" ShowInColumn="REF" />
                    </TotalSummary>
                    <Columns>
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
            <div class="table-container" style="font-size:large">
                <div class="rtl-div bold" style="width: 1377px;">
                    إجراء الطلب
                </div>
                <table>
                    <tr>
                        <td>كمية الأعلاف المستحقة (كيلو جرام / رأس ضأن):
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rad_lst_fooders_qty" runat="server" RepeatDirection="Horizontal"  AutoPostBack="true" OnSelectedIndexChanged="rad_lst_fooders_qty_SelectedIndexChanged">
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>إجمالي الأعلاف المستحقة (كيلو جرام)
                        </td>
                    </tr>
                    <tr>
                        <td>مركز 16 بروتين:
                             <asp:Label ID="lbl_TotalProteinFooderKg" runat="server" Text="225" Font-Bold="true"></asp:Label>
                        </td>
                      
                        <td>أعلاف عشبية:
                             <asp:Label ID="lbl_TotalGrassFooderKg" runat="server" Text="225" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                 <table>
                    <tr>
                        <td>إجمالي الأعلاف المستحقة (كيس)
                        </td>
                    </tr>
                    <tr>
                        <td>مركز 16 بروتين:
                             <asp:Label ID="lbl_TotalProteinFooderBag" runat="server" Text="225" Font-Bold="true"></asp:Label>
                        </td>
                      
                        <td>أعلاف عشبية:
                             <asp:Label ID="lbl_TotalGrassFooderBag" runat="server" Text="225" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="text-align: right;">
                    <table>

                        <tr>
                            <td>الإجراء:</td>

                            <td>
                                <asp:RadioButtonList ID="radBtnList_Action" runat="server" CssClass="radioButtonList" RepeatDirection="Horizontal"  AutoPostBack="true">
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
    </asp:Panel>
</asp:Content>
