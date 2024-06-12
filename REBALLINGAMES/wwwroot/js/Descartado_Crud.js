var tabladata;

$(document).ready(function () {

    $("#form").validate({
        rules: {
            txtNombre: "required"
        },
        messages: {
            txtNombre: "(Este campo es obligatorio)"
        },
        errorElement: 'span'
    });

    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": "/Descartado/Obtener",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Descartado" },
            { "data": "nombre" },
            {
                "data": "id_Descartado", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i> Editar</button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i> Eliminar</button>";
                },
                "orderable": false,
                "searchable": false,
                "width": "60px"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Spanish.json"
        },
        responsive: true
    });

});

function abrirPopUpForm(json) {
    $("#txtid").val(0);

    if (json != null) {
        $("#txtid").val(json.id_Descartado);
        $("#txtNombre").val(json.Nombre);
    } else {
        $("#txtNombre").val("");
    }

    $('#FormModal').modal('show');
}

function Guardar() {
    if ($("#form").valid()) {
        var idDescartado = ($("#txtid").val() != "") ? parseInt($("#txtid").val()) : 0;
        var request = {
            id_Descartado: idDescartado,
            Nombre: $("#txtNombre").val()
        };

        jQuery.ajax({
            url: "/Descartado/Guardar",
            type: "POST",
            data: request,
            success: function (data) {
                if (data.resultado) {
                    tabladata.ajax.reload();
                    $('#FormModal').modal('hide');
                } else {
                    Swal.fire("Mensaje", "No se pudo guardar los cambios", "warning");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}

function eliminar(idDescartado) {
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
            jQuery.ajax({
                url: "/Descartado/Eliminar?id=" + idDescartado,
                type: "DELETE",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.resultado) {
                        tabladata.ajax.reload();
                        Swal.fire('¡Eliminado!', 'El registro ha sido eliminado.', 'success');
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
