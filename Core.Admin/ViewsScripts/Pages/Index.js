
$(document).ready(function () {
    $("#btnSearch").click(function (e) {
        $('#gridView').jtable('load', {
            Name: $("#Name").val(),
        });
    });
    //$('#gridView').jtable();
    $('#gridView').jtable({
        title: "قائمة الصلاحيات",
        paging: true,
        sorting: false,
        actions: {
            listAction: ROOT + 'Pages/Search'
        },
        toolbar: {
            items: [
                {
                    text: 'إضافة صلاحية',
                    cssClass: 'lnkAddVendor',
                    icon: "/Content/assets/plugins/jtable/themes/lightcolor/Add.png"
                }
            ]
        },
        recordsLoaded: function (event, data) {
            RowNumber = 0;
        },
        fields: {
            Serial: {
                sorting: false,
                width: '5%',
                title: "م",
                display: function () {
                    RowNumber++;
                    return RowNumber;
                }
            },
            Name: {
                width: '15%',
                title: "الأسم",
            },
            Url: {
                width: '15%',
                title: "الرابط",
            },
            Icon: {
                width: '15%',
                title: "العلامة",
            },
            Order: {
                width: '15%',
                title: "الترتيب",
            },
            Edit: {
                title: "تعديل",
                width: '5%',
                sorting: false,
                display: function (data) { return $('<a href="Pages/AddEdit/' + data.record.Id + '"><i style="color:Blue;font-size:26px;" class="fa fa-file-text-o"></i></a>'); }

            },
        }
    });

    $("#btnSearch").trigger("click");

    $(".lnkAddVendor").click(function () {
        $(this).css('cursor', 'pointer');
        window.location = ROOT + "Pages/AddEdit";
    });
});
function DeleteItem(Id) {
    swal({ title: "", text: "هل تريد حذف هذا العنصر ؟", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                $("#loadingPanel").show();
                var url1 = '/Pages/DeleteItem?Id=' + Id;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data == "Success") {
                            swal("", "تم حذف العنصر بنجاح", "success");
                            $("#btnSearch").trigger("click");
                            $("#loadingPanel").hide();
                        }
                    }
                });
            }
            else { swal("", "لم يتم حذف القسم", "error"); $("#loadingPanel").hide(); event.preventDefault(); }
        });
}
