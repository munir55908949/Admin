
$(document).ready(function () {
    $("#btnSearch").click(function (e) {
        $('#gridView').jtable('load', {
            UserName: $("#UserName").val(),//view
            FullName: $("#FullName").val(),
            PhoneNumber: $("#PhoneNumber").val(),
        });
    });
    //$('#gridView').jtable();
    $('#gridView').jtable({
        title: "قائمة المستخدمين",
        paging: true,
        sorting: false,
        actions: {
            listAction: ROOT + 'Users/Search'
        },
        toolbar: {
            hoverAnimation: true, //Enable/disable small animation on mouse hover to a toolbar item.
            hoverAnimationDuration: 60, //Duration of the hover animation.
            hoverAnimationEasing: undefined, //Easing of the hover animation. Uses jQuery's default animation ('swing') if set to undefined.
            items: [{
                text: 'إضافة مستخدم',
                cssClass: 'lnkAddVendor',
                icon: "/Content/assets/plugins/jtable/themes/lightcolor/Add.png"
            }
            ]
        },
        recordsLoaded: function (event, data) {
            RowNumber = 0;
            var footer = $('table.jtable').find('tfoot');
            if (!footer.length) {
                footer = $('<tfoot>').appendTo('table.jtable');
                footer.append($('<tr><td class="total-report" >عدد المستخدمين</td><td  class="total-report"  id="countUser"colspan="9">00</td></tr>'));
            }
            var count = 0;
            var items = data.serverResponse['Records'];
            $.each(items, function (index, value) {
                count += 1;
            });
            $('#countUser').html(count);
        },
        fields: {
            Serial: {
                width: '5%',
                title: "م",
                sorting: false,
                display: function () {
                    RowNumber++;
                    return RowNumber;
                }
            },
            UserName: {
                width: '10%',
                title: "اسم المستخدم",
            },
            FullName: {
                width: '10%',
                title: "الاسم كامل",
            },
            PhoneNumber: {
                width: '10%',
                title: " رقم الجوال",
            },

            ActiveDesActive: {
                title: "الحالة",
                width: '10%',
                sorting: false,
                display: function (data) { return $(data.record.IsActive == true ? '<a class="label label-default" href="javascript:DesActive(\'' + data.record.UserName + '\')">ايقاف</a>' : '<a class="label label-danger" href="javascript:Active(\'' + data.record.UserName + '\')">تفعيل</a>'); }
            },
            ChangePassword: {
                title: "تغير الرقم السري",
                width: '10%',
                sorting: false,
                display: function (data) { return $('<a class="label label-default" href="Manage/ChangePassword/' + data.record.Id + '">تغير</a>'); }
            },
            Edit: {
                title: "تعديل",
                width: '5%',
                sorting: false,
                display: function (data) { return $('<a href="Users/AddEdit/' + data.record.Id + '"><i style="color:Blue;font-size:26px;" class="fa fa-file-text-o"></i></a>'); }
            }
            },
            Delete: {
                title: "حذف",
                width: '5%',
                sorting: false,
                display: function (data) { return $('<a href="javascript:DeleteItem(\'' + data.record.Id + '\')"><i style="color:red;font-size:26px;" class="fa fa-trash-o"></i></a>'); }
            }
    });

    $("#btnSearch").trigger("click");

    $(".lnkAddVendor").click(function () {
        $(this).css('cursor', 'pointer');
        window.location = ROOT + "Users/AddEdit";
    });
});

function DeleteItem(UserName) {
    swal({ title: "", text: "هل تريد حذف هذا المستخدم ؟", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                var url1 = '/Users/DeleteItem?UserName=' + UserName;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data == "Success") {
                            swal("", "تم حذف المستخدم بنجاح", "success");
                            $("#btnSearch").trigger("click");
                        } 
                    }
                });
            }
            else { swal("", "لم يتم حذف المستخدم", "error"); event.preventDefault(); }
        });
}

function Active(UserName) {
    swal({ title: "", text: "هل تريد تفعيل هذا المستخدم ؟", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                var url1 = '/Users/Active?UserName=' + UserName;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data == "Success") {
                            swal("", "تم تفعيل المستخدم بنجاح", "success");
                            $("#btnSearch").trigger("click");
                        }
                    }
                });
            }
            else { swal("", "لم يتم تفعيل المستخدم", "error"); event.preventDefault(); }
        });
}
function DesActive(UserName) {
    swal({ title: "", text: "هل تريد ايقاف هذا المستخدم ؟", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "نعم", cancelButtonText: "الغاء", closeOnConfirm: false, closeOnCancel: false },
        function (isConfirm) {
            if (isConfirm) {
                var url1 = '/Users/DesActive?UserName=' + UserName;
                $.ajax({
                    url: url1,
                    type: 'Get',
                    cache: false,
                    success: function (data) {
                        if (data == "Success") {
                            swal("", "تم تفعيل المستخدم بنجاح", "success");
                            $("#btnSearch").trigger("click");
                        }
                    }
                });
            }
            else { swal("", "لم يتم ايقاف المستخدم", "error"); event.preventDefault(); }
        });
}