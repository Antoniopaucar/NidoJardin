<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado_OfertaGrupoServicio.aspx.cs" Inherits="ProyectoNido.frm_Apoderado_OfertaGrupoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/docente_grupo_anual.css" rel="stylesheet" />
    <link href="CSS/apoderado_oferta_servicio.css" rel="stylesheet" />
    <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="docente-left">
        <div class="docente-avatar"></div>
        <div class="docente-nombre">
            <asp:Label ID="lblNombreApoderado" runat="server" Text="nombre y apellido"></asp:Label>
        </div>
        <div class="docente-menu">
            <a href="frm_Apoderado_Datos.aspx">Datos</a>
            <a href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
            <a class="activo" href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
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
    <div class="grupo-container" style="max-width: 750px; margin: 0 auto;">
        <h2 class="grupo-titulo-pagina">Ofertas de Servicios Adicionales</h2>
        <p class="grupo-subtitulo">Periodo Actual - Selecciona un servicio para ver los grupos disponibles</p>

        <asp:Repeater ID="rptOfertas" runat="server">
            <ItemTemplate>
                <div class="oferta-servicio-card">
                    <div class="oferta-servicio-header">
                        <h3 class="oferta-servicio-nombre"><%# Eval("Servicio") %></h3>
                        <div class="oferta-servicio-costo">
                            S/. <%# Eval("Costo", "{0:F2}") %>
                        </div>
                    </div>
                    <div class="oferta-servicio-body">
                        <div class="oferta-servicio-descripcion">
                            <p><%# Eval("Descripcion") %></p>
                        </div>
                        <div class="oferta-servicio-info">
                            <div class="oferta-info-item">
                                <i class="fas fa-tag"></i>
                                <span><strong>Tipo:</strong> 
                                    <%# Eval("Tipo") == "H" ? "Por Hora" : Eval("Tipo") == "D" ? "Por Día" : "Por Mes" %>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="oferta-grupo-card">
                        <div class="oferta-grupo-header">
                            <span class="oferta-grupo-periodo">Periodo <%# Eval("Periodo") %></span>
                            <span class="oferta-grupo-salon">Salón: <%# Eval("Salon") %></span>
                        </div>
                        <div class="oferta-grupo-body">
                            <div class="oferta-grupo-info">
                                <i class="fas fa-chalkboard-teacher"></i>
                                <span><strong>Docente:</strong> <%# (Eval("NombreDocente") ?? "").ToString() + " " + (Eval("ApellidoPaternoDocente") ?? "").ToString() + " " + (Eval("ApellidoMaternoDocente") ?? "").ToString() %></span>
                            </div>
                            <div class="oferta-grupo-info">
                                <i class="fas fa-users"></i>
                                <span><strong>Cupos:</strong> 
                                    <%# (Convert.ToInt32(Eval("Aforo")) - Convert.ToInt32(Eval("TotalAlumnos"))) > 0 
                                        ? (Convert.ToInt32(Eval("Aforo")) - Convert.ToInt32(Eval("TotalAlumnos"))).ToString() + " disponibles" 
                                        : "<span style='color: red; font-weight: bold;'>AGOTADO</span>" %>
                                </span>
                            </div>
                            <div class="oferta-grupo-info">
                                <i class="fas fa-info-circle"></i>
                                <span><strong>Capacidad:</strong> <%# Eval("TotalAlumnos") %> / <%# Eval("Aforo") %></span>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlSinOfertas" runat="server" Visible="false" CssClass="sin-grupos">
            <i class="fas fa-folder-open" style="font-size: 48px; margin-bottom: 15px;"></i>
            <h3>No hay ofertas de servicios disponibles para el periodo actual.</h3>
        </asp:Panel>
    </div>
</asp:Content>
