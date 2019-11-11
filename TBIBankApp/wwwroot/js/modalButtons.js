$(document).ready(function () {
    $('#example').DataTable();
});
function ModalTest(id) {
    $(`.${id}`).modal('show');
}

function SetInvalid(value) {
    let but = document.getElementById(value+'+classID');
    console.log(but);
    but.remove();
    data = {
        id: value,
        status: 'InvalidApplication'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
};


function SetNew(value) {
    console.log(value)
    let but = document.getElementById(value+'+classID');
    but.remove();
    data = {
        id: value,
        status: 'New'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
}
