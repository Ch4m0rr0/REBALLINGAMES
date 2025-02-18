﻿var tabladata;

$(document).ready(function () {

    // Validación del formulario
    $("#form").validate({
        rules: {
            Nombre_Almacen: "required",
            Descripcion: "required"
        },
        messages: {
            Nombre_Almacen: "(Este campo es obligatorio)",
            Descripcion: "(Este campo es obligatorio)"
        },
        errorElement: 'span'
    });

    // Inicialización del DataTable
    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": "/Almacen/Obtener",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id_Almacen" },
            { "data": "nombre_Almacen" },
            { "data": "ubicacion" },
            { "data": "descripcion" },
            {
                "data": "id_Almacen", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i> Editar</button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i> Eliminar</button>";
                },
                "orderable": false,
                "searchable": false,
                "width": "150px"
            }
        ],
        "language": {
            "processing": "Procesando...",
            "lengthMenu": "Mostrar _MENU_ registros",
            "zeroRecords": "No se encontraron resultados",
            "emptyTable": "Ningún dato disponible en esta tabla",
            "info": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "infoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "search": "Buscar:",
            "infoThousands": ",",
            "loadingRecords": "Cargando...",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "aria": {
                "sortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sortDescending": ": Activar para ordenar la columna de manera descendente"
            },
            "buttons": {
                "copy": "Copiar",
                "colvis": "Visibilidad",
                "collection": "Colección",
                "colvisRestore": "Restaurar visibilidad",
                "copyKeys": "Presione ctrl o ⌘ + C para copiar los datos de la tabla al portapapeles del sistema. <br \/> <br \/> Para cancelar, haga clic en este mensaje o presione escape.",
                "copySuccess": {
                    "1": "Copiada 1 fila al portapapeles",
                    "_": "Copiadas %d filas al portapapeles"
                },
                "copyTitle": "Copiar al portapapeles",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Mostrar todas las filas",
                    "1": "Mostrar 1 fila",
                    "_": "Mostrar %d filas"
                },
                "pdf": "PDF",
                "print": "Imprimir"
            },
            "autoFill": {
                "cancel": "Cancelar",
                "fill": "Rellene todas las celdas con <i>%d<\/i>",
                "fillHorizontal": "Rellenar celdas horizontalmente",
                "fillVertical": "Rellenar celdas verticalmente"
            },
            "decimal": ",",
            "searchBuilder": {
                "add": "Añadir condición",
                "button": {
                    "0": "Constructor de búsqueda",
                    "_": "Constructor de búsqueda (%d)"
                },
                "clearAll": "Borrar todo",
                "condition": "Condición",
                "conditions": {
                    "date": {
                        "after": "Después",
                        "before": "Antes",
                        "between": "Entre",
                        "empty": "Vacío",
                        "equals": "Igual a",
                        "notBetween": "No entre",
                        "notEmpty": "No vacío",
                        "not": "Diferente de"
                    },
                    "number": {
                        "between": "Entre",
                        "empty": "Vacío",
                        "equals": "Igual a",
                        "gt": "Mayor a",
                        "gte": "Mayor o igual a",
                        "lt": "Menor que",
                        "lte": "Menor o igual que",
                        "notBetween": "No entre",
                        "notEmpty": "No vacío",
                        "not": "Diferente de"
                    },
                    "string": {
                        "contains": "Contiene",
                        "empty": "Vacío",
                        "endsWith": "Termina en",
                        "equals": "Igual a",
                        "notEmpty": "No vacío",
                        "startsWith": "Empieza con",
                        "not": "Diferente de"
                    },
                    "array": {
                        "not": "Diferente de",
                        "equals": "Igual",
                        "empty": "Vacío",
                        "contains": "Contiene",
                        "notEmpty": "No vacío",
                        "without": "Sin"
                    }
                },
                "data": "Data",
                "deleteTitle": "Eliminar regla de filtrado",
                "leftTitle": "Criterios anulados",
                "logicAnd": "Y",
                "logicOr": "O",
                "rightTitle": "Criterios de sangría",
                "title": {
                    "0": "Constructor de búsqueda",
                    "_": "Constructor de búsqueda (%d)"
                },
                "value": "Valor"
            },
            "searchPanes": {
                "clearMessage": "Borrar todo",
                "collapse": {
                    "0": "Paneles de búsqueda",
                    "_": "Paneles de búsqueda (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown} ({total})",
                "emptyPanes": "Sin paneles de búsqueda",
                "loadMessage": "Cargando paneles de búsqueda",
                "title": "Filtros Activos - %d"
            },
            "select": {
                "1": "%d fila seleccionada",
                "_": "%d filas seleccionadas",
                "cells": {
                    "1": "1 celda seleccionada",
                    "_": "$d celdas seleccionadas"
                },
                "columns": {
                    "1": "1 columna seleccionada",
                    "_": "%d columnas seleccionadas"
                }
            },
            "thousands": ".",
            "datetime": {
                "previous": "Anterior",
                "next": "Próximo",
                "hours": "Horas",
                "minutes": "Minutos",
                "seconds": "Segundos",
                "unknown": "-",
                "amPm": [
                    "am",
                    "pm"
                ]
            },
            "editor": {
                "close": "Cerrar",
                "create": {
                    "button": "Nuevo",
                    "title": "Crear Nuevo Registro",
                    "submit": "Crear"
                },
                "edit": {
                    "button": "Editar",
                    "title": "Editar Registro",
                    "submit": "Actualizar"
                },
                "remove": {
                    "button": "Eliminar",
                    "title": "Eliminar Registro",
                    "submit": "Eliminar",
                    "confirm": {
                        "_": "¿Está seguro que desea eliminar %d filas?",
                        "1": "¿Está seguro que desea eliminar 1 fila?"
                    }
                },
                "error": {
                    "system": "Ha ocurrido un error en el sistema (<a target=\"\\\" rel=\"\\ nofollow\" href=\"\\\">Más información<\/a>)."
                },
                "multi": {
                    "title": "Múltiples Valores",
                    "info": "Los elementos seleccionados contienen diferentes valores para este registro. Para editar y establecer todos los elementos de este registro con el mismo valor, haga clic o toque aquí, de lo contrario conservarán sus valores individuales.",
                    "restore": "Deshacer Cambios",
                    "noMulti": "Este registro puede ser editado individualmente, pero no como parte de un grupo."
                }
            }
        },
        responsive: true
    });

});

// Función para abrir el modal con los datos del almacén
function abrirPopUpForm(json) {
    $("#txtid").val(0);

    if (json != null) {
        $("#txtid").val(json.id_Almacen);
        $("#txtNombre_Almacen").val(json.nombre_Almacen);
        $("#txtUbicacion").val(json.ubicacion);
        $("#txtDescripcion").val(json.descripcion);
    } else {
        $("#txtNombre_Almacen").val("");
        $("#txtUbicacion").val("");
        $("#txtDescripcion").val("");
    }

    $('#FormModal').modal('show');
}

// Función para guardar los datos del almacén
function Guardar() {
    if ($("#form").valid()) {
        var request = {
            id_Almacen: parseInt($("#txtid").val()),
            nombre_Almacen: $("#txtNombre_Almacen").val(),
            ubicacion: ($("#txtUbicacion").val() != "" ? $("#txtUbicacion").val() : ""),
            descripcion: ($("#txtDescripcion").val() != "" ? $("#txtDescripcion").val() : "")
        };

        jQuery.ajax({
            url: "/Almacen/Guardar",
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

// Función para eliminar un almacén
function eliminar(idAlmacen) {
    Swal.fire({
        title: '¿Está seguro de eliminar el registro?',
        text: "¡Esta acción no podrá revertirse!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, Eliminarlo',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            jQuery.ajax({
                url: "/Almacen/Eliminar?id=" + idAlmacen,
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
