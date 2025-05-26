<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommitteeReportAnix.aspx.cs" Inherits="FooderSupportService.backend.reports.CommitteeReportAnix" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="../../Assets/css/StyleSheetReferance.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="centered-div">
            <div class="table-container" style="padding: 0px;">
                <div class="rtl-div bold">
                    <table style="text-align: center; width: 1900px;">
                        <tr>
                            <td>
                                <img src="../../Assets/images/MME-logo.png" />
                            </td>
                            <td>لجنة تقدير احتياجات أصحاب مزارع الثروة الحيوانية من المياه
                           <br />
                                محضر الاجتماع رقم (<asp:Label ID="lbl_sn" runat="server" Text="Label"></asp:Label>)
                            </td>
                        </tr>
                        <tr>
                            <td></td>

                            <td>تاريخ الطلبات
                            من:
                           
                            
                                <asp:Label ID="lbl_from" runat="server" Text="Label"></asp:Label>

                                الى:
                            
                           
                                <asp:Label ID="lbl_to" runat="server" Text="Label"></asp:Label>
                            </td>

                        </tr>

                    </table>
                </div>
            </div>
        </div>
        <div class="centered-div" style="margin-top: 0px">
            <div class="table-container" style="padding: 0px">
                <div style="font-weight: bold; font-size: 20px;">
                    كشف المستفيدين من دعم المياه:
                </div>
                <div style="text-align: center;">
                    <asp:Table ID="tbl_requests" runat="server" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" Width="1500px" Font-Bold="true">
                        <asp:TableRow>
                            <asp:TableCell>م</asp:TableCell>
                            <asp:TableCell>رقم الطلب</asp:TableCell>
                            <asp:TableCell>اسم المستفيد</asp:TableCell>
                            <asp:TableCell>الرقم الشخصي</asp:TableCell>
                            <asp:TableCell>اسم البنك</asp:TableCell>
                            <asp:TableCell>رقم الحساب</asp:TableCell>
                            <asp:TableCell>ابل</asp:TableCell>
                            <asp:TableCell>بقر</asp:TableCell>
                            <asp:TableCell>ضان</asp:TableCell>
                            <asp:TableCell>ماعز</asp:TableCell>
                            <asp:TableCell>خيول</asp:TableCell>
                            <asp:TableCell>غزلان</asp:TableCell>
                            <asp:TableCell>كمية المياه بالجالون</asp:TableCell>
                            <asp:TableCell>المبلغ المستحق</asp:TableCell>

                        </asp:TableRow>
                    </asp:Table>
                    <div class="table-container">
                        <div class="rtl-div bold">
                            <table style="text-align: center; width: 100%">
                                <tr>

                                    <td>عضو
                                    </td>
                                    <td>عضو
                                    </td>
                                    <td>عضو
                                    </td>
                                    <td>عضو
                                    </td>
                                    <td>عضو
                                    </td>

                                    <td>نائب الرئيس
                                    </td>
                                    <td>الرئيس
                                    </td>
                                </tr>
                                <tr>

                                    <td>مبارك حمد الهاجري
                                    </td>
                                    <td>ناصر حسن النعيمي
                                    </td>
                                    <td>محمد حمد النعيمي
                                    </td>
                                    <td>حمد ابراهيم المرزوقي
                                    </td>
                                    <td>سيف احمد عثمان
                                    </td>
                                    <td>حمد ساكت الشمري
                                    </td>
                                    <td>عبد العزيز محمود الزيارة
                                    </td>
                                </tr>
                                <tr>

                                    <td>ممثل مربي الثروة الحيوانية
                                    </td>
                                    <td>ممثل مربى الثروة الحيوانية
                                    </td>

                                    <td>ممثل الكهرباء والماء	
                                    </td>
                                    <td>ممثل وزارة المالية
                                    </td>
                                    <td>ممثل ديوان المحاسبة	
                                    </td>

                                    <td>مدير ادارة البحوث الزراعية	
                                    </td>
                                    <td>مدير إدارة الثروة الحيوانية
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
