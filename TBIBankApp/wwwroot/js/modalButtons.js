let butTestDisable;
$(document).ready(function () {
    $('#example').DataTable();
});
function ModalTest(id) {
    $(`.${id}`).modal('show');
    butTestDisable = document.getElementById(id);
    setTimeout('Disable()', 0,01);
}
function Disable() {
    butTestDisable.setAttribute("disabled", true);
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

function SetClosed(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'Closed'
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
function SetNotReviewed(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'NotReviewed'
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
function SetOpen(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'Open'
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
