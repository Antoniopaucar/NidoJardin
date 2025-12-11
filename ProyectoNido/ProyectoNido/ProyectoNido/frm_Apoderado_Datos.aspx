<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado_Datos.aspx.cs" Inherits="ProyectoNido.frm_Apoderado_Datos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
 <script src="Js/docente_datos.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="docente-left">
    <div class="docente-avatar"></div>
    <div class="docente-nombre">
        <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
    </div>
    <div class="docente-menu">
        <a class="activo" href="frm_Apoderado_Datos.aspx">Datos</a>
        <a href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
        <a href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
    </div>

    <!-- Sección de Hijos -->
    <div class="hijos-seccion">
        <div class="hijos-titulo">Mis Hijos</div>
        <div class="hijos-container">
            <asp:Repeater ID="rptHijos" runat="server">
                <ItemTemplate>
                    <asp:Button ID="btnHijo" runat="server" 
                        Text='<%# Eval("Nombres") + " " + Eval("ApellidoPaterno") + " " + Eval("ApellidoMaterno") %>'
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnHijo_Click"
                        CssClass='<%# Convert.ToInt32(Eval("Id")) == (Session["IdAlumnoSeleccionado"] != null ? Convert.ToInt32(Session["IdAlumnoSeleccionado"]) : 0) ? "btn-hijo activo" : "btn-hijo" %>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table id="tablaApoderado" class="tablaForm">
    <tr class="titulo">
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Apoderado"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_IdApoderado" runat="server" Enabled="false"></asp:TextBox>
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
            <asp:TextBox ID="txt_Nombres" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Apellido Paterno:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_ApellidoPaterno" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Apellido Materno:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_ApellidoMaterno" runat="server" onkeypress="return SoloTexto(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
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
            <asp:Label ID="Label8" runat="server" Text="Documento:"></asp:Label>
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
            <asp:TextBox ID="txt_Telefono" runat="server" onkeypress="return SoloNumeros(event);" CssClass="full-width-textbox" MaxLength="9"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label12" runat="server" Text="Email:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_Email" runat="server" onkeypress="return SinEspacios(event);" CssClass="full-width-textbox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Copia Dni:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:FileUpload ID="fup_Copia_Dni" runat="server" CssClass="file-upload-custom" />
            <asp:LinkButton ID="lnk_Copia_Dni" runat="server" Text="Descargar"
                OnClick="DescargarArchivo_Click" CommandArgument="CopiaDni" Visible="false"
                CssClass="btn btn-link" ForeColor="Green" Font-Bold="true" />
            <asp:Label ID="lbl_Copia_Dni_Msg" runat="server" Text="Cargar documento" ForeColor="Red"
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

        </td>
        <td>
            <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                OnClientClick="return confirm('¿Deseas modificar este Apoderado?') && validarCamposTabla('tablaApoderado','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,Ddl_Distrito,txt_Direccion,txt_Telefono,txt_Email,fup_copia_dni') ;" 
                OnClick="btn_Modificar_Click" class="btn btn-primario" />
        </td>
       
        <td>

        </td>
    </tr>
</table>

   
</asp:Content>
