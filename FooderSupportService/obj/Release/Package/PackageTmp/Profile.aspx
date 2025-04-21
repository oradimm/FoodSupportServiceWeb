<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WaterSupportService.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
        .ErrorControl
        {
            background-color: #FBE3E4;
            border: solid 1px Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div" style="font-weight: bold;">
                تحديث البيانات البنك و شهادة IBAN
            </div>
            <div style="text-align: center;">
                <table>
                    <tr>
                        <td>اسم المستفيد</td>
                        <td>
                            <asp:TextBox ID="txt_name" runat="server" Enabled="false" Width="460px"> </asp:TextBox></td>
                        <td>الرقم الشخصي</td>
                        <td>
                            <asp:TextBox ID="txt_qid" runat="server" Enabled="false" Width="460px"> </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>اسم البنك</td>
                        <td>
                            <asp:DropDownList ID="ddl_bank" runat="server" Width="460px" AutoPostBack="true" OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged">
                                <asp:ListItem Text="يرجى الاختيار" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="بنك قطر الوطني" Value="1" BankCode="QNBA"></asp:ListItem>
                                <asp:ListItem Text="مصرف قطر الإسلامي" Value="2" BankCode="QISB"></asp:ListItem>
                                <asp:ListItem Text="بنك قطر الدولي الإسلامي" Value="3" BankCode="QIIB"></asp:ListItem>
                                <asp:ListItem Text="مصرف الريان" Value="4" BankCode="MAFR"></asp:ListItem>
                                <asp:ListItem Text="البنك التجاري القطري" Value="5" BankCode="CBQA"></asp:ListItem>
                                <asp:ListItem Text="بنك الدوحه" Value="6" BankCode="DOHB"></asp:ListItem>
                                <asp:ListItem Text="بنك دخان" Value="7" BankCode="BRWA"></asp:ListItem>
                                <asp:ListItem Text="البنك الأهلي" Value="8" BankCode="ABQQ"></asp:ListItem>
                                <asp:ListItem Text="البنك البريطاني" Value="9" BankCode="BBME"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td>رقم الايبان</td>
                        <td>
                            <asp:TextBox ID="txt_iban8" runat="server" Width="50px" MaxLength="5" ValidationGroup="iban"></asp:TextBox>
                            <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban8" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{5,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            <script type="text/javascript">
                                function Validate(sender, args) {
                                    if (document.getElementById(sender.controltovalidate).value != "") {
                                        args.IsValid = true;
                                    } else {
                                        args.IsValid = false;
                                    }
                                }
                            </script>
                            -
                            <asp:TextBox ID="txt_iban7" runat="server" Width="40px" MaxLength="4" ValidationGroup="iban"></asp:TextBox>
                            <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban7" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            -
                            <asp:TextBox ID="txt_iban6" runat="server" Width="40px" MaxLength="4" ValidationGroup="iban"></asp:TextBox>
                            <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban6" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            -
                            <asp:TextBox ID="txt_iban5" runat="server" Width="40px" MaxLength="4" ValidationGroup="iban"></asp:TextBox>
                             <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban5" ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            -
                            <asp:TextBox ID="txt_iban4" runat="server" Width="40px" MaxLength="4" ValidationGroup="iban"></asp:TextBox>
                            <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban4" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            -
                            <asp:TextBox ID="txt_iban3" runat="server" Width="40px" MaxLength="4" Enabled="false" ValidationGroup="iban"></asp:TextBox>
                            -
                            <asp:TextBox ID="txt_iban2" runat="server" Width="25px" MaxLength="2" ValidationGroup="iban"></asp:TextBox>
                            <asp:RegularExpressionValidator ClientValidationFunction="Validate" ValidationGroup="iban" Display="Dynamic" ControlToValidate="txt_iban2" ID="RegularExpressionValidator6" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="">
                            </asp:RegularExpressionValidator>
                            -
                            <asp:TextBox ID="txt_iban1" runat="server" Width="25px" MaxLength="2" Enabled="false" ValidationGroup="iban"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>رقم الجوال</td>
                        <td>
                            <asp:TextBox ID="txt_mobile" runat="server" Enabled="false" Width="460px"> </asp:TextBox></td>
                        <td>شهادة الايبان</td>
                        <td>
                            <asp:FileUpload ID="fu_iban_cert" runat="server" Width="320px" /></td>
                        <td>
                            <asp:LinkButton ID="lb_view_iban_cert" runat="server" OnClick="lb_view_iban_cert_Click">عرض</asp:LinkButton></td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:Button ValidationGroup="iban" ID="btn_save" CssClass="red-button" runat="server" Text="حفظ بيانات البنك" Visible="true" OnClick="btn_save_Click" />
                        </td>

                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div" style="font-weight: bold;">
                تحديث شهادة تحلية مياه الابار الخاصة <span style="color:orangered;">بالمزارع فقط</span>        
            </div>
            <div style="text-align: center;">
                <table>
                    <tr>

                        <td>شهادة تحلية المياه <span style="color:orangered;">للمزارع فقط</span> </td>
                        <td>
                            <asp:FileUpload ID="fu_water_cert" runat="server" Width="320px" /></td>
                        <td>
                            <asp:LinkButton ID="lb_view_water_cert" runat="server" OnClick="lb_view_water_cert_Click">عرض</asp:LinkButton></td>
                        <td style="width: 700px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btn_save_water_cert" OnClick="btn_save_water_cert_Click" CssClass="red-button" runat="server" Text="حفظ شهادة تحلية المياه للمزارع فقط" Visible="true" />
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
                            <asp:Button ID="btn_back" CssClass="red-button" runat="server" Text="عودة" Visible="true" OnClick="btn_back_Click" />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lbl_message" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function WebForm_OnSubmit() {
            var errorControls = new Array();
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) {
                for (var i in Page_Validators) {
                    try {
                        var control = document.getElementById(Page_Validators[i].controltovalidate);
                        if (!Page_Validators[i].isvalid) {
                            errorControls[errorControls.length] = control;
                        }
                        control.className = "";
                    } catch (e) { }
                }
                for (var i in errorControls) {
                    errorControls[i].className = "ErrorControl";
                }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
