using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using SistemaCotizaciones.DAL;
using SistemaCotizaciones.Models;

namespace SistemaCotizaciones.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario está autenticado de forma segura
            if (Session["UsuarioLogueado"] == null)
            {
                // Redirigir al login sin usar Response.IsRequestBeingRedirected
                try
                {
                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                catch
                {
                    // Si falla la redirección, mostrar mensaje
                    Response.Write("<script>alert('Sesión expirada. Redirigiendo al login...'); window.location='Login.aspx';</script>");
                    return;
                }
            }

            if (!IsPostBack)
            {
                try
                {
                    CargarDatosDashboard();
                    VerificarAlertasCotizaciones();
                }
                catch (Exception ex)
                {
                    // Manejar errores sin redirect para evitar loops
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        $"alert('Error al cargar datos del dashboard: {ex.Message}');", true);
                }
            }
        }

        private void CargarDatosDashboard()
        {
            try
            {
                // Mostrar nombre del usuario
                if (Session["NombreUsuario"] != null)
                {
                    lblUsuario.Text = Session["NombreUsuario"].ToString();
                }
                else
                {
                    lblUsuario.Text = "Usuario";
                }

                // Cargar contadores
                CargarContadores();

                // Cargar cotizaciones recientes
                CargarCotizacionesRecientes();
            }
            catch (Exception ex)
            {
                // Log del error pero continuar
                lblUsuario.Text = "Error al cargar datos";
                ClientScript.RegisterStartupScript(this.GetType(), "error",
                    $"console.log('Error dashboard: {ex.Message}');", true);
            }
        }

        private void CargarContadores()
        {
            try
            {
                // Contador de clientes
                try
                {
                    ClienteDAL clienteDAL = new ClienteDAL();
                    var clientes = clienteDAL.ObtenerTodosLosClientes();
                    lblTotalClientes.Text = clientes.Count.ToString();
                }
                catch
                {
                    lblTotalClientes.Text = "0";
                }

                // Contador de productos
                try
                {
                    ProductoDAL productoDAL = new ProductoDAL();
                    var productos = productoDAL.ObtenerTodosLosProductos();
                    lblTotalProductos.Text = productos.Count.ToString();
                }
                catch
                {
                    lblTotalProductos.Text = "0";
                }

                // Contador de cotizaciones activas
                try
                {
                    CotizacionDAL cotizacionDAL = new CotizacionDAL();
                    var cotizaciones = cotizacionDAL.ObtenerTodasLasCotizaciones();
                    lblTotalCotizaciones.Text = cotizaciones.Count.ToString();
                }
                catch
                {
                    lblTotalCotizaciones.Text = "0";
                }
            }
            catch (Exception ex)
            {
                lblTotalClientes.Text = "Error";
                lblTotalProductos.Text = "Error";
                lblTotalCotizaciones.Text = "Error";
                ClientScript.RegisterStartupScript(this.GetType(), "errorContadores",
                    $"console.log('Error contadores: {ex.Message}');", true);
            }
        }

        private void CargarCotizacionesRecientes()
        {
            try
            {
                CotizacionDAL cotizacionDAL = new CotizacionDAL();
                var cotizaciones = cotizacionDAL.ObtenerTodasLasCotizacionesCompletas();

                // Obtener solo las 5 más recientes
                var cotizacionesRecientes = cotizaciones
                    .OrderByDescending(c => c.FechaCotizacion)
                    .Take(5)
                    .Select(c => new
                    {
                        ID = c.ID,
                        NombreCliente = c.NombreCliente ?? "Sin nombre",
                        NombreProducto = c.NombreProducto ?? "Sin producto",
                        CantidadProducto = c.Cantidad,
                        Total = c.Total,
                        FechaCotizacion = c.FechaCotizacion,
                        Estado = c.Estado ?? "Activa"
                    }).ToList();

                gvCotizacionesRecientes.DataSource = cotizacionesRecientes;
                gvCotizacionesRecientes.DataBind();
            }
            catch (Exception ex)
            {
                gvCotizacionesRecientes.EmptyDataText = "Error al cargar cotizaciones";
                gvCotizacionesRecientes.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "errorCotizaciones",
                    $"console.log('Error cotizaciones recientes: {ex.Message}');", true);
            }
        }

        private void VerificarAlertasCotizaciones()
        {
            try
            {
                CotizacionDAL cotizacionDAL = new CotizacionDAL();
                var cotizacionesPorVencer = cotizacionDAL.ObtenerCotizacionesPorVencer(7);

                if (cotizacionesPorVencer != null && cotizacionesPorVencer.Count > 0)
                {
                    string mensaje = $"<strong>Hay {cotizacionesPorVencer.Count} cotización(es) que vencen pronto:</strong><br/>";

                    foreach (var cotizacion in cotizacionesPorVencer)
                    {
                        string fechaVencimiento = cotizacion.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "Sin fecha";
                        mensaje += $"• Cotización #{cotizacion.ID} - Vence: {fechaVencimiento}<br/>";
                    }

                    lblAlertas.Text = mensaje;
                    pnlAlertas.Visible = true;
                }
                else
                {
                    pnlAlertas.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // No mostrar alertas si hay error, pero log el problema
                pnlAlertas.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "errorAlertas",
                    $"console.log('Error alertas: {ex.Message}');", true);
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar sesión
                Session.Clear();
                Session.Abandon();

                // Redirigir al login de forma segura
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch
            {
                // Si falla la redirección normal, usar JavaScript
                Response.Write("<script>window.location='Login.aspx';</script>");
            }
        }
    }
}