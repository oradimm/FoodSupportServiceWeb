<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WaterSupportService.Apply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_cb_agree {
            width: 25px;
            height: 25px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold" style="color: forestgreen;">
                بيانات المواقع المستوفية لشروط الخدمة
            </div>
            <div style="text-align: center;">
                <asp:Table ID="tbl_valid_ownerships" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="1200px" Font-Bold="true">
                    <asp:TableRow>

                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">رقم الترخيص</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">نوع الترخيص</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">الموقع</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الابار</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">صلاحية المزرعة</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ اخر حصر</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                بيانات الطلب
            </div>
            <div style="text-align: center;">
                <table>
                    <tr>
                        <td>اسم المستفيد</td>
                        <td>
                            <asp:TextBox ID="txt_name" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                        <td>الرقم الشخصي</td>
                        <td>
                            <asp:TextBox ID="txt_qid" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>اسم البنك</td>
                        <td>
                            <asp:TextBox ID="txt_bank_name" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                        <td>رقم الايبان</td>
                        <td>
                            <asp:TextBox ID="txt_iban" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>رقم الجوال</td>
                        <td>
                            <asp:TextBox ID="txt_mobile" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                        <td>تاريخ الطلب</td>
                        <td>
                            <asp:TextBox ID="txt_applied_Date" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                    </tr>
                    <tr>

                        <td>
                            <asp:CheckBox ID="cb_iban_cert" Text="شهادة الايبان" runat="server" Enabled="false" /></td>
                        <td>
                            <asp:HyperLink ID="hl_iban_cert" runat="server" NavigateUrl="~/Profile.aspx">(ارفاق)</asp:HyperLink></td>
                        <td>
                            <asp:CheckBox ID="cb_water_cert" Text="شهادة تحلية مياه الابار والخاصة بالمزارع فقط" runat="server" Enabled="false" /></td>
                        <td>
                            <asp:HyperLink ID="hl_water_cert" runat="server" NavigateUrl="~/Profile.aspx">(ارفاق)</asp:HyperLink></td>

                    </tr>
                </table>
            </div>
        </div>
    </div>



    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                احتساب كميات المياه و مبالغ الدعم
            </div>
            <div style="text-align: center;">
                <asp:Table ID="tbl_calculations" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="1200px" Font-Bold="true">
                    <asp:TableRow ID="tbl_calculations_header_row">
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">النوع</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">العدد</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">كمبة المياه المحتسبة شهريا بالجالون</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">المبلغ المحتسب (شهري)</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">المبلغ المحتسب (سنوي)</asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Camels_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Camels_cell">ابل</asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Cows_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Cows_cell">بقر</asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Goats_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Goats_cell">ماعز</asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Sheeps_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Sheeps_cell">ضان</asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Horses_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Horses_cell">خيول</asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_Gazals_row" Height="30px" runat="server">
                        <asp:TableCell ID="tbl_calculations_Gazals_cell">غزلان</asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="tbl_calculations_total_row" Height="30px">
                        <asp:TableCell ID="tbl_calculations_total_cell" BorderWidth="1px">المجموع</asp:TableCell>
                        <asp:TableCell ID="tbl_calculations_total_animals_cell" runat="server" BorderWidth="1px"></asp:TableCell>
                        <asp:TableCell ID="tbl_calculations_total_water_cell" runat="server" BorderWidth="1px"></asp:TableCell>
                        <asp:TableCell ID="tbl_calculations_total_monthly_value_cell" runat="server" BorderWidth="1px"></asp:TableCell>
                        <asp:TableCell ID="tbl_calculations_total_yearly_value_cell" runat="server" BorderWidth="1px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>



    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold" style="color: orangered;">
                بيانات المواقع الغير مستوفية لشروط الخدمة
            </div>
            <div style="text-align: center;">
                <asp:Table ID="tbl_not_valid_ownerships" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="1200px" Font-Bold="true">
                    <asp:TableRow>

                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px"> رقم الاستمارة</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">نوع الترخيص</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">الموقع</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الابار</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">صلاحية المزرعة</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">تاريخ اخر حصر</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">الملاحظات</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </div>

    <div class="centered-div">
        <div class="table-container">
            <div style="text-align: right; font-size: x-large;">
                <table>
                    <tr>
                        <td style="font-weight: bold; color: orangered;">معلومات هامة:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ul>
                                <li>هذه الخدمة متاحة للمواقع الموضحة في خانة (بيانات المواقع المستوفية لشروط الخدمة) </li>
                                <li>هذه الخدمة غير متاحة للمواقع الموضحة في خانة (بيانات المواقع الغير مستوفية لشروط الخدمة)</li>
                                <li>يحق للمربي التقديم على خدمة دعم المياه مرة واحدة فقط بالسنة</li>

                            </ul>
                            <ul style="list-style-type: none">

                                <li>
                                    <asp:CheckBox ID="cb_agree" Text="اقر بكامل العلم بصحة جميع المعلومات المقدمة في الطلب" runat="server" AutoPostBack="true" OnCheckedChanged="cb_agree_CheckedChanged" /></li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="centered-div">
        <div class="table-container">
            <div style="text-align: center;">
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btn_save" CssClass="red-button" runat="server" Text="تقديم الطلب" Visible="true" OnClick="btn_save_Click" />
                            <asp:Button ID="btn_back" CssClass="red-button" runat="server" Text="عودة" Visible="true" OnClick="btn_back_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="text-align: right; font-size: x-large; color:orangered; font-weight:bold;">
                                <asp:Label ID="lbl_error_msg" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
