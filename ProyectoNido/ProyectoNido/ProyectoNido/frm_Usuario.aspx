<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Usuario.aspx.cs" Inherits="ProyectoNido.frm_Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaUsuarios" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Usuarios"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdUsuario" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Usuario:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_NombreUsuario" runat="server" onkeypress="return SinEspacios(event);" CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Contraseña:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Contrasenia" runat="server" TextMode="Password" onkeypress="return SinEspacios(event);" MaxLength="15"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Repetir Contraseña:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Repetir_Contrasenia" runat="server" TextMode="Password" onkeypress="return SinEspacios(event);" MaxLength="15"></asp:TextBox>
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
            <td colspan="2">
                <asp:DropDownList ID="Ddl_Tipo_Documento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Tipo_Documento_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Documento:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Documento" runat="server" onkeypress="return SinEspacios(event);"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Fecha Nacimiento:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Fecha_Nacimiento" runat="server" CssClass="full-width-textbox" TextMode="Date"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Sexo:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="Ddl_Sexo" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Distrito:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="Ddl_Distrito" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Direccion:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Direccion" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Telefono:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Telefono" runat="server" onkeypress="return SoloNumeros(event);" CssClass="full-width-textbox" MaxLength="9"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Email:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Email" runat="server" onkeypress="return SinEspacios(event);" CssClass="full-width-textbox"></asp:TextBox>
            </td>
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
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Usuario?') && validarCamposTabla('tablaUsuarios','txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,Ddl_Distrito,txt_Direccion,txt_Telefono,txt_Email') ;" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                    OnClientClick="return confirm('¿Deseas modificar este Usuario?') && validarCamposTabla('tablaUsuarios','txt_Contrasenia,txt_Repetir_Contrasenia,txt_Nombres,txt_ApellidoPaterno,txt_ApellidoMaterno,txt_Fecha_Nacimiento,Ddl_Sexo,Ddl_Distrito,txt_Direccion,txt_Telefono,txt_Email') ;" 
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                    OnClientClick="return confirm('¿Deseas eliminar este Usuario?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por nombre de usuario, nombres, apellidos o documento ..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
            OnPageIndexChanging="gvUsuarios_PageIndexChanging"
            OnRowCommand="gvUsuarios_RowCommand"
            CssClass="gridview-style sticky-header"> 
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="ApellidoPaterno" HeaderText="Apellido Paterno" />
                <asp:BoundField DataField="ApellidoMaterno" HeaderText="Apellido Materno" /> 
                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                
                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />

                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Activo" HeaderText="Activo" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-info btn-sm" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-peligro btn-sm"
                            OnClientClick="return confirm('¿Deseas eliminar este usuario?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

</asp:Content>
