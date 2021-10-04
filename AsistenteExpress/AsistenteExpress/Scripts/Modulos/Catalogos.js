$(document).ready(function () {
    FindProcesos();
    
});

function FindProcesos() {
    let idCatalogo = parseInt(($("#catalogo").val() == "") ? "0" : $("#catalogo").val());
    if (idCatalogo == 0) {
        CreateGrid([]);
        return;
    }

    Buscar("/Catalogos/Details/", { "idCatalogo": idCatalogo }, function (data) {
        CreateGrid(data);
    });
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
    let idCatalogo = parseInt(($("#catalogo").val() == "") ? "0" : $("#catalogo").val());
    if (idCatalogo == 0)
        document.getElementById("GridContent").style.display = "none";
    else
        document.getElementById("GridContent").style.display = "block";

    
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
            filterable: true
        },
        height: 500,
        editable: false,
        pageable: {
            refresh: false,
            pageSize: 10,
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
        filterable: true,
        toolbar: "<button class='k-button' onclick='Nuevo()'>Nuevo Catalogo</button>",
        columns: [
            {
                field: "Descripcion",
                title: "Descripcion",
                width: 140
            },
            {
                field: "Id",
                title: "Commands",
                template: function myfunction(data) {
                    return "<button class='btn btn-warning' onclick='Editar(" + data.Id + ")'>Editar</button> <button class='btn btn-danger' onclick='Eliminar(" + data.Id + ")'>Eliminar</button>";
                },
                visible: false
            },
        ],
        edit: function (e) {
            return;
        }
    });
}

function Nuevo() {
    $("#FieldId").val("0");
    $("#FieldDescirpcion").val("");    
    document.getElementById("TitleCat").innerText = "Crear Catalogo";
    DisplayModal();
}

function Editar(Id) {
    var data = $('#grid').data("kendoGrid").dataSource.data();
    data = data.find(function (data) {
        return data.Id == Id;
    });
    $("#FieldId").val(Id);
    $("#FieldDescirpcion").val(data.Descripcion);    
    document.getElementById("TitleCat").innerText = "Editar Catalogo";
    DisplayModal();
}

function DisplayModal() {
    var modal = document.getElementById("myModal");
    modal.style.display = "block";

    var span = document.getElementsByClassName("close")[0];

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
    span.onclick = function () {
        modal.style.display = "none";
    }
}

function Guardar() {
    let idCatalogo = parseInt(($("#catalogo").val() == "") ? "0" : $("#catalogo").val());
    Save("/Catalogos/Create/", { "Id": idCatalogo, "IdCatalogo": idCatalogo, "Descripcion": $("#FieldDescirpcion").val(), "Estatus": true }, function (data) {
        FindProcesos();
        $("#FieldDescirpcion").val("");
        $("#FieldId").val("0");
        document.getElementById("myModal").style.display = "none";
    });
}

function Eliminar(Id) {
    let idCatalogo = parseInt(($("#catalogo").val() == "") ? "0" : $("#catalogo").val());
    Buscar("/Catalogos/Delete/", { "IdCatalogo": idCatalogo, "Id": Id }, function (data) {
        FindProcesos();
    });
}

function Cancelar() {
    $("#FieldDescirpcion").val("");
    $("#FieldId").val("0");
    document.getElementById("myModal").style.display = "none";
}