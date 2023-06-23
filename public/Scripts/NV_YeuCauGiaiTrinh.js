
    var t;

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

            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/NV_YeuCauGiaiTrinh/LoadData",
                "type": "POST",
                "datatype": "json"
            },
            "columns": [
                { "data": "TenKeHoachXacMinh" },
                { "data": "TenKeHoachXacMinh" },
                { "data": "KeHoachNam" },

                { "data": "CanCuQuyetDinh" },

                { "data": "NoiDungXacMinh" },
                { "data": "TenTienDo" },


                {
                    "data": "ID", "render": function (data, type, row) {
                        return ` <a style="cursor:pointer" class="btn-edit" pattern="edit" href="/NV_YeuCauGiaiTrinh/ThongTinKeHoach/${data}"><span class="fa fa-pager"></span> Xem chi tiết</a>`
                    }
                },
            ]

        });
        t.on('draw.dt', function () {
            var info = t.page.info();
            t.column(0, {search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1 + info.start;
            });
        });

    }






    $(document).ready(function () {
        loadDataTable();

    })
