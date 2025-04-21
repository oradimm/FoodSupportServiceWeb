<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinanceReport.aspx.cs" Inherits="WaterSupportService.backend.reports.FinanceReport" %>

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
               <table style="text-align:center; width:100%">
                   <tr>
                       <td>
                           <img src="../../Assets/images/MME-logo.png" />
                       </td>
                       <td>
                           لجنة تقدير احتياجات أصحاب مزارع الثروة الحيوانية من المياه
                           <br />
                           كشف تحويل كمية جالونات المياه إلى مبالغ مستحقة للمستفيدين في حسابهم
                       </td>
                       <td>
                           التاريخ: <asp:Label ID="lbl_meeting_date" runat="server" Text="Label"></asp:Label>
                           <br />
                           اجتماع رقم (<asp:Label ID="lbl_sn" runat="server" Text="Label"></asp:Label>)
                       </td>
                   </tr>
               </table>
            </div>
            <div style="text-align: center;">
                <asp:Table ID="tbl_myRequests" runat="server" BorderStyle="Solid" GridLines="Both" BorderWidth="1px" Width="1400px" Font-Bold="true">
                    <asp:TableRow>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">م</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">رقم الطلب</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">اسم المستفيد</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">الرقم الشخصي</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">اسم البنك</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">رقم الحساب</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">كمية المياه بالجالون</asp:TableCell>
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">المبلغ المستحق</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <div class="table-container">
            <div class="rtl-div bold">
               <table style="text-align:left; width:100%">
                   <tr>
                       <td>
                           توقيع رئيس اللجنة / عبد العزيز الزيارة
                       </td>
                   </tr>
                     <tr>
                       <td style="padding-left:40px">
                           مدير إدارة الثروة الحيوانية
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
