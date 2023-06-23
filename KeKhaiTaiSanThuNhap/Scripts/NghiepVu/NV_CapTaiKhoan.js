
function loadDataTable() {

    t = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,
        "language": {
            "search": "",
            "info": "Tổng số _TOTAL_ hàng",
            "infoEmpty": "",
            "infoFiltered": "",
            "paginate": {
                "next": "»",
                "previous": "«"
            },
            "processing": `Đang tải dữ liệu`,
            searchPlaceholder: "Tìm...",
            zeroRecords: "Không tìm thấy kết quả",
        },
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-file-excel"></i>',
                extend: 'excel',
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3'
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2'
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2'
            }
        ],

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/NV_CapTaiKhoan/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [4] }
        ],
        "columns": [
            { "data": "HoTen" },
            { "data": "Ten" },
            { "data": "HoTen" },
            {
                "data": "NgayCap", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },

            {
                "data": { "FileCap": "FileCap", "ID": "ID" }, "render": function (data, type, row) {
                    if (data.FileCap != null) {
                        return ` <a class="btn btn-outline-info btn-sm" href="/NV_CapTaiKhoan/Download?id=${data.ID}">
                                <i class="fa fa-download" aria-hidden="true"></i> ${data.FileCap}
                            </a>
                            `
                    } else {
                        return `Đang chờ cấp`
                    }

                }
            },
        ]

    });
    t.on('draw.dt', function () {
        var info = t.page.info();
        t.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
}

$(document).ready(function () {
    loadDataTable();

    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        t.column(1).search(searchValue);
        t.draw()
    })

    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            t.columns(1).search($("#Filter").val());
            t.draw();
        }
    });
})

