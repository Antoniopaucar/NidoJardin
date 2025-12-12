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

        <table id="tablaProfesor" class="tablaForm">
            <tr class="titulo">
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Text="Profesor"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_IdProfesor" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Usuario:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Usuario" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Nombres:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_Nombres" runat="server" onkeypress="return SoloTexto(event);"
                        CssClass="full-width-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Apellido Paterno:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_ApellidoPaterno" runat="server" onkeypress="return SoloTexto(event);"
                        CssClass="full-width-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Apellido Materno:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_ApellidoMaterno" runat="server" onkeypress="return SoloTexto(event);"
                        CssClass="full-width-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Tipo Documento:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Ddl_Tipo_Documento" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="Ddl_Tipo_Documento_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Documento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Documento" runat="server" onkeypress="return SinEspacios(event);">
                    </asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Fecha Nacimiento:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Fecha_Nacimiento" runat="server" CssClass="full-width-textbox" TextMode="Date">
                    </asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Sexo:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Ddl_Sexo" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Distrito:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Ddl_Distrito" runat="server"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Fecha Ingreso:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Fecha_Ingreso" runat="server" CssClass="full-width-textbox" TextMode="Date" Enabled="false">
                    </asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Direccion:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_Direccion" runat="server" CssClass="full-width-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Telefono:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Telefono" runat="server" onkeypress="return SoloNumeros(event);"
                        CssClass="full-width-textbox" MaxLength="9"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Email:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_Email" runat="server" onkeypress="return SinEspacios(event);"
                        CssClass="full-width-textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Titulo Profesional:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fup_Titulo_Profesional" runat="server" CssClass="file-upload-custom" />
                    <asp:LinkButton ID="lnk_Titulo_Profesional" runat="server" Text="Descargar"
                        OnClick="DescargarArchivo_Click" CommandArgument="TituloProfesional" Visible="false"
                        CssClass="btn btn-link" ForeColor="Green" Font-Bold="true" />
                    <asp:Label ID="lbl_Titulo_Profesional_Msg" runat="server" Text="Cargar documento" ForeColor="Red"
                        Visible="false" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" ForeColor="Black" Font-Size="12px"
                        Text="El archivo no debe superar los 5 MB." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="Curriculum Vitae:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fup_Cv" runat="server" CssClass="file-upload-custom" />
                    <asp:LinkButton ID="lnk_Cv" runat="server" Text="Descargar"
                        OnClick="DescargarArchivo_Click" CommandArgument="Cv" Visible="false" CssClass="btn btn-link"
                        ForeColor="Green" Font-Bold="true" />
                    <asp:Label ID="lbl_Cv_Msg" runat="server" Text="Cargar documento" ForeColor="Red" Visible="false"
                        Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="3">
                    <asp:Label ID="Label19" runat="server" ForeColor="Black" Font-Size="12px"
                        Text="El archivo no debe superar los 5 MB." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Evaluacion Psicologica:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fup_Evaluacion_Psicologica" runat="server" CssClass="file-upload-custom" />
                    <asp:LinkButton ID="lnk_Evaluacion_Psicologica" runat="server" Text="Descargar"
                        OnClick="DescargarArchivo_Click" CommandArgument="EvaluacionPsicologica" Visible="false"
                        CssClass="btn btn-link" ForeColor="Green" Font-Bold="true" />
                    <asp:Label ID="lbl_Evaluacion_Psicologica_Msg" runat="server" Text="Cargar documento"
                        ForeColor="Red" Visible="false" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="3">
                    <asp:Label ID="Label21" runat="server" ForeColor="Black" Font-Size="12px"
                        Text="El archivo no debe superar los 5 MB." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label22" runat="server" Text="Fotos:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fup_Fotos" runat="server" CssClass="file-upload-custom" />
                    <asp:LinkButton ID="lnk_Fotos" runat="server" Text="Descargar"
                        OnClick="DescargarArchivo_Click" CommandArgument="Fotos" Visible="false" CssClass="btn btn-link"
                        ForeColor="Green" Font-Bold="true" />
                    <asp:Label ID="lbl_Fotos_Msg" runat="server" Text="Cargar documento" ForeColor="Red" Visible="false"
                        Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="3">
                    <asp:Label ID="Label23" runat="server" ForeColor="Black" Font-Size="12px"
                        Text="El archivo no debe superar los 5 MB." />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Text="Verificacion Domiciliaria:"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:FileUpload ID="fup_Verificacion_Domiciliaria" runat="server" CssClass="file-upload-custom" />
                    <asp:LinkButton ID="lnk_Verificacion_Domiciliaria" runat="server" Text="Descargar"
                        OnClick="DescargarArchivo_Click" CommandArgument="VerificacionDomiciliaria" Visible="false"
                        CssClass="btn btn-link" ForeColor="Green" Font-Bold="true" />
                    <asp:Label ID="lbl_Verificacion_Domiciliaria_Msg" runat="server" Text="Cargar documento"
                        ForeColor="Red" Visible="false" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="3">
                    <asp:Label ID="Label25" runat="server" ForeColor="Black" Font-Size="12px"
                        Text="El archivo no debe superar los 5 MB." />
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="2">
                    <asp:Button ID="btn_Modificar" runat="server" Text="Guardar"
                        OnClientClick="return confirm('¿Deseas guardar los cambios?') && validarCamposTabla('tablaProfesor','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,Ddl_Distrito,txt_Direccion,txt_Telefono,txt_Email,fup_Titulo_Profesional,fup_Cv,fup_Evaluacion_Psicologica,fup_Fotos,fup_Verificacion_Domiciliaria') ;"
                        OnClick="btn_Modificar_Click" class="btn btn-primario" />
                </td>
                <td>

                </td>
            </tr>
        </table>
    </asp:Content>