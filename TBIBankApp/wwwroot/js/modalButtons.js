let butTestDisable;
let timercheto;
let modalId;
$(document).ready(function () {
    $('#example').DataTable();
});
function ModalTest(id) {
    $(`.${id}`).modal('show');
}
$(`.${modalId}`).on('hidden.bs.modal', () => {
    console.log(213123);
    SetButtonToEnable(modalId);
})

function ChekForDisable(id) {
    let thirtyminutes = 1800;
    butTestDisable = document.getElementById(id);
    $.ajax(
        {
            type: "GET",
            url: "IsItOpenAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data:
            {
                'id': id,
            },
            success: function (response) {
                if (response === "true") {
                    butTestDisable.setAttribute("disabled", true);
                    $(butTestDisable).css('background-color', 'red');
                    $(butTestDisable).text('Denied');
                }
                else {
                    modalId = id;
                    $(`.${id}`).modal('show');
                    starttimer(thirtyminutes, id);

                }
            }
        })
}
function starttimer(duration, id) {
    console.log(duration);
    let idName = id + '+timerId';
    console.log(idName);
    let timer = duration, minutes, seconds;
    if (duration != 0) {

        timercheto = setInterval(function () {
            if (timer == -1) {
                $(butTestDisable).removeAttr("disabled");
                clearTimeout(timercheto);
            }
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            if (document.getElementById(idName) === null) {
                clearTimeout(timercheto);
                console.log(15);
            }
            else {

                document.getElementById(idName).innerHTML = "You have " + minutes + "m " + seconds + "s " + "before clsoing automaticly"
            }
            if (--timer < 0) {
                SetButtonToEnable(id);
                $(`.${id}`).modal('hide');
            }
        }, 1000);
    }
}

function SetButtonToEnable(id) {
    clearTimeout(timercheto);
    butTestDisable = document.getElementById(id);
    $.ajax(
        {
            type: "Get",
            url: "SetToEnableAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data:
            {
                'id': id,
            },
            success: function () {
                $(butTestDisable).removeAttr("disabled");
                //butTestDisable.setAttribute("disabled", false);
                $(butTestDisable).css('background-color', '#007bff');
                $(butTestDisable).text('Preview');

            }

        })
}


function SetInvalid(value) {
    clearTimeout(timercheto);
    let but = document.getElementById(value + '+classID');
    console.log(but);
    but.remove();
    data = {
        id: value,
        status: 'InvalidApplication'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatusAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: data,
            success: function () {
                window.location.replace("/Email/ListEmailsAsync?emailStatus=InvalidApplication");
            }
        })
};


function SetNew(value) {
    clearTimeout(timercheto);
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'New'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatusAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: data,
            success: function () {
            }
        })
}

function SetClosed(value) {
    clearTimeout(timercheto);
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
            url: "ChangeStatusAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
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
            url: "ChangeStatusAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: data,
            success: function () {
            }
        })
}
function SetOpen(value) {

    let firstName = value + '+customFirstName';
    let lastName = value + '+customLastName';
    let Egn = value + '+customEgn';
    let cardId = value + '+customCardId';
    let checkEgn = value + '+checkEgn';

    let firstname = document.getElementById(firstName).value;
    let lastname = document.getElementById(lastName).value;
    let egn = document.getElementById(Egn).value;
    let cardid = document.getElementById(cardId).value;
    let but = document.getElementById(value + '+classID');
    let checkegn = document.getElementById(checkEgn);

    data = {
        'EmailId': value,
        'FirstName': firstname,
        'LastName': lastname,
        'EGN': egn,
        'CardId': cardid
    }
    $.ajax(
        {
            type: "Post",
            url: "/Application/CreateAsync",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(data),
            dataType: 'text',
            success: function (response) {
                console.log(response);
                if (response === `"false"`) {
                    console.log($(checkegn));
                    $(checkegn).text('This EGN is invalid!');
                }
                else {
                    but.remove();
                    $.ajax(
                        {
                            type: "Get",
                            url: 'ChangeStatusAsync',
                            data: {
                                'id': value,
                                'status': 'Open'
                            },
                            success: function () {
                                console.log(221323);
                                $(`.${value}`).modal('hide');
                                $('.modal-backdrop').hide();
                            },
                            error: function () {
                                $(`.${value}`).modal('hide');
                            }
                        })
                }
            }

        })
}
