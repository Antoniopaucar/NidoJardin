<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Tarifario.aspx.cs" Inherits="ProyectoNido.frm_Tarifario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaTarifario" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="LabelTitulo" runat="server" Text="Tarifario"></asp:Label>
            </td>
        </tr>

        <!-- Id -->
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Id Tarifario:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdTarifario" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <!-- Tipo -->
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Tipo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_Tipo" runat="server">
                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                    <asp:ListItem Value="M">Matrícula</asp:ListItem>
                    <asp:ListItem Value="C">Cuota</asp:ListItem>
                    <asp:ListItem Value="S">Servicio</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <!-- Nombre -->
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Nombre" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- Descripción -->
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Descripción:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Descripcion" runat="server" CssClass="full-width-textbox" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- Periodo -->
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Periodo:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Periodo" runat="server" onkeypress="return SoloNumeros(event);" CssClass="full-width-textbox" MaxLength="4"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- Valor -->
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Valor:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Valor" runat="server" onkeypress="return SoloNumeros(event);" CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- Botones -->
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Tarifario?') 
                        && validarCamposTabla('tablaTarifario','ddl_Tipo,txt_Nombre,txt_Periodo,txt_Valor');"
                    OnClick="btn_Agregar_Click"
                    CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Tarifario?') 
                        && validarCamposTabla('tablaTarifario','ddl_Tipo,txt_Nombre,txt_Periodo,txt_Valor');"
                    OnClick="btn_Modificar_Click"
                    CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click"
                    CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Tarifario?');"
                    OnClick="btn_Eliminar_Click"
                    CssClass="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por nombre, periodo o tipo..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvTarifario" runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True" PageSize="5"
        CssClass="gridview-style sticky-header"
        OnPageIndexChanging="gvTarifario_PageIndexChanging"
        OnRowCommand="gvTarifario_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id_Tarifario" HeaderText="ID" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
            <asp:BoundField DataField="Valor" HeaderText="Valor" DataFormatString="{0:N2}" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_Tarifario") %>'
                        CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id_Tarifario") %>'
                        CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este tarifario?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
