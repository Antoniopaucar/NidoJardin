<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Alumno.aspx.cs" Inherits="ProyectoNido.frm_Alumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaAlumnos" class="tablaForm" >
    <tr class="titulo">
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Alumnos"></asp:Label>
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
            <asp:Label ID="Label4" runat="server" Text="Apoderado:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="Ddl_Apoderado" runat="server"></asp:DropDownList>
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
        <td>
            <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                OnClientClick="return confirm('¿Deseas agregar este Alumno?') && validarCamposTabla('tablaAlumnos','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,fup_Fotos,fup_Copia_Dni,fup_Permiso_Publicidad,fup_Carnet_Seguro') ;" 
                OnClick="btn_Agregar_Click" class="btn btn-exito" />
        </td>
        <td>
            <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                OnClientClick="return confirm('¿Deseas modificar este Alumno?') && validarCamposTabla('tablaAlumnos','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,fup_Fotos,fup_Copia_Dni,fup_Permiso_Publicidad,fup_Carnet_Seguro') ;" 
                OnClick="btn_Modificar_Click" class="btn btn-primario" />
        </td>
        <td>
            <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
        </td>
        <td>
            <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                OnClientClick="return confirm('¿Deseas eliminar este Alumno?');"
                OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por nombres, apellidos o documento ..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

        <asp:GridView ID="gvAlumnos" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
            OnPageIndexChanging="gvAlumnos_PageIndexChanging"
            OnRowCommand="gvAlumnos_RowCommand"
            CssClass="gridview-style sticky-header"> 
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="ApellidoPaterno" HeaderText="Apellido Paterno" />
                <asp:BoundField DataField="ApellidoMaterno" HeaderText="Apellido Materno" /> 
                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                
                <asp:BoundField DataField="EstadoFotos" HeaderText="Fotos" />
                <asp:BoundField DataField="EstadoCopiaDni" HeaderText="Copia Dni" />
                <asp:BoundField DataField="EstadoPermisoPublicidad" HeaderText="Permiso Publicidad" />
                <asp:BoundField DataField="EstadoCarnetSeguro" HeaderText="Carnet Seguro" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-info btn-sm" />
                        <asp:Button ID="btnVerFotos" runat="server" Text="Ver Fotos" CommandName="Fotos"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-exito btn-sm" />
                        <asp:Button ID="btnVerCopiaDni" runat="server" Text="Ver Copia Dni" CommandName="CopiaDni"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-exito btn-sm" />
                        <asp:Button ID="btnVerPermisoPublicidad" runat="server" Text="Ver Permiso" CommandName="Permiso"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-exito btn-sm" />
                        <asp:Button ID="btnVerCarnetSeguro" runat="server" Text="Ver Carnet" CommandName="Carnet"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-exito btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
