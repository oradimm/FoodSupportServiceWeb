<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="FooderSupportService.backend.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>دخول الموظفين</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link href="../Assets/css/StyleSheetReferance.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="box">
                <div style="display: flex;">
                </div>

            </div>
            <div class="box">
                <span style="font-weight: bold;">خدمة دعم الاعلاف لمربين الثروة الحيوانية</span>
                <br />
                <span>ادارة الثروة الحيوانية</span>
                <br />
                <span>وزارة البلديـــة</span>
            </div>
            <div class="box">
                <div style="text-align: right;">
                    <img src="../Assets/images/MME-logo.png" />
                </div>
            </div>
        </div>

        <div class="centered-div">
            <div class="table-container" style="border: 1px solid;">
                <div class="rtl-div" style="text-align: center;">
                    <span style="font-weight: bold">دخول الموظفين</span>
                </div>
                <div style="text-align: center;">
                    <table>
                        <tr>

                            <td>اسم المستخدم</td>
                            <td>
                                <asp:TextBox ID="txt_user_name" runat="server" Enabled="true" AutoPostBack="true" > </asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>كلمة المرور</td>
                            <td>
                                <asp:TextBox ID="txt_password" runat="server" TextMode="Password" Enabled="true"> </asp:TextBox></td>

                        </tr>
                    
                        <tr>
                            <td colspan="2">
                                
                                <asp:Button ID="btn_sign_in" CssClass="red-button" runat="server" Text="تسجيل دخول" Visible="true" OnClick="btn_sign_in_Click" />
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

