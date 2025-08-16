using System;
using System.Web;
using System.Web.UI;
using SistemaCotizaciones.DAL;

namespace SistemaCotizaciones.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya está logueado, redirigir al dashboard
            if (Session["UsuarioLogueado"] != null)
            {
                Response.Redirect("Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contraseña = txtContrasena.Text.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
                {
                    MostrarMensaje("Por favor ingrese usuario y contraseña", "alert-error");
                    return;
                }

                // Credenciales hardcodeadas para pruebas (como indica el HTML)
                if (usuario == "admin" && contraseña == "admin123")
                {
                    // Establecer sesión
                    Session["UsuarioLogueado"] = true;
                    Session["NombreUsuario"] = "Administrador";
                    Session["IdUsuario"] = 1;

                    // Redirigir al dashboard
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    // También intentar validar con la base de datos si existe
                    try
                    {
                        UsuarioDAL usuarioDAL = new UsuarioDAL();
                        var usuarioValidado = usuarioDAL.ValidarLogin(usuario, contraseña);

                        if (usuarioValidado != null)
                        {
                            // Login exitoso
                            Session["UsuarioLogueado"] = true;
                            Session["NombreUsuario"] = usuarioValidado.NombreUsuario;
                            Session["IdUsuario"] = usuarioValidado.ID;

                            Response.Redirect("Default.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            MostrarMensaje("Usuario o contraseña incorrectos", "alert-error");
                        }
                    }
                    catch
                    {
                        // Si falla la base de datos, mostrar error de credenciales
                        MostrarMensaje("Usuario o contraseña incorrectos", "alert-error");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al iniciar sesión: " + ex.Message, "alert-error");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = tipo;
            pnlMensaje.Visible = true;
        }
    }
}