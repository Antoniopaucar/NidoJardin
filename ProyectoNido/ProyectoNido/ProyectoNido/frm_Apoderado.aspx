<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado.aspx.cs" Inherits="ProyectoNido.frm_Apoderado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server" >
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
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
    <div style="display: flex; justify-content: center; gap: 10px;">
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por Usuario, Nombres, Apellidos o Dni ..." />
        <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
    </div>
    <div style="margin-top: 10px;">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
    </div>
</div>


<asp:GridView ID="gvApoderados" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
    OnPageIndexChanging="gvApoderados_PageIndexChanging"
    OnRowCommand="gvApoderados_RowCommand"
    CssClass="gridview-style sticky-header"> 
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
        <asp:BoundField DataField="ApellidoPaterno" HeaderText="Apellido Paterno" />
        <asp:BoundField DataField="ApellidoMaterno" HeaderText="Apellido Materno" />
        <asp:BoundField DataField="Documento" HeaderText="Documento" />
        <asp:BoundField DataField="EstadoArchivo" HeaderText="Copia Dni" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                    CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnVerArchivo" runat="server" Text="Ver Archivo" CommandName="VerArchivo"
                    CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-exito btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>
