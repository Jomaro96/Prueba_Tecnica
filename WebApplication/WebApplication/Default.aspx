
<%@  Page Language="C#" Async="true" Title="Prueba Tecnica" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monitor Buckland</title>
    <!-- Bootstrap CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-5"> <!--DIV Container-->
    <h1>Monitor Buckland</h1>
    <form id="form1" runat="server" class="mt-4">
        
            <div class="row">
                <div class="col-md-3 mb-3">
                    <label for="startDate" class="form-label">Fecha de Inicio:</label>
                    <asp:TextBox ID="startDate" runat="server" TextMode="Date" CssClass="form-control" />
                </div>
                <div class="col-md-3 mb-3">
                    <label for="endDate" class="form-label">Fecha Final:</label>
                    <asp:TextBox ID="endDate" runat="server" TextMode="Date" CssClass="form-control" />
                </div>
                <div class="col-md-3 mb-3">
                </div>
                <div class="col-md-3 mb-3 d-flex align-items-end justify-content-end">
                    <asp:Button ID="Button1" runat="server" Text="Buscar" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-md w-50" style="margin-top: 10px;" />
                    <asp:Button ID="Button2" runat="server" Text="Descargar" OnClick="btnDownload_Click" CssClass="btn btn-success btn-md w-50 ms-2" style="margin-top: 10px;" />
                </div>
            </div>
 
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="clave_pedimento" HeaderText="Clave Pedimento" />
                <asp:BoundField DataField="tipo_operacion" HeaderText="Tipo Operacion" />
                <asp:BoundField DataField="referencia" HeaderText="Referencia" />
                <asp:BoundField DataField="pedimento" HeaderText="Pedimento" />
                <asp:BoundField DataField="remesa" HeaderText="Remesa" />
                <asp:BoundField DataField="caja" HeaderText="Caja" />
                <asp:BoundField DataField="sello" HeaderText="Sello" />
                <asp:BoundField DataField="doda" HeaderText="DODA" />
                <asp:BoundField DataField="cp_folios" HeaderText="CP Folios" />
                <asp:BoundField DataField="cruce_soia" HeaderText="Cruce/SOIA" />
                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="t_recibo_bgts" HeaderText="Tiempo Recibo BGTS" />
                <asp:BoundField DataField="t_acg_confirmado" HeaderText="Tiempo ACG Confirmado" />
                <asp:BoundField DataField="fecha_ccp" HeaderText="Fecha CCP" />
                <asp:BoundField DataField="fecha_remesa" HeaderText="Fecha de Remesa" />
            </Columns>
        </asp:GridView>
    </form>
    <span runat="server" id="ErrorMsg" class="text-danger"></span>
  </div><!--Container DIV-->

</body>
</html>
