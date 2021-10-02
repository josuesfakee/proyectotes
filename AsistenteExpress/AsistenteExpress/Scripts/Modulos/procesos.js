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
                       

        JsonRequest = {};
        JsonRequest.aIdCampania = parseInt(idCampania);
        if ($("#perfil").val() != "")
            JsonRequest.aIdPerfil = parseInt($("#perfil").val());

        if ($("#tipo").val() != "")
            JsonRequest.aIdPerfil = parseInt($("#tipo").val());

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
        editable: false,
        pageable: {
            refresh: false,
            pageSize: 5,
            pageSizes: [10, 20, 30, 50],
            buttonCount: 5,
            messages: {
                itemsPerPage: "Items por pagina",
                first: "Principio",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultimo",
            }
        },
        sortable: true,
        navigatable: true,
        resizable: true,
        reorderable: true,
        dataBound: childGridDataBound,
        //groupable: true,
        filterable: true,
        //dataBound: onDataBound,
        toolbar: "<button class='k-button' onclick='Nuevo()'>Nuevo Catalogo</button>",
        columns: [
            {
                field: "Asunto",
                title: "Proceso",
                width: 140
            },
            {
                name: "",
                template: function myfunction(data) {
                    return "<button class='btn btn-normal' onclick='Ver(" + data.Id + ")'>Ver</button> <button class='btn btn-warning' onclick='editar(" + data.Id + ")'>Editar</button> <button class='btn btn-danger' onclick='Eliminar(" + data.Id +")'>Eliminar</button>";
                }
            },
        ],
        edit: function (e) {
            return;
        }
    });
}

function Nuevo() {    
    $("#IdProceso").val(0);
    $("#Asunto").val("");
    document.getElementById("TitleCat").innerText = "Crear Proceso";
    DisplayModal("myModalForm");
}

function DisplayModal(ModalName) {

    document.getElementById("btnGuardar").disabled = false;

    var modal = document.getElementById(ModalName);
    modal.style.display = "block";

    var span = document.getElementsByClassName("close_" + ModalName)[0];

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    if (span != undefined) {
        span.onclick = function () {
            modal.style.display = "none";
        }
    }    
}

function Ver(idProceso) {
    
    Buscar("/Asistente/Archivos/", { aIdProceso: idProceso }, function (data) {
        data.forEach(function myfunction(archivo) {
            viewPdf(window.location.origin.replace("#", "") + "/Content/Temp/" + archivo.NameFile, idProceso);
        });
    });   
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
            let item = "<li><a href='#' onclick=viewPdf('" + (window.location.origin.replace("#", "")) + '/Content/Temp/' + archivo.NameFile + "','" + archivo.IdProceso + "');>" + archivo.NameFile + "</a></li>";
            $("#liarchivos").append(item);            
        });
    });

    document.getElementById("TitleCat").innerText = "Editar Proceso";

    DisplayModal("myModalForm");
}

function viewPdf(urlPDF, idProceso) {
    if (urlPDF.indexOf("pdf") > -1) 
        PDFObject.embed(urlPDF, "#pdfRenderer");  
    else    
        document.getElementById("pdfRenderer").innerHTML = '<img src="' + urlPDF +'" style="width: 100%;" alt="Flowers in Chania"/>';

    Buscar("/Asistente/AddClicks/", { "aIdPrceso": parseInt(idProceso) }, function (data) { });

    DisplayModal("myModalViewer");
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
    jsonRequest.IdTipo = $("#tipo").val() != "" ? parseInt($("#tipo").val()) : null;
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
        Save("/Asistente/Edit/", jsonRequest, function (data) {
            UploadFile(data);
            FindProcesos();
        });
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
                $("#DocFile").val("");
            } else {
                alert("Hubo un error, Intenta nuevamente");
                $("#DocFile").val("");
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
    document.getElementById("myModalForm").style.display = "none";   
}

function LimpiarTodo() {
    Limpiar();
}

