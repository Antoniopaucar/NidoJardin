<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_GrupoServicio.aspx.cs" Inherits="ProyectoNido.frm_GrupoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <table id="tablaGrupoServicio" class="tablaForm">
        <!-- ID GRUPO SERVICIO -->
        <tr>
            <td>Id Grupo Servicio:</td>
            <td>
                <asp:TextBox ID="txt_IdGrupoServicio" runat="server" Width="120" />
            </td>
        </tr>

        <!-- SALÓN -->
        <tr>
            <td>Salón:</td>
            <td>
                <asp:HiddenField ID="hdnIdSalon" runat="server" />
                <asp:TextBox ID="txt_SalonSeleccionado" runat="server" ReadOnly="true" Width="250" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalSalon">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- PROFESOR -->
        <tr>
            <td>Profesor:</td>
            <td>
                <asp:HiddenField ID="hdnIdProfesor" runat="server" />
                <asp:TextBox ID="txt_ProfesorSeleccionado" runat="server" ReadOnly="true" Width="250" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalProfesor">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- SERVICIO ADICIONAL -->
        <tr>
            <td>Servicio Adicional:</td>
            <td>
                <asp:HiddenField ID="hdnIdServicio" runat="server" />
                <asp:TextBox ID="txt_ServicioSeleccionado" runat="server" ReadOnly="true" Width="250" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalServicio">
                    Buscar
                </button>
            </td>
        </tr>

        <!-- PERIODO -->
        <tr>
            <td>Periodo:</td>
            <td>
                <asp:TextBox ID="txt_Periodo" runat="server"
                    MaxLength="4"
                    placeholder="Ej: 2025" />
            </td>
        </tr>

        <!-- BOTONES -->
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar este Grupo de Servicio?');"
                    OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar este Grupo de Servicio?');"
                    OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar este Grupo de Servicio?');"
                    OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
            </td>
        </tr>
    </table>

    <!-- ========================================================= -->
    <!-- ================ MODAL PROFESOR ========================= -->
    <!-- ========================================================= -->
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

    <!-- ========================================================= -->
    <!-- ================ MODAL SALÓN ============================ -->
    <!-- ========================================================= -->
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

    <!-- ========================================================= -->
    <!-- ================ MODAL SERVICIO ADICIONAL =============== -->
    <!-- ========================================================= -->
    <div class="modal fade" id="modalServicio" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Servicio Adicional</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarServicio" runat="server" Width="200" />
                    <asp:LinkButton ID="btnBuscarServicioModal" runat="server"
                                    CssClass="btn btn-primary btn-sm"
                                    OnClick="btnBuscarServicioModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvServicio" runat="server"
                                  CssClass="table table-hover"
                                  AutoGenerateColumns="False"
                                  DataKeyNames="Id_ServicioAdicional"
                                  OnRowCommand="gvServicio_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Servicio" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
    <div style="display: flex; justify-content: center; gap: 10px;">
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por periodo..." />
        <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
    </div>
    <div style="margin-top: 10px;">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
    </div>
</div>

<asp:GridView ID="gvGrupoServicio" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
    OnPageIndexChanging="gvGrupoServicio_PageIndexChanging"
    OnRowCommand="gvGrupoServicio_RowCommand"
    CssClass="gridview-style sticky-header">

    <Columns>
        <asp:BoundField DataField="Id_GrupoServicio" HeaderText="ID" />
        <asp:BoundField DataField="NombreSalon" HeaderText="Salón" />
        <asp:BoundField DataField="NombreProfesor" HeaderText="Profesor" />
        <asp:BoundField DataField="NombreServicio" HeaderText="Servicio" />
        <asp:BoundField DataField="Periodo" HeaderText="Periodo" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                    CommandArgument='<%# Eval("Id_GrupoServicio") %>' CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                    CommandArgument='<%# Eval("Id_GrupoServicio") %>' CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este Grupo de Servicio?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>
