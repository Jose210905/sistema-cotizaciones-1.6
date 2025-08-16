using System;
using System.Text;
using System.Web.UI;
using SistemaCotizaciones.DAL;

namespace SistemaCotizaciones.Pages
{
    public partial class Diagnostico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EjecutarDiagnostico();
            }
        }

        protected void btnEjecutarDiagnostico_Click(object sender, EventArgs e)
        {
            EjecutarDiagnostico();
        }

        private void EjecutarDiagnostico()
        {
            StringBuilder resultado = new StringBuilder();
            resultado.Append("<div style='font-family: monospace;'>");

            // Test 1: Conexión a la base de datos
            resultado.Append("<h4>🔗 Test 1: Conexión a la Base de Datos</h4>");
            try
            {
                bool conexionOk = DatabaseHelper.TestConnection();
                if (conexionOk)
                {
                    resultado.Append("<p class='status-ok'>✅ Conexión exitosa a la base de datos</p>");
                }
                else
                {
                    resultado.Append("<p class='status-error'>❌ Error de conexión a la base de datos</p>");
                }
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error de conexión: {ex.Message}</p>");
            }

            // Test 2: Verificar cadena de conexión
            resultado.Append("<h4>🔧 Test 2: Configuración de Conexión</h4>");
            try
            {
                string connectionString = DatabaseHelper.ConnectionString;
                if (!string.IsNullOrEmpty(connectionString))
                {
                    resultado.Append("<p class='status-ok'>✅ Cadena de conexión configurada</p>");
                    resultado.Append($"<p><small>Conexión: {connectionString.Substring(0, Math.Min(50, connectionString.Length))}...</small></p>");
                }
                else
                {
                    resultado.Append("<p class='status-error'>❌ Cadena de conexión no configurada</p>");
                }
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error al leer configuración: {ex.Message}</p>");
            }

            // Test 3: Verificar tablas
            resultado.Append("<h4>📊 Test 3: Verificación de Tablas</h4>");
            try
            {
                // Test tabla Usuarios
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                var usuarios = usuarioDAL.ObtenerTodosLosUsuarios();
                resultado.Append($"<p class='status-ok'>✅ Tabla Usuarios: {usuarios.Count} registros</p>");
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error tabla Usuarios: {ex.Message}</p>");
            }

            try
            {
                // Test tabla Clientes
                ClienteDAL clienteDAL = new ClienteDAL();
                var clientes = clienteDAL.ObtenerTodosLosClientes();
                resultado.Append($"<p class='status-ok'>✅ Tabla Clientes: {clientes.Count} registros</p>");
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error tabla Clientes: {ex.Message}</p>");
            }

            try
            {
                // Test tabla Productos
                ProductoDAL productoDAL = new ProductoDAL();
                var productos = productoDAL.ObtenerTodosLosProductos();
                resultado.Append($"<p class='status-ok'>✅ Tabla Productos: {productos.Count} registros</p>");
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error tabla Productos: {ex.Message}</p>");
            }

            try
            {
                // Test tabla Cotizaciones
                CotizacionDAL cotizacionDAL = new CotizacionDAL();
                var cotizaciones = cotizacionDAL.ObtenerTodasLasCotizaciones();
                resultado.Append($"<p class='status-ok'>✅ Tabla Cotizaciones: {cotizaciones.Count} registros</p>");
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-error'>❌ Error tabla Cotizaciones: {ex.Message}</p>");
            }

            // Test 4: Verificar sesión
            resultado.Append("<h4>🔐 Test 4: Estado de Sesión</h4>");
            if (Session["UsuarioLogueado"] != null)
            {
                resultado.Append("<p class='status-ok'>✅ Usuario autenticado</p>");
                resultado.Append($"<p>Usuario: {Session["NombreUsuario"]}</p>");
            }
            else
            {
                resultado.Append("<p class='status-warning'>⚠️ Usuario no autenticado</p>");
            }

            // Test 5: Login de prueba
            resultado.Append("<h4>🔑 Test 5: Login de Prueba</h4>");
            try
            {
                UsuarioDAL usuarioTest = new UsuarioDAL();
                var usuario = usuarioTest.ValidarLogin("admin", "admin123");
                if (usuario != null)
                {
                    resultado.Append("<p class='status-ok'>✅ Credenciales admin/admin123 válidas</p>");
                }
                else
                {
                    resultado.Append("<p class='status-warning'>⚠️ Credenciales admin/admin123 no encontradas en BD</p>");
                    resultado.Append("<p><small>Nota: Se puede usar login hardcodeado</small></p>");
                }
            }
            catch (Exception ex)
            {
                resultado.Append($"<p class='status-warning'>⚠️ Error al probar login BD: {ex.Message}</p>");
                resultado.Append("<p><small>Nota: Se puede usar login hardcodeado</small></p>");
            }

            // Test 6: Configuración del servidor
            resultado.Append("<h4>⚙️ Test 6: Configuración del Servidor</h4>");
            resultado.Append($"<p>Versión .NET Framework: {Environment.Version}</p>");
            resultado.Append($"<p>Servidor: {Request.ServerVariables["SERVER_SOFTWARE"]}</p>");
            resultado.Append($"<p>Ruta de la aplicación: {Server.MapPath("~/")}</p>");

            // Recomendaciones
            resultado.Append("<h4>💡 Recomendaciones</h4>");
            resultado.Append("<ol>");
            resultado.Append("<li>Si hay errores de base de datos, ejecutar el script SQL proporcionado</li>");
            resultado.Append("<li>Verificar que SQL Server esté corriendo</li>");
            resultado.Append("<li>Asegurar que la cadena de conexión sea correcta en Web.config</li>");
            resultado.Append("<li>Probar login con credenciales: admin / admin123</li>");
            resultado.Append("</ol>");

            resultado.Append("</div>");

            lblResultados.Text = resultado.ToString();
        }
    }
}