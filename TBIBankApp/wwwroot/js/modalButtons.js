let butTestDisable;
let timercheto;
$(document).ready(function () {
    $('#example').DataTable();
});
function ModalTest(id) {
    $(`.${id}`).modal('show');
}

function ChekForDisable(id) {
    let thirtyminutes = 1800;
    butTestDisable = document.getElementById(id);
    $.ajax(
        {
            type: "GET",
            url: "IsItOpen",
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
            url: "SetToEnable",
            data:
            {
                'id': id,
            },
            success: function () {
                $(butTestDisable).removeAttr("disabled");
                //butTestDisable.setAttribute("disabled", false);
                $(butTestDisable).css('background-color', '#007bff');
                $(butTestDisable).text('Body Preview');

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
            url: "ChangeStatus",
            data: data,
            success: function () {
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
            url: "ChangeStatus",
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

    let firstName = value + '+customFirstName';
    let lastName = value + '+customLastName';
    let Egn = value + '+customEgn';
    let firstname = document.getElementById(firstName).value;
    let lastname = document.getElementById(lastName).value;
    let egn = document.getElementById(Egn).value;
    let but = document.getElementById(value + '+classID');
    but.remove();
    //data = {
    //    id: value,
    //    status: 'Open'


    //};
    data = {
        'EmailId': value,
        'FirstName': firstname,
        'LastName': lastname,
        'EGN': egn
    }
    $.ajax(
        {
            type: "Post",
            url: "http://localhost:54266/Application/Create",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(data),
            dataType: 'text',
            success: function () {
                console.log(222);
                $.ajax(
                    {
                       
                        type: "Get",
                        url: 'ChangeStatus',
                        data: {
                            'id': value,
                            'status': 'Open'
                        },
                        success: function () {
                        }
                    })
            }
        })
    

}
