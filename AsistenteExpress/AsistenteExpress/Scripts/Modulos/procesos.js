$(document).ready(function () {
    FindProcesos();
});

function FindProcesos() {
    let JsonRequest = FillObject();
    if (JsonRequest != null)
        Buscar("/Asistente/Procesos/", JsonRequest);
    else
        CreateGrid([]);
}

function FillObject() {
    let JsonRequest = null;

    let idCampania = parseInt(($("#campaña").val() == "") ? 0 : $("#campaña").val());

    if (idCampania != 0) {

        document.getElementById("btnGuardar").disabled = false;

        JsonRequest = {};
        JsonRequest.aIdCampania = parseInt(idCampania);
        if ($("#perfil").val() != "")
            JsonRequest.aIdPerfil = parseInt($("#perfil").val());

        if ($("#motivo").val() != "")
            JsonRequest.aIdMotivo = parseInt($("#motivo").val());

        if ($("#submotivo").val() != "")
            JsonRequest.aIdSubMotivos = parseInt($("#submotivo").val());

    }
    else
        document.getElementById("btnGuardar").disabled = true;

    return JsonRequest;
}

function Buscar(url, JsonRequest) {
    var jqxhr = $.get(url, JsonRequest)
        .done(function (data) {
            CreateGrid(data)
            return;
        })
        .fail(function (e) {
            //alert("error");
            console.log(e);
            CreateGrid([]);
            return;
        });
}

function Save(url, JsonRequest) {
    var jqxhr = $.post(url, JsonRequest)
        .done(function (data) {
            FindProcesos();
            if (url.indexOf("Create") > -1)
                alert("Registro creado correctamente");
            else
                alert("Registro actualizado correctamente");
            
            return;
        })
        .fail(function (e) {
            console.log(e);
            return;
        });
}


function CreateGrid(data) {
    $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: function (e) {
                    e.success(data)
                }
            },
            batch: true,
            schema: {
                model: {
                    fields: {
                        Id: { type: "string" },
                        Asunto: { type: "string" }
                    }
                }
            }
        },
        columnMenu: {
            filterable: false
        },
        height: 250,
        editable: "incell",
        pageable: true,
        sortable: true,
        navigatable: true,
        resizable: true,
        reorderable: true,
        dataBound: childGridDataBound,
        //groupable: true,
        filterable: true,
        //dataBound: onDataBound,
        //toolbar: ["excel", "pdf", "search"],
        columns: [
            {
                field: "Asunto",
                title: "Proceso",
                width: 140
            },
            {
                command: [
                    {
                        name: "Ver",
                        click: function (e) {
                            e.preventDefault();
                            var tr = $(e.target).closest("tr");
                            var data = $("#grid").data("kendoGrid").dataItem(tr);
                            $("#IdProceso").val(data.Id);
                            $("#Asunto").val(data.Asunto);
                            document.getElementById("Asunto").disabled = true;
                            document.getElementById("btnGuardar").disabled = true;
                        }
                    },
                    {
                        name: "Editar",
                        click: function (e) {
                            e.preventDefault();
                            var tr = $(e.target).closest("tr");
                            var data = $("#grid").data("kendoGrid").dataItem(tr);
                            $("#IdProceso").val(data.Id);
                            $("#Asunto").val(data.Asunto);
                            document.getElementById("Asunto").disabled = false;
                            document.getElementById("btnGuardar").disabled = false;
                        }
                    },
                    {
                        name: "Eliminar",
                        click: function (e) {
                            e.preventDefault();
                            var tr = $(e.target).closest("tr");
                            var data = $("#grid").data("kendoGrid").dataItem(tr);
                            let JsonRequest = FillObject();
                            JsonRequest.aIdProceso = data.Id;
                            Buscar("/Asistente/Delete/", JsonRequest);
                        }
                    },
                ]
            }
        ],
        edit: function (e) {
            return;
        }
    });
}

function childGridDataBound(e) {
    var colors = ["red", "yellow", "blue"];
    var ID = e.sender.element[0].id;
    var row = $("#" + ID).find(".k-grid-header tr");
    var cells = row.children();

    cells.css("background-color", "yellow");
}

function Guardar() {
    let jsonRequest = {};

    let idProceso = ($("#IdProceso").val() == "" ? 0 : parseInt($("#IdProceso").val()));
    jsonRequest.IdProceso = idProceso;
    jsonRequest.Asunto = $("#Asunto").val();
    jsonRequest.IdCampania = $("#campaña").val() != "" ? parseInt($("#campaña").val()) : null;
    jsonRequest.IdPerfil = $("#perfil").val() != "" ? parseInt($("#perfil").val()) : null;
    jsonRequest.IdMotivo = $("#motivo").val() != "" ? parseInt($("#motivo").val()) : null;
    jsonRequest.IdSubMotivo = $("#submotivo").val() != "" ? parseInt($("#submotivo").val()) : null;
    if (idProceso == 0)
        Save("/Asistente/Create/", jsonRequest);
    else
        Save("/Asistente/Edit/", jsonRequest);

    Limpiar();
}
function Limpiar() {
    $("#IdProceso").val("");
    $("#Asunto").val("");
}

function LimpiarTodo() {
    Limpiar();
}

