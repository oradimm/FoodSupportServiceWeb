<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Backend.Master" AutoEventWireup="true" CodeBehind="meetings.aspx.cs" Inherits="WaterSupportService.backend.meetings" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.18.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnAttachBtnClick(meeting_id, ATTACHMENT) {
            document.getElementById('<%= hf_meeting_id_attachment.ClientID %>').value = meeting_id;
            document.getElementById('<%= hf_uploaded_file_id.ClientID %>').value = ATTACHMENT;
            document.getElementById('<%= hf_saved_file_id.ClientID %>').value = ATTACHMENT;
            if (!ATTACHMENT) {
                //btn_attachment_save.SetEnabled(true);
                //btn_upload_file.SetEnabled(true);
            } else {
                //btn_attachment_save.SetEnabled(false);
                //btn_upload_file.SetEnabled(false);
            }

            attachFileModal.Show();
        }
        function OnEditBtnClick(meeting_id, notes, meetingDate) {
            document.getElementById('<%= hf_meeting_id_edit.ClientID %>').value = meeting_id;
            txt_edit_notes.SetText(notes.toString());
            de_edit_meeting_date.SetText(meetingDate);
            updateMeetingModal.Show();
        }
        function ShowNewMeetingModalWindow() {

            newMeetingModal.Show();
        }
        function Success(result) {
            //alert(result);
        }
        function Failure(error) {
            alert(error);
        }
        function ShowFile() {
            window.open('https://dotnet.mme.gov.qa/AttachmentsDownloadManager/GetAttachmentProd?DocID=' + document.getElementById('<%= hf_uploaded_file_id.ClientID %>').value, '_blank').focus();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btShowModal" runat="server" Text="إجتماع جديد" AutoPostBack="False" UseSubmitBehavior="false" Width="200px" Height="35px">
                                <ClientSideEvents Click="function(s, e) { ShowNewMeetingModalWindow(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btn_back" OnClick="btn_back_Click" runat="server" Text="عودة" AutoPostBack="False" UseSubmitBehavior="false" Width="200px" Height="35px">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>


            </div>
        </div>
    </div>
    <div class="centered-div">
        <div class="table-container">
            <div class="rtl-div bold">
                إجتماعات لجنة دعم المياه
            </div>
            <div style="text-align: center;">
                <dx:ASPxGridView ID="ASPxGridView1" EnableCallBacks="true" runat="server" Theme="Default" Width="100%" KeyFieldName="ID" AutoGenerateColumns="False" RightToLeft="True" OnDataBinding="ASPxGridView1_DataBinding">
                    <SettingsBehavior AllowFocusedRow="false" AllowSelectByRowClick="false" ProcessSelectionChangedOnServer="true" />
                    <Settings ShowFilterRow="True" ShowFilterBar="Visible" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" />

                    <Columns>
                        <dx:GridViewDataColumn Caption="تسلسل" FieldName="SN"></dx:GridViewDataColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الاجتماع" FieldName="MEETING_DATE">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلبات من" FieldName="REPORT_DATE_FROM">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="تاريخ الطلبات الى" FieldName="REPORT_DATE_TO">
                            <PropertiesDateEdit DisplayFormatString="dd-MM-yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataMemoColumn Caption="الاجندة" FieldName="NOTES">
                            <PropertiesMemoEdit>
                            </PropertiesMemoEdit>
                        </dx:GridViewDataMemoColumn>
                        <dx:GridViewDataTextColumn Caption="عمليات" ShowInCustomizationForm="True">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btn_upload_attachment" Text="المرفقات" AutoPostBack="false" OnInit="btn_upload_attachment_Init">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btn_edit" Text="تعديل" AutoPostBack="false" OnInit="btn_edit_Init">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>

                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="270px" Caption="تقارير">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btn_meeting_report" Text="محضر اللجنة" OnClick="btn_meeting_report_Click" >
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btn_committee_report_anix" Text="كشف اللجنة" OnClick="btn_committee_report_anix_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btn_finance_report" Text="كشف المالية" OnClick="btn_finance_report_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>

                    </Columns>
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
    <dx:ASPxPopupControl ID="newMeetingModal" runat="server" Width="500" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="newMeetingModal"
        HeaderText="اجتماع جديد" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true" RightToLeft="True" OnLoad="pcLogin_Load">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                                <Items>
                                    <dx:LayoutItem Caption="التسلسل">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txt_sn" runat="server" Width="100%">
                                                                <ValidationSettings>
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txt_sn_y" runat="server" Width="100%" Enabled="false">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="تاريخ الاجتماع">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxDateEdit ID="de_meeting_date" runat="server" ShowShadow="false" Width="100%"></dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem Caption="تاريخ الطلبات من">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxDateEdit ID="de_report_date_from" runat="server" ShowShadow="false" Width="100%"></dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="تاريخ الطلبات الى">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxDateEdit ID="de_report_date_to" runat="server" ShowShadow="false" Width="100%"></dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="الاجندة">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxMemo ID="txt_notes" runat="server" Height="100px" Width="100%" RightToLeft="True"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btOK" runat="server" Text="حفظ" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px" OnClick="btOK_Click">
                                                    <ClientSideEvents Click="function(s, e) { if(ASPxClientEdit.ValidateGroup('entryGroup')) newMeetingModal.Hide(); }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btCancel" runat="server" Text="الغاء" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) { newMeetingModal.Hide(); }" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="updateMeetingModal" runat="server" Width="500" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="updateMeetingModal"
        HeaderText="تعديل اجتماع" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true" RightToLeft="True" OnLoad="pcLogin_Load">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" Width="100%" Height="100%">
                                <Items>

                                    <dx:LayoutItem Caption="تاريخ الاجتماع">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxDateEdit ID="de_edit_meeting_date" ClientInstanceName="de_edit_meeting_date" runat="server" ShowShadow="false" Width="100%"></dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="الاجندة">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <asp:HiddenField ID="hf_meeting_id_edit" runat="server" />
                                                <dx:ASPxMemo ID="txt_edit_notes" ClientInstanceName="txt_edit_notes" runat="server" Height="100px" Width="100%" RightToLeft="True"></dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btn_edit_save" runat="server" Text="حفظ" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px" OnClick="btn_edit_save_Click">
                                                    <ClientSideEvents Click="function(s, e) { if(ASPxClientEdit.ValidateGroup('entryGroup')) updateMeetingModal.Hide(); }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btn_edit_cancel" runat="server" Text="الغاء" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                    <ClientSideEvents Click="function(s, e) { updateMeetingModal.Hide(); }" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="attachFileModal" ShowPageScrollbarWhenModal="true" runat="server" Width="500" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  ClientInstanceName="attachFileModal"
        HeaderText="ارفاق محضر الأجتماع الموقع" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true" RightToLeft="True" OnLoad="pcLogin_Load">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" >
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout3" Width="100%" Height="100%">
                                <Items>

                                    <dx:LayoutItem Caption="المرفقات">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <asp:HiddenField ID="hf_meeting_id_attachment" runat="server" />
                                                <asp:HiddenField ID="hf_uploaded_file_id" runat="server" />
                                                <asp:HiddenField ID="hf_saved_file_id" runat="server" />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:FileUpload ID="fu_attachment" runat="server" /></td>
                                                        <td>
                                                            <%--<dx:ASPxButton  ID="btn_upload_file" ClientInstanceName="btn_upload_file"  runat="server" Text="تحميل" OnClick="btn_upload_file_Click"></dx:ASPxButton>--%>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btn_show_file" OnInit="btn_show_file_Init" AutoPostBack="false" runat="server" Text="عرض"></dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxButton ID="btn_attachment_save"  ClientInstanceName="btn_attachment_save" runat="server" Text="حفظ" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px" OnClick="btn_attachment_save_Click">
                                                    <ClientSideEvents Click="function(s, e) { if(ASPxClientEdit.ValidateGroup('entryGroup')) attachFileModal.Hide(); }" />
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btn_attachment_cancel" runat="server" Text="الغاء" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px" OnClick="btn_attachment_cancel_Click">
                                                    <ClientSideEvents Click="function(s, e) { attachFileModal.Hide(); }" />
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
