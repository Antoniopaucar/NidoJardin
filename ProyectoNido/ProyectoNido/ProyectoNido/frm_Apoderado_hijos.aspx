<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado_hijos.aspx.cs" Inherits="ProyectoNido.frm_Apoderado_hijos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
     <link href="CSS/apoderado_hijos_expandible.css" rel="stylesheet" />
 <script src="Js/docente_datos.js" type="text/javascript"></script>
 <script src="Js/apoderado_hijos_expandible.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
            <div class="docente-left">
    <div class="docente-avatar"></div>
    <div class="docente-nombre">
        <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
    </div>
    <div class="docente-menu">
        <a href="frm_Apoderado_Datos.aspx">Datos</a>
        <a href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
        <a href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
    </div>

    <!-- Sección de Hijos Expandibles -->
    <div class="hijos-seccion">
        <div class="hijos-titulo">Mis Hijos</div>
        <div class="hijos-container">
            <asp:Repeater ID="rptHijos" runat="server">
                <ItemTemplate>
                    <div class="hijo-item" data-id-hijo='<%# Eval("Id") %>'>
                        <asp:Button ID="btnHijo" runat="server" 
                            Text='<%# Eval("Nombres") + " " + Eval("ApellidoPaterno") + " " + Eval("ApellidoMaterno") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnClick="btnHijo_Click"
                            CssClass='<%# Convert.ToInt32(Eval("Id")) == (Session["IdAlumnoSeleccionado"] != null ? Convert.ToInt32(Session["IdAlumnoSeleccionado"]) : 0) ? "btn-hijo activo" : "btn-hijo" %>' />
                        <div class="hijo-opciones" style="display:none;">
                            <asp:LinkButton ID="lnkCronograma" runat="server" 
                                CssClass="hijo-opcion"
                                OnClick="lnkCronograma_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                <i class="fas fa-calendar-alt"></i> Cronograma de Pagos
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkHistorial" runat="server" 
                                CssClass="hijo-opcion"
                                OnClick="lnkHistorial_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                <i class="fas fa-history"></i> Ver Historial de Servicio
                            </asp:LinkButton>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:Panel ID="pnlSinHijoSeleccionado" runat="server" CssClass="sin-hijo-seleccionado">
        <div class="mensaje-sin-seleccion">
            <i class="fas fa-child" style="font-size: 48px; margin-bottom: 15px; color: #6c757d;"></i>
            <h3>Selecciona un hijo para ver y editar sus datos</h3>
            <p>Haz clic en el nombre de uno de tus hijos en el panel lateral para comenzar.</p>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlFormularioHijo" runat="server" Visible="false">
        <table id="tablaAlumnos" class="tablaForm" >
            <tr class="titulo">
                <td colspan="4">
                    <asp:Label ID="Label2" runat="server" Text="Datos del Estudiante"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_IdAlumno" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Nombres:"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txt_Nombres" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Apellido Paterno:"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txt_ApellidoPaterno" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Apellido Materno:"></asp:Label>
        </td>
        <td colspan="2">
            <asp:TextBox ID="txt_ApellidoMaterno" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label16" runat="server" Text="Tipo Documento:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Ddl_Tipo_Documento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Tipo_Documento_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Documento:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_Documento" runat="server" onkeypress="return SinEspacios(event);"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label15" runat="server" Text="Fecha Nacimiento:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_Fecha_Nacimiento" runat="server" CssClass="full-width-textbox" TextMode="Date"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Sexo:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Ddl_Sexo" runat="server"></asp:DropDownList>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chb_Activo" runat="server" Text="Activo"></asp:CheckBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Fotos:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:FileUpload ID="fup_Fotos" runat="server" CssClass="file-upload-custom" />
        </td>
    </tr>
    <tr>
        <td>

        </td>
        <td colspan="3">
            <asp:Label ID="Label10" runat="server" ForeColor="Black" Font-Size="12px"
                    Text="El archivo no debe superar los 5 MB." />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Copia Dni:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:FileUpload ID="fup_Copia_Dni" runat="server" CssClass="file-upload-custom" />
        </td>
    </tr>
    <tr>
        <td>

        </td>
        <td colspan="3">
            <asp:Label ID="Label12" runat="server" ForeColor="Black" Font-Size="12px"
                    Text="El archivo no debe superar los 5 MB." />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label13" runat="server" Text="Permiso Publicidad:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:FileUpload ID="fup_Permiso_Publicidad" runat="server" CssClass="file-upload-custom" />
        </td>
    </tr>
    <tr>
        <td>

        </td>
        <td colspan="3">
            <asp:Label ID="Label14" runat="server" ForeColor="Black" Font-Size="12px"
                    Text="El archivo no debe superar los 5 MB." />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label17" runat="server" Text="Carnet Seguro:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:FileUpload ID="fup_Carnet_Seguro" runat="server" CssClass="file-upload-custom" />
        </td>
    </tr>
    <tr>
        <td>

        </td>
        <td colspan="3">
            <asp:Label ID="Label18" runat="server" ForeColor="Black" Font-Size="12px"
                    Text="El archivo no debe superar los 5 MB." />
        </td>
    </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btn_Modificar" runat="server" Text="GUARDAR" 
                        OnClientClick="return confirm('¿Deseas guardar los cambios de este estudiante?') && validarCamposTabla('tablaAlumnos','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,fup_Fotos,fup_Copia_Dni,fup_Permiso_Publicidad,fup_Carnet_Seguro') ;" 
                        OnClick="btn_Modificar_Click" class="btn btn-primario" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
