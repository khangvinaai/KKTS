var s1 = window.location.href.split("/");
var idd = s1[s1.length - 1];

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
            "url": `/NV_CapTaiKhoan_ChiTiet/loadData/${idd}`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "tenCB" },
            { "data": "tenCanBo" },
            {
                "data": "DoB"
            },
           
            {
                "data": "tenCoQuan"
            },
            {
                "data": "ten_ChucVu_ChucDanh"
            },
            {
                "data": "nguoiGui"
            },
            {
                "data": "NgayGui", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss');
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

//Tìm kiếm 
$("#search_btn").click(() => {
    var searchValue = $('#Filter').val().trim()
    t.column(0).search(searchValue);
    t.draw()
})

$("#Filter").keypress(function (e) {
    if (e.keyCode == 13) {
        t.columns(0).search($("#Filter").val());
        t.draw();
    }
});


$(document).ready(function () {
    loadDataTable();
})

