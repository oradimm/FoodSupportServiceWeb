<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommiteeReport.aspx.cs" Inherits="FooderSupportService.backend.reports.CommiteeReport" %>

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
            <div class="table-container">
                <div class="rtl-div bold">
                    <table style="text-align: center; width: 1200px;">
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


        <div class="centered-div">
            <div class="table-container">
                <div style="text-align: center;">

                    <table style="text-align: right; width: 1000px; font-size: 20px">
                        <tr>
                            <td colspan="3">
                                <span style="font-size: 20px; font-weight: bold;">عقدت اللجنة اجتماعها يوم
                                    <asp:Label ID="lbl_meeting_day" runat="server" Text="Label"></asp:Label>
                                    الموافق تاريخ
                                    <asp:Label ID="lbl_meeting_date" runat="server" Text="Label"></asp:Label>
                                    م بمقر اللجنة                           بإدارة الثروة الحيوانية مبنى القطاع الزراعي وبحضور كل من: - </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>السيد/ عبد العزيز محمود الزيارة</span>
                            </td>
                            <td>
                                <span>مدير ادارة الثروة الحيوانية</span>
                            </td>
                            <td>
                                <span>رئيس اللجنة</span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <span>السيد/ حمد ساكت الشمري </span>
                            </td>
                            <td>
                                <span>مدير ادارة البحوث الزراعية</span>
                            </td>
                            <td>
                                <span>نائب الرئــيس </span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <span>السيد/ مبارك حمد الهاجري</span>
                            </td>
                            <td>
                                <span>ممثل مربي الثروة الحيوانية</span>
                            </td>
                            <td>
                                <span>عضو اللجنة</span>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <span>السيد/ ناصر حسن النعيمي</span>
                            </td>
                            <td>
                                <span>ممثل مربى الثروة الحيوانية</span>
                            </td>
                            <td>
                                <span>عضو اللجنة</span>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <span>السيد/ محمد حمد النعيمي</span>
                            </td>
                            <td>
                                <span>ممثل الكهرباء والماء</span>
                            </td>
                            <td>
                                <span>عضو اللجنة</span>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <span>السيد/ حمد ابراهيم المرزوقي</span>
                            </td>
                            <td>
                                <span>ممثل وزارة المالية</span>
                            </td>
                            <td>
                                <span>عضو اللجنة</span>
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <span>السيد/ سيف احمد عثمان</span>
                            </td>
                            <td>
                                <span>ممثل ديوان المحاسبة</span>
                            </td>
                            <td>
                                <span>عضو اللجنة</span>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <span style="font-weight: bold; padding-top: 50px;">وبحضور امانة سر اللجنة:-</span>
                            </td>
                        </tr>



                        <tr>
                            <td>
                                <span>السيد / حمد صالح اليزيدي اليافعي</span>
                            </td>
                            <td colspan="2">
                                <span>أمانة سر اللجنة</span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <span>السيد / احمد ابراهيم الحوسني</span>
                            </td>
                            <td colspan="2">
                                <span>أمانة سر اللجنة</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>السيد / علي سعيد اليافعي</span>
                            </td>
                            <td colspan="2">
                                <span>أمانة سر اللجنة</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>السيدة / مها مبارك البوعينين</span>
                            </td>
                            <td colspan="2">
                                <span>أمانة سر اللجنة</span>
                            </td>
                        </tr>


                    </table>
                </div>
            </div>
        </div>
        <div class="centered-div" style="margin-top: 0px;">
            <div class="table-container">
                <div style="text-align: center;">

                    <table style="text-align: right; width: 1000px; font-size: 20px;">
                        <tr>
                            <td colspan="2">
                                <span style="font-size: 20px; font-weight: bold;">توقيع أعضاء اللجنة:</span>
                            </td>
                        </tr>

                        <tr>
                            <td style="border: 1px solid; text-align: center; font-weight: bold; width: 350px;">
                                <span>الاسم</span>
                            </td>

                            <td style="border: 1px solid; text-align: center; font-weight: bold;">
                                <span>الصفة</span>
                            </td>

                            <td style="border: 1px solid; text-align: center; font-weight: bold;">
                                <span>التوقيع</span>
                            </td>
                        </tr>

                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ عبد العزيز محمود الزيارة</span>
                            </td>

                            <td>
                                <span>رئيس اللجنة</span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>

                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ حمد ساكت الشمري </span>
                            </td>

                            <td>
                                <span>نائب الرئــيس </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>

                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ مبارك حمد الهاجري</span>
                            </td>

                            <td>
                                <span>عضو </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ ناصر حسن النعيمي</span>
                            </td>

                            <td>
                                <span>عضو </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ محمد حمد النعيمي</span>
                            </td>

                            <td>
                                <span>عضو </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ حمد ابراهيم المرزوقي</span>
                            </td>

                            <td>
                                <span>عضو </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>السيد/ سيف احمد عثمان</span>
                            </td>

                            <td>
                                <span>عضو </span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>


        <div class="centered-div">
            <div class="table-container">
                <div style="text-align: center;">

                    <table style="text-align: right; width: 1000px; font-size: 20px;">
                        <tr>
                            <td colspan="2">
                                <span style="font-size: 20px; font-weight: bold;">توقيع امانة سر اللجنة:</span>
                            </td>
                        </tr>

                        <tr>
                            <td style="border: 1px solid; text-align: center; font-weight: bold; width: 350px;">
                                <span>الاسم</span>
                            </td>

                            <td style="border: 1px solid; text-align: center; font-weight: bold;">
                                <span>الصفة</span>
                            </td>

                            <td style="border: 1px solid; text-align: center; font-weight: bold;">
                                <span>التوقيع</span>
                            </td>
                        </tr>

                        <tr style="text-align: center;">
                            <td>
                                <span>حمد صالح اليزيدي اليافعي</span>
                            </td>

                            <td>
                                <span>أمانة سر اللجنة</span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>



                        <tr style="text-align: center;">
                            <td>
                                <span>احمد ابراهيم الحوسني</span>
                            </td>

                            <td>
                                <span>أمانة سر اللجنة</span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>علي سعيد اليافعي</span>
                            </td>

                            <td>
                                <span>أمانة سر اللجنة</span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                        <tr style="text-align: center;">
                            <td>
                                <span>مها مبارك البوعينين</span>
                            </td>

                            <td>
                                <span>أمانة سر اللجنة</span>
                            </td>
                            <td style="border: 1px solid"></td>
                        </tr>


                    </table>
                </div>
            </div>
        </div>


        <%--<div class="centered-div" style="margin-top: 220px">
            <div class="table-container">
                <div style="font-weight: bold; font-size: 20px;">
                    كشف المستفيدين من دعم المياه:
                </div>
                <div style="text-align: center;">
                    <asp:Table ID="tbl_requests" runat="server" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" Width="1200px" Font-Bold="true">
                        <asp:TableRow>
                            <asp:TableCell>م</asp:TableCell>
                            <asp:TableCell>رقم الطلب</asp:TableCell>
                            <asp:TableCell>اسم المستفيد</asp:TableCell>
                            <asp:TableCell>الرقم الشخصي</asp:TableCell>
                            <asp:TableCell>اسم البنك</asp:TableCell>
                            <asp:TableCell>رقم الحساب</asp:TableCell>
                            <%-- <asp:TableCell>ابل</asp:TableCell>
                            <asp:TableCell>بقر</asp:TableCell>
                            <asp:TableCell>ماعز</asp:TableCell>
                            <asp:TableCell>ضان</asp:TableCell>
                            <asp:TableCell>خيول</asp:TableCell>
                            <asp:TableCell>غزلان</asp:TableCell>
                            <asp:TableCell>كمية المياه بالجالون</asp:TableCell>
                            <asp:TableCell>المبلغ المستحق</asp:TableCell>

                        </asp:TableRow>
                    </asp:Table>

                </div>
            </div>
        </div>--%>


        <div class="centered-div" style="margin-top: 400px;">
            <div class="table-container">
                <div style="text-align: center;">

                    <table style="text-align: right; width: 1000px; font-size: 20px">
                        <tr>
                            <td>
                                <span style="font-size: 20px; font-weight: bold;">محضر اجتماع رقم(<asp:Label ID="lbl_sn2" runat="server" Text=""></asp:Label>)  :</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txt_notes" ReadOnly="true" runat="server" TextMode="MultiLine" Width="100%" Height="1100px" Font-Size="20px"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
        <div class="centered-div">
            <div class="table-container" style="padding: 0;">
                <div class="rtl-div bold">
                    <table style="text-align: center; font-size: small; width:800px;">
                        <tr>
                            <td>أمين السر
                            </td>
                            <td>الرئيس
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>عبد العزيز محمود الزيارة
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>مدير إدارة الثروة الحيوانية
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
