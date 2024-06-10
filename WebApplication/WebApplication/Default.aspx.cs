using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Models;
using System.IO;

namespace WebApplication
{
    public partial class Default : System.Web.UI.Page
    {
        const string apiUrl = "http://localhost:5293/api/data"; // url de la api
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMsg.InnerText = "Aplique los filtros requeridos y haga clic en buscar";
            ErrorMsg.Attributes["class"] = "text-primary";
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        protected async void btnDownload_Click(object sender, EventArgs e)
        {
            await ExportToExcel();
        }

        /// <summary>
        /// Funcion que carga los datos en la pagina
        /// </summary>
        private async Task LoadData()
        {
            var dataList = await FetchData(apiUrl);

            if (dataList != null)
            {
                var filteredData = DateFilter(dataList);
                GridView1.DataSource = filteredData;
                GridView1.DataBind();
                if (filteredData.Count == 0)
                {
                    ErrorMsg.InnerText = "No hay informacion para mostrar con este rango de fechas";
                    ErrorMsg.Attributes["class"] = "text-danger";
                }
                else
                {
                    ErrorMsg.InnerText = null;
                }
            }
            else
            {
                ErrorMsg.InnerText = "No se encontraron datos";
                ErrorMsg.Attributes["class"] = "text-danger";
            }
        }
        /// <summary>
        /// Funcion para consumir la API y obtener los datos
        /// </summary>
        private async Task<List<Data>> FetchData(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var dataList = JsonSerializer.Deserialize<List<Data>>(jsonResponse, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return dataList;
                    }
                    else
                    {
                        ErrorMsg.InnerText = "Error cargando datos desde la API";
                        ErrorMsg.Attributes["class"] = "text-danger";
                        return null;
                    }
                }
                catch (Exception)
                {
                    ErrorMsg.InnerText = "No hay conexion con la API";
                    ErrorMsg.Attributes["class"] = "text-danger";
                    return null;
                }
            }
        }

        /// <summary>
        ///  Funcion para filtrar los datos con la informacion de los inputs.
        /// </summary>
        private List<Data> DateFilter(List<Data> dataList)
        {
            DateTime? startDate = string.IsNullOrWhiteSpace(this.startDate.Text) ? (DateTime?)null : DateTime.Parse(this.startDate.Text);
            DateTime? endDate = string.IsNullOrWhiteSpace(this.endDate.Text) ? (DateTime?)null : DateTime.Parse(this.endDate.Text);

            if (startDate.HasValue || endDate.HasValue)
            {
                DateTime startDateTime = startDate.HasValue ? startDate.Value.Date : DateTime.MinValue;
                DateTime endDateTime = endDate.HasValue ? endDate.Value.Date.AddDays(1).AddTicks(-1) : DateTime.MaxValue;

                dataList = dataList.Where(data =>
                {
                    DateTime dataDate;
                    bool dateParsed = DateTime.TryParse(data.t_recibo_bgts, out dataDate);

                    if (!dateParsed)
                    {
                        return false;
                    }

                    return dataDate >= startDateTime && dataDate <= endDateTime;
                }).ToList();
            }

            return dataList;
        }

        /// <summary>
        /// Funcion para exportar los datos a excel
        /// </summary>
        private async Task ExportToExcel()
        {
            var dataList = await FetchData(apiUrl);
            var filteredData = DateFilter(dataList);

            //Checando si el rango de fechas devuelve algo, si no previene la descarga
            if (filteredData == null || filteredData.Count == 0)
            {
                ErrorMsg.InnerText = "No hay informacion para mostrar con este rango de fechas";
                ErrorMsg.Attributes["class"] = "text-danger";
                return;
            }


            // Grid temporal que refleja los cambios del grid dependiendo del filtro
            GridView tempGridView = new GridView
            {
                AutoGenerateColumns = false,
                DataSource = filteredData
            };

            //Renombrando los headers con nombres mas apropiados
            tempGridView.Columns.Add(new BoundField { HeaderText = "Cliente", DataField = "cliente" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Clave Pedimento", DataField = "clave_pedimento" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Tipo Operación", DataField = "tipo_operacion" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Referencia", DataField = "referencia" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Pedimento", DataField = "pedimento" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Remesa", DataField = "remesa" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Caja", DataField = "caja" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Sello", DataField = "sello" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "DODA", DataField = "doda" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "CP Folios", DataField = "cp_folios" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Cruce SOIA", DataField = "cruce_soia" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Usuario", DataField = "usuario" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Tiempo Recibo BGTS", DataField = "t_recibo_bgts" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Tiempo ACG Confirmado", DataField = "t_acg_confirmado" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Fecha CCP", DataField = "fecha_ccp" });
            tempGridView.Columns.Add(new BoundField { HeaderText = "Fecha Remesa", DataField = "fecha_remesa" });

            tempGridView.DataBind();

            // Render al grid a un StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    tempGridView.RenderControl(hw);

                    // Salida del archivo
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=1716417755841.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // To confirm that an HtmlForm control is rendered for the specified ASP.NET
            // server control at run time.
        }
    }
}