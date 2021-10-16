function DeleteItem(Id, text, controller) {
    swal({ title: "", text: text + "?", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                $("#loadingPanel").show();
                var url1 = '/' + controller + '/DeleteItem?Id=' + Id;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data === "Success") {
                            swal({ title: "", text: "تم الحذف بنجاح", type: "success", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false });
                            refreshGrid("Grid");
                            $("#loadingPanel").hide();
                        }
                    }
                });
            }
            else { swal({ title: "", text: "لم يتم الحذف بنجاح", type: "error", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false }); $("#loadingPanel").hide(); event.preventDefault(); }
        });
}
function DeleteItemDetails(Id, text, action, controller,gridName) {
    console.log(action);
    swal({ title: "", text: text + "?", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                $("#loadingPanel").show();
                var url1 = '/' + controller + '/'+action+ '?Id=' + Id;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data === "Success") {
                            swal({ title: "", text: "تم الحذف بنجاح", type: "success", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false });
                            refreshGrid(gridName);
                            $("#loadingPanel").hide();
                        }
                    }
                });
            }
            else { swal({ title: "", text: "لم يتم الحذف بنجاح", type: "error", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false }); $("#loadingPanel").hide(); event.preventDefault(); }
        });
}
function DeleteWarrantyHistory(Id, text, action, controller, gridName) {
    console.log(action);
    swal({ title: "", text: text + "?", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                $("#loadingPanel").show();
                var url1 = '/' + controller + '/' + action + '?Id=' + Id;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data === "Success") {
                            swal({ title: "", text: "تم الحذف بنجاح", type: "success", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false });
                            refreshGrid(gridName);
                            $("#loadingPanel").hide();
                        }
                    }
                });
            }
            else { swal({ title: "", text: "لم يتم الحذف بنجاح", type: "error", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false }); $("#loadingPanel").hide(); event.preventDefault(); }
        });
}
function refreshGrid(gridName) {
    var filters = [];
    var grid = $("#" + gridName).data("kendoGrid");
    var dataSource = grid.dataSource;
    dataSource.filter({
        logic: "and",
        filters: filters
    });

}