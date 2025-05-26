<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="FooderSupportService.Apply" %>

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
            <div style="text-align: right; font-size: x-large;">
                <table>
                    <tr>
                        <td style="font-weight: bold; color: orangered;">شروط و أحكام التقديم على الخدمة
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ol>
                                <li>ان تكون رخصة العزبة سارية الصلاحية.</li>
                                <li>الا يتعدى ترقيم الثروة الحيوانية عن سنة من تاريخ اخر حصر.</li>
                                <li>توزيع الاعلاف المجانية مخصصة فقط للأغنام.</li>
                                <li>الا يقل عدد الأغنام (ضان) بالحيازة عن 50 راس كحد أدنى.</li>
                                <li>يحق للمربي الحصول على الاعلاف المجانية مرة واحدة فقط بالسنة، و يستثنى من ذلك المربين المشاركين في المبادرة الوطنية لتشجيع الإنتاج المحلي من الأغنام خلال فترتي رمضان و عيد الأضحى.</li>
                                <li>ان يستوفى المربي جميع التحصينات السيادية المطلوبة.</li>
                                <li>يتم صرف الاعلاف المجانية لأصحاب الحظائر المؤقتة بمنطقة النخش وغيرها فقط للمشاركين في المبادرة الوطنية لتشجيع الإنتاج المحلي من الأغنام.</li>
                                <li>يتم توزيع الاعلاف المجانية بناء على مخرجات الإنتاج من خلال التنسيق مع أسواق شركة حصاد الغذائية او ساحات بيع المنتج المحلي بمجمعات العزب.</li>
                            </ol>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
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
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الأغنام (االضأن)</asp:TableCell>
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
                        <td>رقم الجوال</td>
                        <td>
                            <asp:TextBox ID="txt_mobile" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                        <td>تاريخ الطلب</td>
                        <td>
                            <asp:TextBox ID="txt_applied_Date" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                    </tr>

                     <tr>
                        <td>إجمالي عدد الأغنام (االضأن)</td>
                        <td>
                            <asp:TextBox ID="txt_total_sheeps" runat="server" ReadOnly="true" Width="250px"> </asp:TextBox></td>
                       
                    </tr>
                    
                </table>
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
                        <asp:TableCell BorderStyle="Solid" BorderWidth="1px">عدد الأغنام (االضأن)</asp:TableCell>
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
                                <li>يحق للمربي التقديم على خدمة دعم الاعلاف مرة واحدة فقط بالسنة</li>

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
