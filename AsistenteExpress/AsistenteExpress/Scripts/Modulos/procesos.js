$(document).ready(function () {
    FindProcesos();
    Limpiar();
    $("#datetimepicker").kendoDateTimePicker({
        value: new Date(),
        dateInput: true
    });
});

function FindProcesos() {
    let JsonRequest = FillObject();
    if (JsonRequest != null)
        Buscar("/Asistente/Procesos/", JsonRequest, function (data) {
            CreateGrid(data);
        });
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

function Buscar(url, JsonRequest, functionOK) {
    var jqxhr = $.get(url, JsonRequest)
        .done(function (data) {            
            functionOK(data);
            return;
        })
        .fail(function (e) {
            //alert("error");
            console.log(e);
            CreateGrid([]);
            return;
        });
}

function Save(url, JsonRequest, functionOK) {
    var jqxhr = $.post(url, JsonRequest)
        .done(function (data) {            
            functionOK(data);                                   
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
                name: "",
                template: function myfunction(data) {
                    return "<button onclick='Ver(" + data.Id + ")'>Ver</button> <button onclick='editar(" + data.Id + ")'>Editar</button> <button onclick='Eliminar(" + data.Id +")'>Eliminar</button>";
                }
            },
        ],
        edit: function (e) {
            return;
        }
    });
}

function Ver(idProceso) {
    var data = $('#grid').data("kendoGrid").dataSource.data();
    data = data.find(function (data) {
        return data.Id == idProceso;
    });
    $("#IdProceso").val(data.Id);
    $("#Asunto").val(data.Asunto);
    document.getElementById("Asunto").disabled = true;
    document.getElementById("btnGuardar").disabled = true;
}

function editar(idProceso) {    
    var data = $('#grid').data("kendoGrid").dataSource.data();
    data = data.find(function (data) {
        return data.Id == idProceso;
    });
    $("#IdProceso").val(data.Id);
    $("#Asunto").val(data.Asunto);

    document.getElementById("liarchivos").innerHTML = "";
    Buscar("/Asistente/Archivos/", { aIdProceso: data.Id }, function (data) {
        data.forEach(function myfunction(archivo) {
            $("#liarchivos").append('<li><a href="#" onclick=viewPdf("' + (window.location.href.replace("#","")) + "Content/Temp/" + archivo.NameFile + '")>' + archivo.NameFile + '</a></li>');            
        });
    });

    document.getElementById("Asunto").disabled = false;
    document.getElementById("btnGuardar").disabled = false;
}

function viewPdf(urlPDF) {
    PDFObject.embed(urlPDF, "#pdfRenderer");
    document.getElementById("pdfviewer").style.display = "block";
}

function Eliminar(idProceso) {
    let JsonRequest = FillObject();
    JsonRequest.aIdProceso = idProceso;
    Buscar("/Asistente/Delete/", JsonRequest, function (data) {
        CreateGrid(data);
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

    let files = document.getElementById("DocFile").files;
    if (files.length == 0 && idProceso == 0) {
        swal("Carga un archivo para guardar");
        return;
    }

    if (idProceso == 0)
        Save("/Asistente/Create/", jsonRequest, function (data) {
            UploadFile(data);
            FindProcesos();

        });
    else {
        if (document.querySelectorAll("#liarchivos li").length > 0 && files.length != 0) {
            Save("/Asistente/Edit/", jsonRequest, function (data) {
                UploadFile(data);
                FindProcesos();
            });
        } else {
            swal("Carga un archivo para guardar");
            return;
        }
    }
        


    Limpiar();
}

function UploadFile(data) {
    var file = $('#DocFile').get(0);
    var img = file;
    var ajaxData = new FormData();
    var nameFile = "";
    $.each($(img), function (i, obj) {
        nameFile = obj.name;
        $.each(obj.files, function (i, file) {
            ajaxData.append(nameFile, file);
        })
    });

    $.ajax({
        url: '/Asistente/UploadFile/?IdProceso=' + data?.Id,
        type: "POST",
        contentType: false,
        processData: false,
        data: ajaxData,
        success: function (data) {
            if (data.Msj == "OK") {
                $("#DocFile").val("");
                document.getElementById("liarchivos").innerHTML = "";
            } else {
                alert("Hubo un error, Intenta nuevamente");
            }
        },
        error: function (err) {
            alert("Hubo un error, Intenta nuevamente");
        }
    });
}

function Limpiar() {
    $("#IdProceso").val("");
    $("#Asunto").val("");
    document.getElementById("liarchivos").innerHTML = "";
    $("#DocFile").val("");
    document.getElementById("pdfviewer").style.display = "none";
}

function LimpiarTodo() {
    Limpiar();
}

