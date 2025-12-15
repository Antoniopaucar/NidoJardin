<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_GrupoAnual.aspx.cs" Inherits="ProyectoNido.frm_GrupoAnual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<style type="text/css">
    .auto-style2 {
        height: 83px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaGrupoAnual" class="tablaForm">

        <!-- ID -->
        <tr>
            <td>Id Grupo Anual:</td>
            <td>
                <asp:TextBox ID="txt_IdGrupoAnual" runat="server" Width="120" />
            </td>
        </tr>

        <!-- SALÓN -->
        <tr>
            <td>Salón:</td>
            <td>
                <asp:HiddenField ID="hdnIdSalon" runat="server" />
                <asp:TextBox ID="txt_SalonSeleccionado" runat="server" ReadOnly="true" Width="250" EnableViewState="false" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalSalon">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- PROFESOR -->
        <tr>
            <td class="auto-style2">Profesor:</td>
            <td class="auto-style2">
                <asp:HiddenField ID="hdnIdProfesor" runat="server" />
                <asp:TextBox ID="txt_ProfesorSeleccionado" runat="server" ReadOnly="true" Width="250" EnableViewState="false" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalProfesor">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- NIVEL -->
        <tr>
            <td>Nivel:</td>
            <td>
                <asp:HiddenField ID="hdnIdNivel" runat="server" />
                <asp:TextBox ID="txt_NivelSeleccionado" runat="server" ReadOnly="true" Width="250" EnableViewState="false" 
                    />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalNivel">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- PERIODO -->
        <tr>
            <td>Periodo:</td>
            <td>
                <asp:TextBox ID="txt_Periodo" runat="server" MaxLength="4" placeholder="Ej: 2025" Width="120" />
            </td>
        </tr>

        <!-- BOTONES -->
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Grupo Anual?');"
                    OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Grupo Anual?');"
                    OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Grupo Anual?');"
                    OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
            </td>
        </tr>

    </table>

    <!-- ======================= MODAL SALON ======================= -->
    <div class="modal fade" id="modalSalon" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Salón</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarSalon" runat="server" Width="200" />
                    <asp:LinkButton ID="btnBuscarSalonModal" runat="server"
                        CssClass="btn btn-primary btn-sm"
                        OnClick="btnBuscarSalonModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvSalon" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="Id_Salon"
                        OnRowCommand="gvSalon_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Salón" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

    <!-- ======================= MODAL PROFESOR ======================= -->
    <div class="modal fade" id="modalProfesor" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Profesor</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarProfesor" runat="server" Width="200" />
                    <asp:LinkButton ID="btnBuscarProfesorModal" runat="server"
                        CssClass="btn btn-primary btn-sm"
                        OnClick="btnBuscarProfesorModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvProfesor" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="Id_Profesor"
                        OnRowCommand="gvProfesor_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Profesor" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

    <!-- ======================= MODAL NIVEL ======================= -->
    <div class="modal fade" id="modalNivel" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Nivel</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarNivel" runat="server" Width="200" />
                    <asp:LinkButton ID="btnBuscarNivelModal" runat="server"
                        CssClass="btn btn-primary btn-sm"
                        OnClick="btnBuscarNivelModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvNivel" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="Id"
                        OnRowCommand="gvNivel_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nivel" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 55%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por periodo..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvGrupoAnual" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
        OnPageIndexChanging="gvGrupoAnual_PageIndexChanging"
        OnRowCommand="gvGrupoAnual_RowCommand"
        CssClass="gridview-style sticky-header">

        <Columns>
            <asp:BoundField DataField="Id_GrupoAnual" HeaderText="ID" />
            <asp:BoundField DataField="NombreSalon" HeaderText="Salón" />
            <asp:BoundField DataField="Aforo" HeaderText="Aforo" />
            <asp:BoundField DataField="NombreProfesor" HeaderText="Profesor" />
            <asp:BoundField DataField="NombreNivel" HeaderText="Nivel" />
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_GrupoAnual") %>' CssClass="btn btn-info btn-sm" />

                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id_GrupoAnual") %>' CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este Grupo Anual?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
