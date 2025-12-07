<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true"
    CodeBehind="frm_Docente_Datos.aspx.cs" Inherits="ProyectoNido.frm_Docente_Datos" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="Js/docente_datos.js" type="text/javascript"></script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="docente-left">
            <div class="docente-avatar"></div>
            <div class="docente-nombre">
                <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
            </div>
            <div class="docente-menu">
                <a class="activo" href="frm_Docente_Datos.aspx">Datos</a>
                <a href="frm_Docente_Comunicado.aspx">Ver Comunicado</a>
                <a href="frm_Docente_GrupoAnual.aspx">Grupo Anual</a>
                <a href="frm_Docente_GrupoServicio.aspx">Grupo Servicio</a>
            </div>
        </div>

    </asp:Content>




    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
        <div class="docente-right">
            <table class="docente-form-table">
                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelNombres" runat="server" Text="NOMBRES"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="docente-input"></asp:TextBox>
                    </td>

                    <td class="docente-label">
                        <asp:Label ID="LabelTitulo" runat="server" Text="Título Profesional"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuTituloProfesional" runat="server" CssClass="file-upload-custom" />
                        <div class="archivo-info">
                            <asp:HyperLink ID="lnkTituloProfesional" runat="server" Target="_blank" Visible="false"
                                CssClass="archivo-existente"></asp:HyperLink>
                            <asp:Label ID="lblTituloProfesional" runat="server" Text="Sin archivo guardado"
                                CssClass="sin-archivo"></asp:Label>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelApePat" runat="server" Text="APELLIDO PATERNO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="docente-input"></asp:TextBox>
                    </td>

                    <td class="docente-label">
                        <asp:Label ID="LabelCV" runat="server" Text="CV"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuCV" runat="server" CssClass="file-upload-custom" />
                        <div class="archivo-info">
                            <asp:HyperLink ID="lnkCV" runat="server" Target="_blank" Visible="false"
                                CssClass="archivo-existente"></asp:HyperLink>
                            <asp:Label ID="lblCV" runat="server" Text="Sin archivo guardado" CssClass="sin-archivo">
                            </asp:Label>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelApeMat" runat="server" Text="APELLIDO MATERNO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="docente-input"></asp:TextBox>
                    </td>

                    <td class="docente-label">
                        <asp:Label ID="LabelEvalPsi" runat="server" Text="Evaluación Psicológica"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuEvaluacionPsicologica" runat="server" CssClass="file-upload-custom" />
                        <div class="archivo-info">
                            <asp:HyperLink ID="lnkEvaluacionPsicologica" runat="server" Target="_blank" Visible="false"
                                CssClass="archivo-existente"></asp:HyperLink>
                            <asp:Label ID="lblEvaluacionPsicologica" runat="server" Text="Sin archivo guardado"
                                CssClass="sin-archivo" Height="16px"></asp:Label>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelDNI" runat="server" Text="DNI"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDNI" runat="server" CssClass="docente-input docente-input-readonly"
                            ReadOnly="true"></asp:TextBox>
                    </td>

                    <td class="docente-label">
                        <asp:Label ID="LabelFoto" runat="server" Text="Foto"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuFoto" runat="server" CssClass="file-upload-custom" />
                        <div class="archivo-info">
                            <asp:HyperLink ID="lnkFoto" runat="server" Target="_blank" Visible="false"
                                CssClass="archivo-existente"></asp:HyperLink>
                            <asp:Label ID="lblFoto" runat="server" Text="Sin archivo guardado" CssClass="sin-archivo">
                            </asp:Label>
                        </div>
                    </td>

                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelFechaNac" runat="server" Text="FECHA DE NACIMIENTO"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="docente-input" TextMode="Date">
                        </asp:TextBox>
                    </td>
                    <td class="docente-label">
                        <asp:Label ID="LabelVerifDom" runat="server" Text="Verificación Domiciliaria"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuVerificacionDomiciliaria" runat="server" CssClass="file-upload-custom" />
                        <div class="archivo-info">
                            <asp:HyperLink ID="lnkVerificacionDomiciliaria" runat="server" Target="_blank"
                                Visible="false" CssClass="archivo-existente"></asp:HyperLink>
                            <asp:Label ID="lblVerificacionDomiciliaria" runat="server" Text="Sin archivo guardado"
                                CssClass="sin-archivo"></asp:Label>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelSexo" runat="server" Text="Sexo"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="docente-input">
                            <asp:ListItem Text="Seleccionar" Value=""></asp:ListItem>
                            <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelDireccion" runat="server" Text="Dirección"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="docente-input"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelEmail" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="docente-input"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="docente-label">
                        <asp:Label ID="LabelFechaIngreso" runat="server" Text="Fecha de ingreso"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_FechaIngreso" runat="server"
                            CssClass="docente-input docente-input-readonly" TextMode="Date" ReadOnly="true">
                        </asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <asp:Button ID="btnGuardarDocente" runat="server" Text="GUARDAR"
                CssClass="btn btn-exito btn-guardar-docente" OnClick="btnGuardarDocente_Click" />
        </div>

        </div>
    </asp:Content>