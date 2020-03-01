function FullDateFormat(date) {
    var d = new Date(date);
    var setdate = ("0" + d.getDate()).slice(-2) + "." +
        ("0" + (d.getMonth() + 1)).slice(-2) + "." +
        d.getFullYear() + " " +
        ("0" + d.getHours()).slice(-2) + ":" +
        ("0" + d.getMinutes()).slice(-2) + ":" +
        ("0" + d.getSeconds()).slice(-2);
    return setdate;
};
function ShortDateFormat(date) {
    if (date == '' || date == null) {
        return '/';
    }
    else {
        var d = new Date(date);
        var setdate = ("0" + d.getDate()).slice(-2) + "." +
            ("0" + (d.getMonth() + 1)).slice(-2) + "." +
            d.getFullYear();
        return setdate;
    }
};

function MoneyFormat(value) {
    const formatter = new Intl.NumberFormat('it-IT', {
        style: 'currency',
        currency: 'EUR'
    });
    return formatter.format(value).replace('€','');
};

function ChangeFile(getfile) {
    var name = getfile.files && getfile.files.length ? getfile.files[0].name : 'No file chosen';
    var fileExtension = ['docx', 'pdf', 'doc', 'png', 'jpg', 'jpeg'];
    if ($.inArray(getfile.files[0].name.split('.').pop().toLowerCase(), fileExtension) == -1) {
        //   alert("This file format is not allowed!!. \n Accepted File Types : .docx .pdf .doc .png .jpg .jpeg");
     //   $("#filenotsupportmsg").append("This file format is not allowed!!");
        //$("#filenotsupportmsg").append("This file format is not allowed!!");
        $("#filenotsupportmsg").text(null).append("Formati i dokumentit ." + getfile.files[0].name.split('.').pop().toLowerCase() + " nuk lejohet !!");
        $("#lblfile").text('No file chosen');
        $("#file").val('');
        return false;
    }
    else {
        $("#lblfile").text(name);
        $("#filenotsupportmsg").text(null);

    }
};

function AddDataTable(id) {

  
       $('#' + id).DataTable({
            lengthChange: false,
            ordering: false,
           dom: 'Blfrtip',
           stateSave: true,
           bAutoWidth: false,
            buttons: [
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'LEGAL',
                    footer: true,
                    text: '<i class="fas fa-file-pdf"></i> PDF',
                    titleAttr: 'Extract to PDF',
                    exportOptions: {
                        columns: "thead th:not(.noExport)"
                    }
                },
                {
                    extend: 'excel',
                    footer: false,
                    text: '<i class="fas fa-file-excel"></i> Excel',
                    titleAttr: 'Extract to Excel',
                    exportOptions: {
                        columns: "thead th:not(.noExport)"
                    }
                }
            ],
            "language": {
                "loadingRecords": "Please wait - loading...",
                "search": "",
                "searchPlaceholder": "Search...",
            }
        });
  
};
function DestoryDataTable(id) {
    $("#" + id).DataTable().clear().destroy();
}

function CompareDates(fromDate, toDate,label,RemTodayDateVal) {
    var _return = true;
    var today = new Date();
    var frommonthfield = fromDate.split("/")[1];
    var fromdayfield = fromDate.split("/")[0];
    var fromyearfield = fromDate.split("/")[2];


    var tomonthfield = toDate.split("/")[1];
    var todayfield = toDate.split("/")[0];
    var toyearfield = toDate.split("/")[2];


    var fromDate = new Date(fromyearfield, frommonthfield - 1, fromdayfield);
    var toDate = new Date(toyearfield, tomonthfield - 1, todayfield);
    if (RemTodayDateVal != 1) {
        if (fromDate.getTime() > today.getTime()) {
            $("#" + label).text("Data prej duhet te jete me e vogel se data e sotme")
            _return = false;
        }
   
    if (toDate.getTime() > today.getTime()) {
        $("#" + label).text("Data deri duhet te jete me e vogel se data e sotme")
        _return = false;
        }
    }
    if (fromDate.getTime() > toDate.getTime()) {
        $("#" + label).text("Data Prej duhet te jete me e vogel se data Deri")
        _return = false;
    }
    if (_return) {
        $("#" + label).text('');
    }
    return _return;
}


function GetWorkFlow(CPEID, geturl, getdownloadurl) {

    $("#workflow").modal('show');
    $("#workflowalert").html("");
    $("#wftitle").text("Procesi i punes për vlerësimin #" + CPEID);
    $("#tblworkflow tbody").html(null);
    $.ajax({
        type: 'POST',
        url: geturl,
        data: { CPEID: CPEID },
        datatype: 'json',
        success: function (res) {
            var totalwf = 0;
            var jsonData = JSON.parse(res.workflow);
            $.each(jsonData, function (i, item) {
                totalwf++;
                var d = new Date(item.InsertDate);
                var SubmitDate = new Date(item.Date);
                var hasFile = (item.hasFile == 1) ? "<a href='" + getdownloadurl+"/" + item.CPEEvApproveID + "' class='btn btn-sm btn-lang'><i class=\"fas fa-file-download\"></i></a>" : "";
                var setdate = ("0" + d.getDate()).slice(-2) + "." +
                    ("0" + (d.getMonth() + 1)).slice(-2) + "." +
                    d.getFullYear() + " " +
                    ("0" + d.getHours()).slice(-2) + ":" +
                    ("0" + d.getMinutes()).slice(-2) + ":" +
                    ("0" + d.getSeconds()).slice(-2);

                var setSubmitdate = ("0" + SubmitDate.getDate()).slice(-2) + "." +
                    ("0" + (SubmitDate.getMonth() + 1)).slice(-2) + "." +
                    SubmitDate.getFullYear();
                $("#tblworkflow tbody").append(
                    "<tr>"
                    + "<td>" + item.FromStatusName + "</td>"
                    + "<td><b class='text-success'>" + item.StatusName + "</b></td>"
                    + "<td>" + item.Comment + "</td>"
                    + "<td>" + setSubmitdate + "</td>"
                    + "<td>" + setdate + "</td>"
                    + "<td>" + item.InsertBy + "</td>"
                    + "<td class='text-center'>" + hasFile + "</td>"
                    + "</tr>")
            });
            if (totalwf == 0) {
                var getdiv = document.getElementById("workflowalert");
                getdiv.innerHTML = '<div class="alert alert-info">0 Work flow found</div>';
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Error,please contact support");
        }

    });

   

}
function CleanHTML(id) {
    $("#" + id).html(null);
}

$('.dsearch').select2();