<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Docente_GrupoServicio.aspx.cs" Inherits="ProyectoNido.frm_Docente_GrupoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/docente_grupo_anual.css" rel="stylesheet" />
    <script src="Js/docente_grupo_anual.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="docente-left">
        <div class="docente-avatar"></div>
        <div class="docente-nombre">
            <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
        </div>
        <div class="docente-menu">
            <a href="frm_Docente_Datos.aspx">Datos</a>
            <a href="frm_Docente_Comunicado.aspx">Ver Comunicado</a>
            <a href="frm_Docente_GrupoAnual.aspx">Grupo Anual</a>
            <a href="frm_Docente_GrupoServicio.aspx" class="activo">Grupo Servicio</a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="grupo-container">
        <h2 class="grupo-titulo-pagina">Mis Grupos de Servicio Asignados</h2>

        <asp:Repeater ID="rptGruposServicio" runat="server">
            <HeaderTemplate>
                <div class="grupo-grid">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="grupo-card">
                    <div class="grupo-header">
                        <div class="grupo-nivel">
                            <%# Eval("Servicio") %>
                        </div>
                        <div class="grupo-periodo">
                            <%# Eval("Periodo") %>
                        </div>
                    </div>
                    <div class="grupo-body">
                        <div class="grupo-info-item">
                            <i class="fas fa-tag"></i>
                            <span><strong>Tipo:</strong>
                                <%# Eval("Tipo") == "H" ? "Por Hora" : Eval("Tipo") == "D" ? "Por Día" : "Por Mes" %>
                            </span>
                        </div>
                        <div class="grupo-info-item">
                            <i class="fas fa-chalkboard-teacher"></i>
                            <span><strong>Salón:</strong>
                                <%# Eval("Salon") %>
                            </span>
                        </div>
                        <div class="grupo-info-item">
                            <i class="fas fa-users"></i>
                            <span><strong>Alumnos:</strong>
                                <%# Eval("TotalAlumnos") %> / <%# Eval("Aforo") %>
                            </span>
                        </div>
                    </div>
                    <div class="grupo-footer">
                        <asp:HyperLink ID="lnkVerAlumnos" runat="server" CssClass="btn-ver-alumnos"
                            NavigateUrl='<%# "frm_Docente_AlumnosServicio.aspx?idGrupo=" + Eval("Id_GrupoServicio") %>'>
                            Ver Alumnos <i class="fas fa-arrow-right"></i>
                        </asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlSinGrupos" runat="server" Visible="false" CssClass="sin-grupos">
            <i class="fas fa-folder-open" style="font-size: 48px; margin-bottom: 15px;"></i>
            <h3>No tienes grupos de servicio asignados para este periodo.</h3>
        </asp:Panel>
    </div>
</asp:Content>
