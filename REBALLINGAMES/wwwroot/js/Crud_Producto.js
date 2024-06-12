var tabladataP;

$(document).ready(function () {
    //validamos el formulario
    $("#form").validate({
        rules: {
            Nombre_Producto: "required"
        },
        messages: {
            Nombre_Producto: "(Este campo obligatorio)"
        },
        errorElement: 'span'
    });

    // Obtener categorías al cargar la página
    obtenerCategorias();

    // Inicializar la tabla de productos
    tabladataP = $('#tbdata').DataTable({
        "ajax": {
            "url": "/Producto/Obtener",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Producto" },
            { "data": "nombre_Producto" },
            { "data": "descripcion" },
            {
                "data": "objCategoria", render: function (data) {
                    return data.nombre_Categoria
                }
            },
            {
                "data": "estado", "render": function (data) {
                    if (data) {
                        return '<span class="btn-success btn-sm ml-2">Activo</span>'
                    } else {
                        return '<span class="btn-warning btn-sm ml-2">Inactivo</span>'
                    }
                }, "width": "10%"
            },
            {
                "data": "id_Producto", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpFormP(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i>Editar</button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i>Eliminar</button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "60px"
            }

        ], "language": {
            // Aquí va tu configuración de idioma
        },
        responsive: true
    });

});

function obtenerCategorias() {
    $.ajax({
        url: "/Categoria/Obtener",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ddlCategoria").html("");
            if (data.data != null) {
                $.each(data.data, function (i, item) {
                    if (item.estado == true) {
                        $("<option>").attr({ "value": item.id_Categoria }).text(item.nombre_Categoria).appendTo("#ddlCategoria")
                    }
                })
                $("#ddlCategoria").val($("#ddlCategoria option:first").val());
            }
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {

        },
    });
}

function abrirPopUpFormP(json) {
    $("#txtid").val(0);
    if (json != null) {
        $("#txtid").val(json.id_Producto);
        $("#txtNombre_Producto").val(json.nombre_Producto);
        $("#txtDescripcion").val(json.descripcion);
        $("#ddlCategoria").val(json.id_Categoria);
        $("#cboEstado").val(json.estado == true ? 1 : 0);
        $("#txtid").prop("disabled", true)
    } else {
        $("#txtid").val("AUTOGENERADO");
        $("#txtid").prop("disabled", true)
        $("#txtNombre_Producto").val("");
        $("#txtDescripcion").val("");
        $("#ddlCategoria").val($("#ddlCategoria option:first").val());
        $("#cboEstado").val();
    }
    $('#FormModal').modal('show');
}

function Guardar() {
    if ($("#form").valid()) {
        var request = {
            objeto: {
                id_Producto: parseInt($("#txtid").val()),
                nombre_Producto: $("#txtNombre_Producto").val(),
                descripcion: ($("#txtDescripcion").val() != "" ? $("#txtDescripcion").val() : ""),
                id_Categoria: $("#ddlCategoria").val(),
                estado: ($("#cboEstado").val() == "1" ? true : false)
            }
        }
        $.ajax({
            url: "/Producto/Guardar",
            type: "POST",
            data: request,
            success: function (data) {
                if (data.resultado) {
                    tabladataP.ajax.reload();
                    $('#FormModal').modal('hide');
                } else {
                    Swal.fire("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });
    }
}

function eliminar(idProducto) {
    Swal.fire({
        title: '¿Está seguro de eliminar el registro?',
        text: "Esta acción no podrá revertirse!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, Eliminarlo',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Hacer la solicitud AJAX para eliminar el registro
            $.ajax({
                url: "/Producto/Eliminar?id=" + idProducto,
                type: "DELETE",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.resultado) {
                        // Recargar la tabla después de eliminar el registro
                        tabladataP.ajax.reload();
                        Swal.fire(
                            '¡Eliminado!',
                            'El registro ha sido eliminado.',
                            'success'
                        );
                    } else {
                        Swal.fire("Error", "No se pudo eliminar el registro", "error");
                    }
                },
                error: function (error) {
                    console.log(error);
                    Swal.fire("Error", "Ocurrió un error al intentar eliminar el registro", "error");
                }
            });
        }
    });
}
z