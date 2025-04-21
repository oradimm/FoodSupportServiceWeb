<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="WaterSupportService.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تسجيل الدخول</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="Assets/css/StyleSheetReferance.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="box">
                <div style="display: flex;">
                </div>

            </div>
            <div class="box">
                <span style="font-weight: bold;">خدمة دعم المياه لمربين الثروة الحيوانية</span>
                <br />
                <span>ادارة الثروة الحيوانية</span>
                <br />
                <span>وزارة البلديـــة</span>
            </div>
            <div class="box">
                <div style="text-align: right;">
                    <img src="Assets/images/MME-logo.png" />
                </div>
            </div>
        </div>

        <div class="centered-div">
            <div class="table-container" style="border: 1px solid;">
                <div class="rtl-div" style="text-align: center;">
                    <span style="font-weight: bold">تسجيل الدخول</span>
                </div>
                <div style="text-align: center;">
                    <table>
                        <tr>

                            <td>الرقم الشخصي</td>
                            <td>
                                <asp:TextBox ID="txt_qid" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="txt_qid_TextChanged"> </asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>رقم الجوال</td>
                            <td>
                                <asp:TextBox ID="txt_mobile" runat="server" Enabled="true"> </asp:TextBox></td>

                        </tr>
                        <tr>
                            <td>رمز التحقق</td>
                            <td>
                                <asp:TextBox ID="txt_otp" runat="server" Enabled="false"> </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btn_send_otp" CssClass="red-button" runat="server" Text="ارسال رمز التحقق" Visible="true" OnClick="btn_send_otp_Click" />
                                <asp:Button ID="btn_sign_in" CssClass="red-button" runat="server" Text="دخول" Visible="false" OnClick="btn_sign_in_Click" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_error_msg" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="centered-div">
            <div class="table-container">
                <div class="rtl-div" style="text-align: center;">
                    <span>ملكية فكرية 2023</span>
                    <br />
                    <span>جميع الحقوق محفوظة لوزارة البلدية - دولة قطر</span>
                    <br />
                    <span>للمساعدة يرجى الاتصال على 184</span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
