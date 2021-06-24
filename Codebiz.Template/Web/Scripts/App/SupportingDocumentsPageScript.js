$(document).ready(function () {
    var lastRowIndex = 0;
    var deleteBtn = '<button class="btn btn-danger">' +
        '<i class="glyphicon glyphicon-trash"></i>' +
        '<span>Delete</span>' +
        '</button >';
    var table = initDataTable();
    addRow();

    
    //#region Event handlers

    // Delete a record
    $('#supportingDocumentsTable').on('click', 'button.btn-danger', function (e) {
        e.preventDefault();
        table.row($(this).parents('tr'))
            .remove()
            .draw();
        lastRowIndex--;
    });

    //Insert row
    $('#supportingDocumentsTable').on('blur', 'input.form-control', function (e) {
        var inputVal = $(this).val();
        var checkboxVal = $(this).closest('tr').find(':checkbox').prop("checked");

        if (inputVal.length) {
            lastRowIndex++;
            addRow(inputVal, checkboxVal);
            //editRow(inputVal, checkboxVal, $(this).closest('tr'));
            $(this).closest("tr").find("td:last").html(deleteBtn);
        }
        console.log(table.rows().data());
    });

    //#Insert row if check
    $('#supportingDocumentsTable').on('change', ':checkbox',function () {
        if (this.checked) {
            lastRowIndex++;
            addRow();
        }
    });

    //#endregion

    //#region Private

    function addRow(inputVal, checkboxVal) {
        table.row.add([
            inputVal,
            checkboxVal,
            ''
        ]).draw();
    }

    function editRow(inputVal, checkboxVal, element) {
        table.row(element).data([
            inputVal,
            checkboxVal,
            ''
        ]).draw();
    }

    function initDataTable() {
        return $('#supportingDocumentsTable').DataTable({
            "ordering": false,
            "paging": false,
            "scrollY": "350px",
            "scrollCollapse": true,
            "searching": false,
            "responsive": true,
            "columnDefs": [
                {
                    "render": function (data, type, row) {
                        return '<input type="text" class="form-control"/>';
                    },
                    "targets": 0
                },
                {
                    'render': function (data, type, full, meta) {
                        return '<label class="mt-checkbox" style="text-align: center">' +
                            '<input type="checkbox" name="id[]" value="'
                            + $('<div/>').text(data).html() + '">' +
                            '<span></span>' +
                            '</label>';
                    },
                    "targets": 1,
                    'className': 'dt-center'
                },
                {
                    'render': function (data, type, full, meta) {
                        var btn = '';
                        if (lastRowIndex !== meta.row) {
                            btn = deleteBtn;
                        }

                        return btn;
                    },
                    "targets": 2
                }
            ]
        });
    }

    //#endregion
});