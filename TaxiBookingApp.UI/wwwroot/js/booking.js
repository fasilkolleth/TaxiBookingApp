var dataTable;
var dataTable_Previous;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {

    var UserRole = $('#hfUserRole').val();
    var UserID = $('#hfUid').val();

    if (UserRole == 'Admin') {
        dataTable = $('#tblUpcoming').DataTable({

            "ajax": {
                "url": "http://localhost:45235/api/Booking?type=upcoming",
                "dataSrc": ""
            },

            "columns": [
                { "data": "id" },
                { "data": "bookingType" },
                { "data": 'bookingDate', render: $.fn.dataTable.render.moment() },
                { "data": "applicationUser.name" },

                {
                    "data": "locationDescription",

                    "render": function (data, type, row) {
                        return `
                            <a href="javascript:void();" onclick="GetLocation('${row.latitude}', '${row.longitude}');" style="cursor:pointer">
                                ${data} 
                            </a>                            
                            
                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="/Customer/Booking/Upsert/${data}" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                            </div>

                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="javascript:void()" onclick="Delete(${data})" style="cursor:pointer">
                                    <i class="fas fa-trash"></i>
                                </a>
                            </div>

                           `
                    }
                }
            ]
        });

        dataTable_Previous = $('#tblPrevious').DataTable({

            "ajax": {
                "url": "http://localhost:45235/api/Booking?type=previous",
                "dataSrc": ""
            },

            "columns": [
                { "data": "id" },
                { "data": "bookingType" },
                { "data": 'bookingDate', render: $.fn.dataTable.render.moment() },
                { "data": "applicationUser.name" },

                {
                    "data": "locationDescription",

                    "render": function (data, type, row) {
                        return `
                            <a href="javascript:void();" onclick="GetLocation('${row.latitude}', '${row.longitude}');" style="cursor:pointer">
                                ${data} 
                            </a>                            
                            
                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="/Customer/Booking/Upsert/${data}" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                            </div>

                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="javascript:void()" onclick="Delete(${data})" style="cursor:pointer">
                                    <i class="fas fa-trash"></i>
                                </a>
                            </div>

                           `
                    }
                }
            ]
        });
    }

    else {
        dataTable = $('#tblUpcoming').DataTable({

            "ajax": {
                "url": "http://localhost:45235/api/Booking/GetByUser?userId=" + UserID + "&type=upcoming",
                "dataSrc": ""
            },

            "columns": [
                { "data": "id" },
                { "data": "bookingType" },
                { "data": 'bookingDate', render: $.fn.dataTable.render.moment() },

                {
                    "data": "locationDescription",

                    "render": function (data, type, row) {
                        return `
                            <a href="javascript:void();" onclick="GetLocation('${row.latitude}', '${row.longitude}');" style="cursor:pointer">
                                ${data} 
                            </a>                            
                            
                           `
                    }
                },

                { "data": "bookingStatus" },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="/Customer/Booking/Upsert/${data}" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                            </div>

                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="javascript:void()" onclick="Delete(${data})" style="cursor:pointer">
                                    <i class="fas fa-trash"></i>
                                </a>
                            </div>

                           `
                    }
                }
            ]
        });

        dataTable_Previous = $('#tblPrevious').DataTable({

            "ajax": {
                "url": "http://localhost:45235/api/Booking/GetByUser?userId=" + UserID + "&type=previous",
                "dataSrc": ""
            },

            "columns": [
                { "data": "id" },
                { "data": "bookingType" },
                { "data": 'bookingDate', render: $.fn.dataTable.render.moment() },

                {
                    "data": "locationDescription",

                    "render": function (data, type, row) {
                        return `
                            <a href="javascript:void();" onclick="GetLocation('${row.latitude}', '${row.longitude}');" style="cursor:pointer">
                                ${data} 
                            </a>                            
                            
                           `
                    }
                },

                { "data": "bookingStatus" },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="/Customer/Booking/Upsert/${data}" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                            </div>

                           `
                    }
                },

                {
                    "data": "id",

                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="javascript:void()" onclick="Delete(${data})" style="cursor:pointer">
                                    <i class="fas fa-trash"></i>
                                </a>
                            </div>

                           `
                    }
                }
            ]
        });
    }
}

$.fn.dataTable.render.moment = function () {

    return function (d, type, row) {

        if (!d) {
            return type === 'sort' || type === 'type' ? 0 : d;
        }

        var m = moment(d).format('DD-MMM-YYYY hh:mm A');

        return m;
    };
};

function Delete(id) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangermode: true
    }).then((willDelete) => {
        if (willDelete) {

            $.ajax({
                type: "DELETE",
                crossDomain: true,
                url: 'http://localhost:45235/api/Booking/' + id,
                success: function (data) {

                    toastr.success(data);
                    dataTable.ajax.reload();
                    dataTable_Previous.ajax.reload();
                }
            })
        }
    });
}


